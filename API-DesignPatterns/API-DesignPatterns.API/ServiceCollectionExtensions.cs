using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API_DesignPatterns.Infrastructure.DatabaseContext;
using API_DesignPatterns.Infrastructure.Repositories;
using API_DesignPatterns.Core.Interfaces.Repositories;
using API_DesignPatterns.Core.DomainServices;
using API_DesignPatterns.Core.Interfaces.DomainServices;

namespace API_DesignPatterns.API
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(
                  connectionString,
                  b => b.MigrationsAssembly("API-DesignPatterns.Infrastructure"))
            );
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
