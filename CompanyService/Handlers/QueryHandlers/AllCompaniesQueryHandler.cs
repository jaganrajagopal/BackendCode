using CompanyService.Contracts.QueryHandlers;
using CompanyService.Data.Entities;
using CompanyService.ResponseModels.QueryResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace CompanyService.Handlers.QueryHandlers
{
    public class AllCompaniesQueryHandler : IAllCompaniesQueryHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public AllCompaniesQueryHandler(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<List<AllCompaniesResponseModel>> GetListAsync()
        {
            List<AllCompaniesResponseModel> allCompaniesResponseModels = new List<AllCompaniesResponseModel>();

            var result = await _applicationDBContext.Companies.ToListAsync();
            foreach(var r in result)
            {
                 HttpClient client = new HttpClient();
                //var item = await client.GetAsync("/stock");
                List<Stock> stock = null;
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44370/api/v1.0/market/Stock/getDetail?companyCode={r.CompanyCode}");
                if (response.IsSuccessStatusCode)
                {
                   var content = await response.Content.ReadAsStringAsync();
                   stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stock>>(content);
                }
                allCompaniesResponseModels.Add(new AllCompaniesResponseModel
                {
                    CompanyCode = r.CompanyCode,
                    CompanyName = r.CompanyName,
                    CompanyCEO = r.CompanyCEO,
                    CompanyTurnover = r.CompanyTurnover,
                    CompanyWebsite = r.CompanyWebsite,
                    stocks = stock
                });
            }
            return allCompaniesResponseModels;
        }
    }
}
