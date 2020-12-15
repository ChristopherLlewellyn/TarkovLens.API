using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Services;

namespace TarkovLens
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            #region Database (RavenDb)
            RavenSettings ravenSettings = new RavenSettings();
            Configuration.Bind(ravenSettings);

            DocumentStore store = new DocumentStore
            {
                Urls = ravenSettings.Database.Urls,
                Database = ravenSettings.Database.DatabaseName,
            };

            store.Initialize();

            services.AddSingleton<IDocumentStore>(store);

            services.AddScoped<IDocumentSession>(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenSession();
            });
            #endregion

            #region Configure strongly typed settings objects
            // This should be stored in a secrets.json file. Right click the project in solution explorer
            // and select "Manage User Secrets" to create this file. Use secrets.example.json as a template.
            var secretsSection = Configuration.GetSection("Secrets");
            services.Configure<Secrets>(secretsSection);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            #endregion

            #region Configure DI for application services
            //services.AddScoped<ITarkovDatabaseService, TarkovDatabaseService>(); commented as AddHttpClient registers this service
            //services.AddScoped<ITarkovMarketService, TarkovMarketService>(); commented as AddHttpClient registers this service
            #endregion

            #region HTTP Clients
            services.AddHttpClient<ITarkovDatabaseService, TarkovDatabaseService>();
            services.AddHttpClient<ITarkovMarketService, TarkovMarketService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
