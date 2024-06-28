using Application.Interfaces;
using Application.Mappings;
using Application.Service;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var server = configuration["srv-captain--prod-db"] ?? "localhost";
        var port = configuration["SRV_DB_PORT"] ?? "5432";
        var user = configuration["SRV_DB_USER"] ?? "postgres";
        var password = configuration["SRV_DB_PWD"] ?? "1232";
        var database = configuration["SRV_DB_NAME"] ?? "local_dev";

        // var connectionString = $"Server={server};Port={port};Database={database};User Id={user};Password={password};";
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        var handlers = AppDomain.CurrentDomain.Load("Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(handlers));
        
        services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
        return services;
    }
}