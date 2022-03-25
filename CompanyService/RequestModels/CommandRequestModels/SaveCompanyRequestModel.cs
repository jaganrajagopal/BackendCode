using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.RequestModels.CommandRequestModels
{
    public class SaveCompanyRequestModel
    {
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public double CompanyTurnover { get; set; }
        public string CompanyWebsite { get; set; }
    }
}
