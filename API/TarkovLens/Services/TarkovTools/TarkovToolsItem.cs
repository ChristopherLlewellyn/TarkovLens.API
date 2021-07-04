using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovTools
{
    /// <summary>
    /// Provides price and image data.
    /// </summary>
    public class TarkovToolsItem
    {
        [JsonProperty("id")]
        public string BsgId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconLink")]
        public string IconLink { get; set; }

        [JsonProperty("imageLink")]
        public string ImageLink { get; set; }

        [JsonProperty("gridImageLink")]
        public string GridImageLink { get; set; }

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

        [JsonProperty("traderPrices")]
        public IEnumerable<SellToTraderPrice> SellToTraderPrices { get; set; }
    }

    /// <summary>
    /// The price a trader will give you if you sell the item to them.
    /// </summary>
    public class SellToTraderPrice
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("trader")]
        public Trader Trader { get; set; }
    }

    public class Trader
    {
        [JsonProperty("id")]
        public string BsgId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
