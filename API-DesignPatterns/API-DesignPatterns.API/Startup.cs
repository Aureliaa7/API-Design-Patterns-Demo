using API_DesignPatterns.API.Filters;
using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.Interfaces;
using API_DesignPatterns.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API_DesignPatterns.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
         
            services.RegisterDbContext(Configuration.GetConnectionString("DefaultConnection"));

            services.RegisterServices();

            services.ConfigureGlobalFilters();

            services.RegisterFilters();

            services.AddAutoMapper(typeof(MappingConfigurations));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
