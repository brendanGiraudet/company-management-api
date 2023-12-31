using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Service
{
    public interface IServiceRepository
    {
        /// <summary>
        /// Create multiple service
        /// </summary>
        /// <param name="serviceModels"></param>
        /// <returns>Status code and services</returns>
        Task<(int statusCode, IEnumerable<ServiceModel> createdServices)> CreateAsync(IEnumerable<ServiceModel> serviceModels);


        /// <summary>
        /// Get services
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, IEnumerable<ServiceModel> services)> GetAsync();
        
        /// <summary>
        /// Get service
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, ServiceModel? service)> GetAsync(string id);
        
        /// <summary>
        /// Update service
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, ServiceModel? updatedService)> UpdateAsync(ServiceModel serviceModel);


        /// <summary>
        /// Delete service
        /// </summary>
        /// <returns>Status code</returns>
        Task<int> DeleteAsync(string id);
    }
}
