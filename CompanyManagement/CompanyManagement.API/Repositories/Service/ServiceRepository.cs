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

        /// <inheritdoc/>
        public async Task<(int statusCode, ServiceModel? service)> GetAsync(string id)
        {
            try
            {
                return (StatusCodes.Status200OK, _databaseContext.Services.FirstOrDefault(s => s.Id == id));
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, null);
            }
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, ServiceModel? updatedService)> UpdateAsync(ServiceModel serviceModel)
        {
            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Update(serviceModel);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, serviceModel);
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
            var serviceResult = await GetAsync(id);

            if (serviceResult.service == null) return StatusCodes.Status204NoContent;

            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Remove(serviceResult.service);

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
