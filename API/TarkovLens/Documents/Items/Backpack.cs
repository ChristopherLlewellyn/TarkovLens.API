using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Backpack : BaseItem
    {
        [JsonPropertyName("grids")]
        public List<BackpackGrid> Grids { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }
    }

    public class BackpackGrid
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("maxWeight")]
        public float MaxWeight { get; set; }
    }
}

