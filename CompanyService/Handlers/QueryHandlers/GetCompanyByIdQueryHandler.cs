using CompanyService.Contracts.QueryHandlers;
using CompanyService.Data.Entities;
using CompanyService.ResponseModels.QueryResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyService.Handlers.QueryHandlers
{
    public class GetCompanyByIdQueryHandler : IGetCompanyByIdQueryHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public GetCompanyByIdQueryHandler(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<AllCompaniesResponseModel> GetCompanyByIDAsync(int companyCode)
        {
            var result = await _applicationDBContext.Companies.Where(_ => _.CompanyCode == companyCode).FirstOrDefaultAsync();
            if(result!=null)
            {
                HttpClient client = new HttpClient();
                List<Stock> stock = null;
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44370/api/v1.0/market/Stock/getDetail?companyCode={companyCode}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    stock = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stock>>(content);
                }
                return new AllCompaniesResponseModel
                {
                    CompanyCode = result.CompanyCode,
                    CompanyName = result.CompanyName,
                    CompanyCEO = result.CompanyCEO,
                    CompanyTurnover = result.CompanyTurnover,
                    CompanyWebsite = result.CompanyWebsite,
                    stocks = stock
                };
            }
            return null;
        }
    }
}
