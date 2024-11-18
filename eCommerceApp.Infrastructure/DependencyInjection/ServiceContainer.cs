using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Repositories;
using eCommerceApp.Domain.Entities;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using eCommerceApp.Infrastructure.MiddleWare;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Infrastructure.Services;

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

            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepository<Product>> ();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            return services;
        }

        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
