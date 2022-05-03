using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Food : BaseItem
    {
        [JsonPropertyName("type")]
        public FoodType Type { get; set; }

        [JsonPropertyName("resources")]
        public int Resources { get; set; }

        [JsonPropertyName("useTime")]
        public int UseTime { get; set; }

        [JsonPropertyName("effects")]
        public Effects Effects { get; set; }
    }
}
