﻿using CompanyManagement.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ApplyDatabaseMigrations(this WebApplication webApplication)
        {
            using var serviceScope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();

            serviceScope?.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
        }
    }
}
