using Microsoft.Extensions.Options;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Services.TarkovMarket;

namespace TarkovLens.Services
{
    public interface ITarkovMarketService
    {
        public Task<List<TarkovMarketItem>> GetAllItemsAsync();
    }

    public class TarkovMarketService : ITarkovMarketService
    {
        private readonly IDocumentSession session;
        private readonly Secrets _secrets;
        private readonly AppSettings _appSettings;
        private readonly HttpClient httpClient;

        public TarkovMarketService(IDocumentSession documentSession, IOptions<Secrets> secrets,
                                     IOptions<AppSettings> appSettings, HttpClient client)
        {
            session = documentSession;
            _secrets = secrets.Value;
            _appSettings = appSettings.Value;

            client.BaseAddress = new Uri(_appSettings.TarkovMarketV1BaseUrl);
            client.DefaultRequestHeaders.Add("X-API-KEY", _secrets.TarkovMarketAPIKey);
            client.DefaultRequestHeaders.Add("User-Agent", $"{_appSettings.AppName}/{_appSettings.Version}");
            httpClient = client;
        }

        public async Task<List<TarkovMarketItem>> GetAllItemsAsync()
        {
            var response = await httpClient.GetAsync("items/all");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            // Deserialize the response payload
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.PropertyNameCaseInsensitive = true;
            serializerOptions.Converters.Add(new JsonStringEnumConverter());

            var tarkovMarketItems = JsonSerializer.Deserialize<List<TarkovMarketItem>>(json, serializerOptions);

            return tarkovMarketItems;
        }
    }
}
