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
        public async Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels)
        => await _clientRepository.CreateAsync(clientModels);
        
        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync()
        => await _clientRepository.GetAsync();
        
        /// <inheritdoc/>
        public async Task<(int statusCode, ClientModel? client)> GetAsync(string id)
        => await _clientRepository.GetAsync(id);
        
        /// <inheritdoc/>
        public async Task<(int statusCode, ClientModel updatedClient)> UpdateAsync(ClientModel clientModel)
        => await _clientRepository.UpdateAsync(clientModel);

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string id)
        => await _clientRepository.DeleteAsync(id);
    }
}
