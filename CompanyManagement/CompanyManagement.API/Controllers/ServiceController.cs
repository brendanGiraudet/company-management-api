using CompanyManagement.API.Models;
using CompanyManagement.API.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.API.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServiceController : ControllerBase
    {
        private IServiceService _serviceService;

        public ServiceController(IServiceService ServiceService)
        {
            _serviceService = ServiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(IEnumerable<ServiceModel> serviceModels)
        {
            var result = await _serviceService.CreateAsync(serviceModels);

            return StatusCode(result.statusCode, result.createdServices);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _serviceService.GetAsync();

            return StatusCode(result.statusCode, result.services);
        }
    }
}
