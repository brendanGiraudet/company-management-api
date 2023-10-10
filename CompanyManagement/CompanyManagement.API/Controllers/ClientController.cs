using CompanyManagement.API.Models;
using CompanyManagement.API.Services.Client;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.API.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(IEnumerable<ClientModel> clientModels)
        {
            var result = await _clientService.CreateAsync(clientModels);

            return StatusCode(result.statusCode, result.createdClients);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _clientService.GetAsync();

            return StatusCode(result.statusCode, result.clients);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ClientModel clientModel)
        {
            var result = await _clientService.UpdateAsync(clientModel);

            return StatusCode(result.statusCode, result.updatedClient);
        }
    }
}
