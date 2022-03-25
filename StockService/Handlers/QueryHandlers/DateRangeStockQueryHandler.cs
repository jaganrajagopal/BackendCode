using Microsoft.EntityFrameworkCore;
using StockService.Contracts.QueryHandlers;
using StockService.Data.Entities;
using StockService.ResponseModels.QueryResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Handlers.QueryHandlers
{
    public class DateRangeStockQueryHandler : IDateRangeStockQueryHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public DateRangeStockQueryHandler(ApplicationDBContext context)
        {
            _applicationDBContext = context;
        }
        public async Task<List<DateRangeStocksResponseModel>> GetListAsync(DateTime startDateTime, DateTime endDateTime, int companyCode)
        {
            DateTime startDate = startDateTime.Date;
            DateTime endDate = endDateTime.Date;
            return await _applicationDBContext.Stocks
                .Where(_ => _.StockDateTime.Date >= startDate && _.StockDateTime.Date <= endDate && _.CompanyCode==companyCode)
                .Select(_ => new DateRangeStocksResponseModel
                {
                    StockPrice = _.StockPrice,
                    StockDateTime = _.StockDateTime,
                    StockExchange = _.StockExchange
                }).ToListAsync();
        }
        public async Task<List<DateRangeStocksResponseModel>> GetStockDetails(int companyCode)
        {
            return await _applicationDBContext.Stocks
                .Where(_ => _.CompanyCode==companyCode)
                .Select(_ => new DateRangeStocksResponseModel
                {
                    StockPrice = _.StockPrice,
                    StockDateTime = _.StockDateTime,
                    StockExchange = _.StockExchange
                }).OrderByDescending(_ => _.StockDateTime).Take(1).ToListAsync() ;
        }
    }
}
