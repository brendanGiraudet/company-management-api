using CompanyManagement.API.Models;

namespace CompanyManagement.API.Services.Service
{
    public interface IServiceService
    {
        /// <summary>
        /// Create multiple service
        /// </summary>
        /// <param name="serviceModels"></param>
        /// <returns>Status code</returns>
        Task<(int statusCode, IEnumerable<ServiceModel> createdServices)> CreateAsync(IEnumerable<ServiceModel> serviceModels);
    }
}