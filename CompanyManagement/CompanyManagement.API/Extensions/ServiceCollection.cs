using CompanyManagement.API.Repositories;
using CompanyManagement.API.Repositories.Client;
using CompanyManagement.API.Repositories.Service;
using CompanyManagement.API.Services.Client;
using CompanyManagement.API.Services.Service;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.API.Extensions
{
    public static class ServiceCollection
    {

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IServiceService, ServiceService>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(connectionString));
        }
    }
}
