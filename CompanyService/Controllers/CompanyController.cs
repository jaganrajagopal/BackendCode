using CompanyService.Contracts.CommandHandlers;
using CompanyService.Contracts.QueryHandlers;
using CompanyService.RequestModels.CommandRequestModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService.Controllers
{
    [ApiController]
    [Route("api/v1.0/market/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ISaveCompanyCommandHandler _saveCompanyCommandHandler;
        private readonly IAllCompaniesQueryHandler _allCompaniesQueryHandler;
        private readonly IGetCompanyByIdQueryHandler _getCompanyByIdQueryHandler;
        private readonly IDeleteCompanyCommandHandler _deleteCompanyCommandHandler;
        public CompanyController(ISaveCompanyCommandHandler saveCompanyCommandHandler, IAllCompaniesQueryHandler allCompaniesQueryHandler, IGetCompanyByIdQueryHandler getCompanyByIdQueryHandler,IDeleteCompanyCommandHandler deleteCompanyCommandHandler)
        {
            _saveCompanyCommandHandler = saveCompanyCommandHandler;
            _allCompaniesQueryHandler = allCompaniesQueryHandler;
            _getCompanyByIdQueryHandler = getCompanyByIdQueryHandler;
            _deleteCompanyCommandHandler = deleteCompanyCommandHandler;

        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            // TOOO: Reuse and close connections and channel, etc, 
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "company",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            var result = await _saveCompanyCommandHandler.SaveAsync(requestModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> AllCompanies()
        {
            var result = await _allCompaniesQueryHandler.GetListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("info/{companyCode}")]
        public async Task<IActionResult> GetCompanyById(int companyCode)
        {
            var result = await _getCompanyByIdQueryHandler.GetCompanyByIDAsync(companyCode);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{companyCode}")]
        public async Task<IActionResult> DeleteCompany(int companyCode)
        {
            var result = await _deleteCompanyCommandHandler.DeleteCompanyAsync(companyCode);
            var integrationEventData = JsonConvert.SerializeObject(new
            {
                companyCode = companyCode,
            });
            PublishToMessageQueue("stock.delete", integrationEventData);
            return Ok(result);
        }
    }
}
