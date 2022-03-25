using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.RequestModels.CommandRequestModels
{
    public class SaveStockRequestModel
    {
        public double StockPrice { get; set; }
        public DateTime StockDateTime { get; set; }
        public string StockExchange { get; set; }
    
    }
}
