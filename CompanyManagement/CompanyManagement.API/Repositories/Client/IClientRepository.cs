using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Client
{
    public interface IClientRepository
    {
        /// <summary>
        /// Create multiple client
        /// </summary>
        /// <param name="clientModels"></param>
        /// <returns>Status code and the error message if not empty</returns>
        Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels);

        /// <summary>
        /// Get client from database
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync();
    }
}
