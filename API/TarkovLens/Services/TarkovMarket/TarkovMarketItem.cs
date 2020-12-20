using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Services.TarkovMarket
{
    public class TarkovMarketItem
    {
        #region Properties
        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Last lowest price
        /// </summary>
        [JsonPropertyName("price")]
        public int Price { get; set; }

        /// <summary>
        /// Average price over the last 24 hours
        /// </summary>
        [JsonPropertyName("avg24hPrice")]
        public int Avg24hPrice { get; set; }

        /// <summary>
        /// Average price over the last 7 days
        /// </summary>
        [JsonPropertyName("avg7daysPrice")]
        public int Avg7daysPrice { get; set; }

        /// <summary>
        /// The name of the trader who offers the best buyback price
        /// </summary>
        [JsonPropertyName("traderName")]
        public string TraderName { get; set; }

        /// <summary>
        /// The best buyback price offered by a trader
        /// </summary>
        [JsonPropertyName("traderPrice")]
        public int TraderPrice { get; set; }

        /// <summary>
        /// The currency the best buyback price is offered in.
        /// </summary>
        [JsonPropertyName("traderPriceCur")]
        public string TraderPriceCur { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Number of slots this item takes up
        /// </summary>
        [JsonPropertyName("slots")]
        public int Slots { get; set; }

        /// <summary>
        /// Price shift percentage over the last 24 hours
        /// </summary>
        [JsonPropertyName("diff24h")]
        public double Diff24h { get; set; }

        /// <summary>
        /// Price shift percentage over the last 7 days
        /// </summary>
        [JsonPropertyName("diff7days")]
        public double Diff7days { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("wikiLink")]
        public string WikiLink { get; set; }

        /// <summary>
        /// Image of the item as it would appear in the stash or in a menu
        /// </summary>
        [JsonPropertyName("img")]
        public string Img { get; set; }

        /// <summary>
        /// Image of the item's model
        /// </summary>
        [JsonPropertyName("imgBig")]
        public string ImgBig { get; set; }

        [JsonPropertyName("bsgId")]
        public string BsgId { get; set; }

        [JsonPropertyName("isFunctional")]
        public bool IsFunctional { get; set; }
        #endregion
    }
}
