using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API_DesignPatterns.Infrastructure.DatabaseContext;
using API_DesignPatterns.Infrastructure.Repositories;
using API_DesignPatterns.Core.Interfaces.Repositories;
using API_DesignPatterns.Core.Interfaces.DomainServices;
using API_DesignPatterns.Core.DomainServices;
using API_DesignPatterns.API.Filters;
using API_DesignPatterns.Core.DomainEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using API_DesignPatterns.Core.Interfaces.Services;
using API_DesignPatterns.Infrastructure.IdentityEntities;
using API_DesignPatterns.Infrastructure.Services;

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

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureGlobalFilters(this IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add(new CustomExceptionFilter());
            });
        }

        public static void RegisterFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidateEntityExistenceFilter<Author>>();
            services.AddScoped<ValidateEntityExistenceFilter<Book>>();

            services.AddScoped<ValidateNotSoftDeletedEntityFilter<Author>>();
            services.AddScoped<ValidateNotSoftDeletedEntityFilter<Book>>();

            services.AddScoped<ValidateSoftDeletedEntityFilter<Author>>();
            services.AddScoped<ValidateSoftDeletedEntityFilter<Book>>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }

        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // The token is going to be valid if: 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,  // the issuer is not the actual server that created the token
                    ValidateAudience = false, 
                    ValidateLifetime = true, // the token has not expired
                    ValidateIssuerSigningKey = true, // the signing key is valid and is trusted by the server 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("JWT").Value))    // the secret key that the server uses to generate the signature for JWT
                };
            });
        }
    }
}
