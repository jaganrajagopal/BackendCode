using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Data.Entities
{
    public class Stock
    {
        public int StockId { get; set; }
        public double StockPrice { get; set; }
        public DateTime StockDateTime { get; set; }
        public string StockExchange { get; set; }
        public int CompanyCode { get; set; }
      
    }
}
