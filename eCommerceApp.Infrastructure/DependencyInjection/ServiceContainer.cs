using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Repositories;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService
            (this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "Defualt";

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(connectionString),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();

            }),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepository<Product>> ();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

            return services;
        }
    }
}
