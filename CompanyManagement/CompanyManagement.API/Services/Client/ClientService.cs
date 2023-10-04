using CompanyManagement.API.Models;
using CompanyManagement.API.Repositories.Client;

namespace CompanyManagement.API.Services.Client
{
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <inheritdoc/>
        public Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels)
        => _clientRepository.CreateAsync(clientModels);
        
        /// <inheritdoc/>
        public Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync()
        => _clientRepository.GetAsync();
    }
}
