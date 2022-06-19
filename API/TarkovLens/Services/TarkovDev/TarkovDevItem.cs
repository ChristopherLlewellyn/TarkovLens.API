using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TarkovLens.Services.TarkovDev
{
    /// <summary>
    /// Provides price and image data.
    /// </summary>
    public class TarkovDevItem
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

        [JsonProperty("changeLast48hPercent")]
        public decimal ChangeLast48hPercent { get; set; }

        [JsonProperty("low24hPrice")]
        public int Low24hPrice { get; set; }

        [JsonProperty("high24hPrice")]
        public int High24hPrice { get; set; }

        [JsonProperty("wikiLink")]
        public string WikiLink { get; set; }

        [JsonProperty("sellFor")]
        public List<ItemPrice> SellFor { get; set; }

        [JsonProperty("buyFor")]
        public List<ItemPrice> BuyFor { get; set; }
    }

    /// <summary>
    /// The price a trader will give you if you sell the item to them.
    /// </summary>
    public class ItemPrice
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("vendor")]
        public Vendor Vendor { get; set; }
    }

    public class Vendor
    {

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
