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
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _serviceService.GetAsync(id);

            return StatusCode(result.statusCode, result.service);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ServiceModel serviceModel)
        {
            var result = await _serviceService.UpdateAsync(serviceModel);

            return StatusCode(result.statusCode, result.updatedService);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var statusCode = await _serviceService.DeleteAsync(id);

            return StatusCode(statusCode);
        }
    }
}