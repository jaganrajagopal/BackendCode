using CompanyService.Contracts.CommandHandlers;
using CompanyService.Data.Entities;
using CompanyService.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Handlers.CommandHandlers
{
    public class SaveCompanyCommandHandler : ISaveCompanyCommandHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public SaveCompanyCommandHandler(ApplicationDBContext context)
        {
            _applicationDBContext = context;
        }
        public async Task<int> SaveAsync(SaveCompanyRequestModel saveCompanyRequestModel)
        {
            var company = new Company
            {
                CompanyName = saveCompanyRequestModel.CompanyName,
                CompanyCEO = saveCompanyRequestModel.CompanyCEO,
                CompanyTurnover = saveCompanyRequestModel.CompanyTurnover,
                CompanyWebsite = saveCompanyRequestModel.CompanyWebsite

            };
            _applicationDBContext.Companies.Add(company);
            await _applicationDBContext.SaveChangesAsync();
            return company.CompanyCode;
        }
    }
}
