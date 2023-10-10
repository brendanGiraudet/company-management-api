using CompanyManagement.API.EqualityComparers;
using CompanyManagement.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CompanyManagement.API.Repositories.Client
{
    public class ClientRepository : IClientRepository
    {
        private DatabaseContext _databaseContext;

        public ClientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels)
        {
            using var dbContextTransaction = await _databaseContext.Database.BeginTransactionAsync();

            try
            {
                var newClients = clientModels.Distinct(new ClientEqualityComparer());

                newClients = GetUnexistedClients(newClients);

                _databaseContext.Clients.AddRange(newClients);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, newClients);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ClientModel>());
            }
        }

        private IEnumerable<ClientModel> GetUnexistedClients(IEnumerable<ClientModel> newClients)
        {
            var clients = new List<ClientModel>();

            foreach (var client in newClients)
            {
                if(_databaseContext.Clients.FirstOrDefault(f => f.Name == client.Name && f.Email == client.Email) == null) clients.Add(client);
            }
            
            return clients;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync()
        {
            try
            {
                return (StatusCodes.Status200OK, GetClients().ToList());
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ClientModel>());
            }
        }

        private IIncludableQueryable<ClientModel, HashSet<AddressModel>?> GetClients()
        {
            return _databaseContext.Clients.Include(_ => _.Addresses);
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, ClientModel? client)> GetAsync(string id)
        {
            try
            {
                return (StatusCodes.Status200OK, await GetClients().FirstOrDefaultAsync(c => c.Id == id));
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, null);
            }
        }
        
        /// <inheritdoc/>
        public async Task<(int statusCode, ClientModel updatedClient)> UpdateAsync(ClientModel clientModel)
        {
            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Update(clientModel);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status200OK, clientModel);
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, null);
            }
        }
        
        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string id)
        {
             var clientResult = await GetAsync(id);

            if (clientResult.client == null) return StatusCodes.Status204NoContent;

            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Remove(clientResult.client);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();

                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
