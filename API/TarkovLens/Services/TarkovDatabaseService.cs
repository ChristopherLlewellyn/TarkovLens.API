using Microsoft.Extensions.Options;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using TarkovLens.Models.Services.TarkovDatabase;

namespace TarkovLens.Services
{
    public interface ITarkovDatabaseService
    {
        Task<string> GetNewAuthToken();
        Task<ItemKindsMetadata> GetItemKindsMetadata(string token);
    }

    public class TarkovDatabaseService : ITarkovDatabaseService
    {
        private readonly IDocumentSession session;
        private readonly Secrets _secrets;
        private readonly AppSettings _appSettings;
        private readonly HttpClient httpClient;

        public TarkovDatabaseService(IDocumentSession documentSession, IOptions<Secrets> secrets,
                                     IOptions<AppSettings> appSettings, HttpClient client)
        {
            session = documentSession;
            _secrets = secrets.Value;
            _appSettings = appSettings.Value;

            client.BaseAddress = new Uri(_appSettings.TarkovDatabaseV2BaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", $"{_appSettings.AppName}/{_appSettings.Version}");
            httpClient = client;
        }

        public async Task<string> GetNewAuthToken()
        {
            // This method needs to be called before any other methods can be used
            // We need a JWT auth token in order to access the Tarkov-Database API
            // Tarkov-Database auth tokens last for a limited time - currently 30 minutes (15/12/2020)
            // A new token can be created using any of our previously existing tokens
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _secrets.TarkovDatabaseInitialAuthToken);

            var response = await httpClient.GetAsync("token");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            AuthToken newToken = JsonSerializer.Deserialize<AuthToken>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return newToken.Token;
        }

        public async Task<ItemKindsMetadata> GetItemKindsMetadata(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("item");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var kindsResponse = JsonSerializer.Deserialize<ItemKindsMetadata>(json);


            return kindsResponse;
        }
    }
}
