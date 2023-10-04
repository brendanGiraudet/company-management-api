using CompanyManagement.API.Models;

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
                _databaseContext.Clients.AddRange(clientModels);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, clientModels);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ClientModel>());
            }
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
