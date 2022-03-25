using StockService.Contracts.CommandHandlers;
using StockService.Data.Entities;
using StockService.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Handlers.CommandHandlers
{
    public class SaveStockCommandHandler : ISaveStockCommandHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public SaveStockCommandHandler(ApplicationDBContext context)
        {
            _applicationDBContext = context;
        }
        public async Task<int> SaveStockAsync(SaveStockRequestModel saveStockRequestModel, int companyCode)
        {
            var stock = new Stock
            {
                StockPrice = saveStockRequestModel.StockPrice,
                StockDateTime = saveStockRequestModel.StockDateTime,
                StockExchange = saveStockRequestModel.StockExchange,
                CompanyCode = companyCode
            };
            _applicationDBContext.Stocks.Add(stock);
            await _applicationDBContext.SaveChangesAsync();
            return stock.StockId;
        }
    }
}
