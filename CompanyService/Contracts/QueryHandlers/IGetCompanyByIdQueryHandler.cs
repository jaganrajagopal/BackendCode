using CompanyService.ResponseModels.QueryResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyService.Contracts.QueryHandlers
{
    public interface IGetCompanyByIdQueryHandler
    {
        Task<AllCompaniesResponseModel> GetCompanyByIDAsync(int companyCode);
    }
}
