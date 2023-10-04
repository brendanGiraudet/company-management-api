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

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> CreateAsync(IEnumerable<ClientModel> clientModels)
        {
            var result = await _clientService.CreateAsync(clientModels);

            return StatusCode(result.statusCode, result.createdClients);
        }
        
        [HttpGet(Name = "Get")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _clientService.GetAsync();

            return StatusCode(result.statusCode, result.clients);
        }
    }
}
