using StockService.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Contracts.CommandHandlers
{
    public interface ISaveStockCommandHandler
    {
        Task<int> SaveStockAsync(SaveStockRequestModel saveStockRequestModel,int companyCode);
    }
}
