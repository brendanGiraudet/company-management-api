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
        Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels);

        /// <summary>
        /// Get clients
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync();

        /// <summary>
        /// Get client
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, ClientModel? client)> GetAsync(string id);
        
        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="clientModel"></param>
        /// <returns></returns>
        Task<(int statusCode, ClientModel updatedClient)> UpdateAsync(ClientModel clientModel);
    }
}