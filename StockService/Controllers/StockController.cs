using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using StockService.Contracts.CommandHandlers;
using StockService.Contracts.QueryHandlers;
using StockService.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService.Controllers
{
    [ApiController]
    [Route("api/v1.0/market/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ISaveStockCommandHandler _saveStockCommandHandler;
        private readonly IDateRangeStockQueryHandler _dateRangeStockQueryHandler;
        public StockController(ISaveStockCommandHandler saveStockCommandHandler, IDateRangeStockQueryHandler dateRangeStockQueryHandler)
        {
            _saveStockCommandHandler = saveStockCommandHandler;
            _dateRangeStockQueryHandler = dateRangeStockQueryHandler;
        }
        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            // TOOO: Reuse and close connections and channel, etc, 
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "stock",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }
        [HttpPost]
        [Route("add/{companyCode}")]
        public async Task<IActionResult> SaveStockAsync(SaveStockRequestModel requestModel,int companyCode)
        {
            var result = await _saveStockCommandHandler.SaveStockAsync(requestModel,companyCode);
            return Ok(result);
        }

        [HttpGet]
        [Route("date-range/{companyCode}/{startdate}/{enddate}")]
        public async Task<IActionResult> DateRangeStocks(int companyCode, DateTime startdate, DateTime enddate)
        {
            var result = await _dateRangeStockQueryHandler.GetListAsync(startdate, enddate, companyCode);
            return Ok(result);
        }

        [HttpGet]
        [Route("getDetail")]
        public async Task<IActionResult> GetStockDetails(int companyCode)
        {
            var result = await _dateRangeStockQueryHandler.GetStockDetails(companyCode);
            return Ok(result);
        }
    }
}
