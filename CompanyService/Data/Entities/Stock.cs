using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Data.Entities
{
    public class Stock
    {
        public double StockPrice { get; set; }
        public DateTime StockDateTime { get; set; }
        public string StockExchange { get; set; }

    }
}
