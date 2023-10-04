using CompanyManagement.API.Models;

namespace CompanyManagement.API.Services.Client
{
    public interface IClientService
    {
        /// <summary>
        /// Create multiple client
        /// </summary>
        /// <param name="clientModels"></param>
        /// <returns>Status code and the error message if not empty</returns>
        Task<(int statusCode, string errorMessage)> Create(IEnumerable<ClientModel> clientModels);
    }
}
