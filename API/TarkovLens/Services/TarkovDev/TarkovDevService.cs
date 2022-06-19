using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovDev
{
    public interface ITarkovDevService
    {
        public Task<List<TarkovDevItem>> GetAllItemsAsync();
        public Task<List<TarkovDevItem>> GetItemsByTypeAsync(ItemType type, GraphQLHttpClient gqlClient = null);
    }

    public class TarkovDevService : ITarkovDevService
    {
        private readonly AppSettings _appSettings;

        public TarkovDevService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<TarkovDevItem>> GetAllItemsAsync() => await GetItemsByTypeAsync(ItemType.any);

        public async Task<List<TarkovDevItem>> GetItemsByTypeAsync(ItemType type, GraphQLHttpClient tarkovDevClient = null)
        {
            if (tarkovDevClient == null)
            {
                tarkovDevClient = new GraphQLHttpClient(_appSettings.TarkovDevGQLUrl, new NewtonsoftJsonSerializer(
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    ));
            }

            var request = new GraphQLRequest
            {
                Query = @"query ($type: ItemType!)
                {
                    items(type: $type)
                    {
                        id
                        name
                        iconLink
                        imageLink
                        gridImageLink
                        avg24hPrice
                        lastLowPrice
                        changeLast48h
                      	changeLast48hPercent
                        low24hPrice
                        high24hPrice
                        wikiLink
                        sellFor {
                            price
                            currency
                            vendor {
                                name
                          	}
                        }
                      	buyFor {
                            price
                            currency
                            vendor {
                                name
                          	}
                        }
                    }
                }",
                Variables = new { type = type.ToString() }
            };

            var response = await tarkovDevClient.SendQueryAsync<ItemsByTypeResponse>(request);
            tarkovDevClient.Dispose();

            return response.Data.Items;
        }
    }
}
