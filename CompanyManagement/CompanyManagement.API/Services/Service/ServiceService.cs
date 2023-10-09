using CompanyManagement.API.Models;
using CompanyManagement.API.Repositories.Service;

namespace CompanyManagement.API.Services.Service
{
    public class ServiceService : IServiceService
    {
        private IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ServiceModel> createdServices)> CreateAsync(IEnumerable<ServiceModel> serviceModels) => await _serviceRepository.CreateAsync(serviceModels);
        
        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ServiceModel> services)> GetAsync() => await _serviceRepository.GetAsync();
    }
}
