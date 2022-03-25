using StockService.ResponseModels.QueryResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Contracts.QueryHandlers
{
    public interface IDateRangeStockQueryHandler
    {
        Task<List<DateRangeStocksResponseModel>> GetListAsync(DateTime startDateTime, DateTime endDateTime,int companyCode);
        Task<List<DateRangeStocksResponseModel>> GetStockDetails(int companyCode);

    }
}
