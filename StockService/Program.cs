using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using StockService.ResponseModels.QueryResponseModels;

namespace StockService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntegrationEvents();
            CreateHostBuilder(args).Build().Run();
        }
        private static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var contextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().
                    UseSqlServer("Server=DOTNETMC;Database=CQRSStockAPI;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;
                var dbContext = new ApplicationDBContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "stock.delete")
                {
                    int companyCode = data["companyCode"].Value<int>();
                    //Stock stock = dbContext.Stocks.FirstOrDefault(x => x.CompanyCode == companyCode);
                    List<Stock> stocksList = dbContext.Stocks.Where(x => x.CompanyCode == companyCode).ToList();
                    foreach(Stock stock in stocksList)
                    {
                        dbContext.Remove(stock);
                    }
                    dbContext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: "company.stockservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
