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
        public async Task<(int statusCode, string errorMessage)> Create(IEnumerable<ClientModel> clientModels)
        {
            using var dbContextTransaction = await _databaseContext.Database.BeginTransactionAsync();

            try
            {
                _databaseContext.Clients.AddRange(clientModels);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, string.Empty);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
