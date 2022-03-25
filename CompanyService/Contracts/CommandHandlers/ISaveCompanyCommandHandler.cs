using CompanyService.RequestModels.CommandRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Contracts.CommandHandlers
{
    public interface ISaveCompanyCommandHandler
    {
        Task<int> SaveAsync(SaveCompanyRequestModel saveCompanyRequestModel);
    }
}
