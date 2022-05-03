using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using TarkovLens.Helpers;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Indexes;
using TarkovLens.Services;
using TarkovLens.Services.Item;
using TarkovLens.Services.TarkovDatabase;
using TarkovLens.Services.TarkovTools;

namespace TarkovLens
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            // Add appsettings, user secrets, environment variables
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Program>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => 
                    builder.WithOrigins(appSettings.AllowedHosts)
                    .SetIsOriginAllowedToAllowWildcardSubdomains());
            });

            #region Database (RavenDb)
            RavenSettings ravenSettings = new RavenSettings();
            Configuration.Bind(ravenSettings);

            X509Certificate2 certificate = new X509Certificate2();
            if (ravenSettings.Database.RawCert.IsNotNullOrEmpty() && ravenSettings.Database.RawCertKey.IsNotNullOrEmpty())
            {
                certificate = CertificateHelpers.CreateRavenCertificate(ravenSettings.Database.RawCert, ravenSettings.Database.RawCertKey);
            }
            else
            {
                certificate = new X509Certificate2(ravenSettings.Database.CertPath, ravenSettings.Database.CertPass);
            }

            DocumentStore store = new DocumentStore
            {
                Urls = ravenSettings.Database.Urls,
                Database = ravenSettings.Database.DatabaseName,
                Certificate = certificate
            };

            store.Initialize();

            services.AddSingleton<IDocumentStore>(store);

            services.AddScoped(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenSession();
            });

            // Register Indexes
            new Item_Smart_Search().Execute(store);
            new Items_ByName_ForAll().Execute(store);
            new Items_ByBsgId().Execute(store);
            new Characters_ByType().Execute(store);
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
            services.AddScoped<IItemUpdaterService, ItemUpdaterService>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ITarkovToolsService, TarkovToolsService>();
            #endregion

            #region HTTP Clients
            services.AddHttpClient<ITarkovDatabaseService, TarkovDatabaseService>();
            services.AddHttpClient<ITarkovMarketService, TarkovMarketService>();
            #endregion

            #region Hangfire automatic jobs
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IServiceProvider serviceProvider,
                              IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            #region Hangfire background jobs
            if (env.IsDevelopment())
            {
                app.UseHangfireDashboard("/hangfire");
            }
            app.UseHangfireServer(new BackgroundJobServerOptions // reduces CPU usage of the dashboard by reducing Hangfire's "heartbeat"
            {
                HeartbeatInterval = new System.TimeSpan(0, 1, 0),
                ServerCheckInterval = new System.TimeSpan(0, 1, 0),
                SchedulePollingInterval = new System.TimeSpan(0, 1, 0)
            });
            recurringJobManager.AddOrUpdate("Update Items",
                                            () => serviceProvider.GetService<IItemUpdaterService>().UpdateItemsTask(),
                                            "15 * * * *", TimeZoneInfo.Local);
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
