using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Client
{
    public interface IClientRepository
    {
        Task<(int statusCode, string errorMessage)> Create(IEnumerable<ClientModel> clientModels);
    }
}
