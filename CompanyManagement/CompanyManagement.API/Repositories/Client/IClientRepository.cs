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
        Task<(int statusCode, string errorMessage)> CreateAsync(IEnumerable<ClientModel> clientModels);
    }
}
