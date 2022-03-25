using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.ResponseModels.QueryResponseModels
{
    public class DateRangeStocksResponseModel
    {
        public double StockPrice { get; set; }
        public DateTime StockDateTime { get; set; }
        public string StockExchange { get; set; }
    }
}
