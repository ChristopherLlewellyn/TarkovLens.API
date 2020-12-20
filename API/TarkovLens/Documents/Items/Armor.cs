using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Enums.Services.TarkovDatabase;

namespace TarkovLens.Documents.Items
{
    public class Armor : BaseItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("armor")]
        public ArmorProperties ArmorProperties { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }

        [JsonPropertyName("blocking")]
        public string[] Blocking { get; set; }
    }
    
    public class ArmorProperties
    {
        [JsonPropertyName("class")]
        public int Class { get; set; }

        [JsonPropertyName("durability")]
        public float Durability { get; set; }

        [JsonPropertyName("material")]
        public Material Material { get; set; }

        [JsonPropertyName("bluntThroughput")]
        public float BluntThroughput { get; set; }

        [JsonPropertyName("zones")]
        public string[] Zones { get; set; }
    }

    public class Material
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("destructability")]
        public float Destructability { get; set; }
    }

    public class Penalties
    {
        [JsonPropertyName("mouse")]
        public float Mouse { get; set; }

        [JsonPropertyName("speed")]
        public float Speed { get; set; }

        [JsonPropertyName("ergonomics")]
        public int Ergonomics { get; set; }

        [JsonPropertyName("deafness")]
        public string Deafness { get; set; }
    }
}
