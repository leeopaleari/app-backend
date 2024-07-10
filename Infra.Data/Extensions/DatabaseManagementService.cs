using Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data.Extensions;

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            
            serviceDb.Database.Migrate();
        }
    }
}