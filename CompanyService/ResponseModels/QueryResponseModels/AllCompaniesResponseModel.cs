using CompanyService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.ResponseModels.QueryResponseModels
{
    public class AllCompaniesResponseModel
    {
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public double CompanyTurnover { get; set; }
        public string CompanyWebsite { get; set; }
        public List<Stock> stocks { get; set; }
        
    }
}
