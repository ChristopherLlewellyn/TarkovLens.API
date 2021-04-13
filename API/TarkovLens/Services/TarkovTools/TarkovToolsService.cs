using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace TarkovLens.Services.TarkovTools
{
    public interface ITarkovToolsService
    {
        public Task GetItemsByKindAsync();
    }

    public class TarkovToolsService : ITarkovToolsService
    {
        private IGraphQLClient _tarkovToolsClient;

        public TarkovToolsService(IGraphQLClient tarkovToolsClient)
        {
            _tarkovToolsClient = tarkovToolsClient;
        }

        public async Task GetItemsByKindAsync()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                {
                    itemsByType(type: ammo)
                    {
                        id,
                        name,
                        iconLink,
                        imageLink,
                        avg24hPrice
                    }
                }"
            };

            var response = await _tarkovToolsClient.SendQueryAsync<TarkovToolsItem>(request);
            throw new NotImplementedException();
        }
    }
}
