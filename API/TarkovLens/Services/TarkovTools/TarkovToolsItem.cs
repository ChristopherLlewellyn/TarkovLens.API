using System;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovTools
{
    /// <summary>
    /// Provides price and image data.
    /// </summary>
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

        [JsonProperty("lastLowPrice")]
        public int LastLowPrice { get; set; }

        [JsonProperty("changeLast48h")]
        public decimal ChangeLast48h { get; set; }

        [JsonProperty("low24hPrice")]
        public int Low24hPrice { get; set; }

        [JsonProperty("high24hPrice")]
        public int High24hPrice { get; set; }

        [JsonProperty("wikiLink")]
        public string WikiLink { get; set; }
    }
}
