using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Data.Entities
{
    public class Company
    {
        [Key]
        public int CompanyCode { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCEO { get; set; }
        [Required]
        public double CompanyTurnover { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
    }
}
