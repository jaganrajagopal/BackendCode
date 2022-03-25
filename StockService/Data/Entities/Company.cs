using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Data.Entities
{
    public class Company
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public double CompanyTurnover { get; set; }
        public string CompanyWebsite { get; set; }
    }
}
