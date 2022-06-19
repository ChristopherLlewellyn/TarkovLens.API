using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    /// <summary>
    /// The price a trader will give you if you sell the item to them.
    /// </summary>
    public class TraderPrice
    {
        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("trader")]
        public Trader Trader { get; set; }
    }

    public class Trader
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
