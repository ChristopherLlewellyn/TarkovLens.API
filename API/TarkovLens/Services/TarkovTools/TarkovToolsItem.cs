using System;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovTools
{
    public class TarkovToolsItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconLink")]
        public string IconLink { get; set; }

        [JsonProperty("imageLink")]
        public string ImageLink { get; set; }

        [JsonProperty("avg24hPrice")]
        public int Avg24hPrice { get; set; }
    }
}
