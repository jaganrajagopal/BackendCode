using CompanyService.Contracts.CommandHandlers;
using CompanyService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Handlers.CommandHandlers
{
    public class DeleteCompanyCommandHandler : IDeleteCompanyCommandHandler
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public DeleteCompanyCommandHandler(ApplicationDBContext context)
        {
            _applicationDBContext = context;
        }
        public async Task<bool> DeleteCompanyAsync(int companyCode)
        {
            Company company = await _applicationDBContext.Companies.FirstOrDefaultAsync(x => x.CompanyCode == companyCode);
            if (company == null)
            {
                return false;
            }
            _applicationDBContext.Companies.Remove(company);
            await _applicationDBContext.SaveChangesAsync();
            return true;
        }
    }
}
