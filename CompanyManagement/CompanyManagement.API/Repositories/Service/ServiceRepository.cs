using CompanyManagement.API.EqualityComparers;
using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Service
{
    public class ServiceRepository : IServiceRepository
    {
        private DatabaseContext _databaseContext;

        public ServiceRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ServiceModel> createdServices)> CreateAsync(IEnumerable<ServiceModel> serviceModels)
        {
            using var dbContextTransaction = await _databaseContext.Database.BeginTransactionAsync();

            try
            {
                var newServices = serviceModels.Distinct(new ServiceEqualityComparer());

                newServices = GetUnexistedServices(newServices);

                _databaseContext.Services.AddRange(newServices);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, newServices);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ServiceModel>());
            }
        }

        private IEnumerable<ServiceModel> GetUnexistedServices(IEnumerable<ServiceModel> newServices)
        {
            var services = new List<ServiceModel>();

            foreach (var service in newServices)
            {
                if (_databaseContext.Services.FirstOrDefault(f => f.Name == service.Name) == null) services.Add(service);
            }

            return services;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<ServiceModel> services)> GetAsync()
        {
            try
            {
                return (StatusCodes.Status200OK, _databaseContext.Services);
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<ServiceModel>());
            }
        }
    }
}
