using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlatformService.Database;
using PlatformService.Database.Contexts;
using PlatformService.Implementation.Interfaces;
using PlatformService.Implementation.Repositories;
using PlatformService.Implementation.SyncDataServices.Http;
using System;

namespace PlatformService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_environment.IsProduction())
            {
                Console.WriteLine("---> Initialize Sql Server usage");
                services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("PlatformsDb")));
            }
            else
            {
                Console.WriteLine("---> Initialize InMemory Database");
                services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("InMemory"));
            }

            services.AddScoped<IPlatformRepository, PlatformRepository>();

            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepopulateDatabase.Prepopulate(app, _environment.IsProduction());
        }
    }
}
