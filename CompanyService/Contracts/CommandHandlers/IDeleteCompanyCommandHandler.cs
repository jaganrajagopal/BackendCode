using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Contracts.CommandHandlers
{
    public interface IDeleteCompanyCommandHandler
    {
        Task<bool> DeleteCompanyAsync(int companyCode);
    }
}
