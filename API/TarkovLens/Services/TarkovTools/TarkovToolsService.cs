using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovTools
{
    public interface ITarkovToolsService
    {
        public Task<List<TarkovToolsItem>> GetItemsByTypeAsync(ItemType type, GraphQLHttpClient tarkovToolsClient = null);
    }

    public class TarkovToolsService : ITarkovToolsService
    {
        private readonly AppSettings _appSettings;

        public TarkovToolsService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Makes many (~15) HTTP requests - use sparingly.
        /// </summary>
        public async Task<List<TarkovToolsItem>> GetAllItemsAsync()
        {
            var tarkovToolsClient = new GraphQLHttpClient(_appSettings.TarkovToolsGQLUrl, new NewtonsoftJsonSerializer());

            var allItems = new List<TarkovToolsItem>();
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                var items = await GetItemsByTypeAsync(type, tarkovToolsClient);
                allItems.AddRange(items);
            }

            tarkovToolsClient.Dispose();
            return allItems;
        }

        public async Task<List<TarkovToolsItem>> GetItemsByTypeAsync(ItemType type, GraphQLHttpClient tarkovToolsClient = null)
        {
            if (tarkovToolsClient == null)
            {
                tarkovToolsClient = new GraphQLHttpClient(_appSettings.TarkovToolsGQLUrl, new NewtonsoftJsonSerializer(
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    ));
            }

            var request = new GraphQLRequest
            {
                Query = @"query ($type: ItemType!)
                {
                    itemsByType(type: $type)
                    {
                        id
                        name
                        iconLink
                        imageLink
                        gridImageLink
                        avg24hPrice
                        lastLowPrice
                        changeLast48h
                        low24hPrice
                        high24hPrice
                        wikiLink
                        traderPrices {
                            price
                            trader {
                            id
                            name
                          }
                        }
                    }
                }",
                Variables = new { type = type.ToString() }
            };

            var response = await tarkovToolsClient.SendQueryAsync<ItemsByTypeResponse>(request);
            tarkovToolsClient.Dispose();

            return response.Data.ItemsByType;
        }
    }
}
