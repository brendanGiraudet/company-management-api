using CompanyManagement.API.EqualityComparers;
using CompanyManagement.API.Models;
using Microsoft.EntityFrameworkCore;

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
            return newClients.Where(c => _databaseContext.Clients.FirstOrDefaultAsync(f => f.Name == c.Name && f.Email == c.Email) == null);
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync()
        {
            try
            {
                return (StatusCodes.Status200OK, _databaseContext.Clients);
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ClientModel>());
            }
        }
    }
}
