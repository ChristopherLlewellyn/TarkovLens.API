using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Enums.Services.TarkovDatabase;

namespace TarkovLens.Documents.Items
{
    public class Armor : IItem
    {
        #region IItem fields
        public string Id { get; set; }

        [JsonPropertyName("_id")]
        public string BsgId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("_kind")]
        public KindOfItem KindOfItem { get; set; }

        [JsonPropertyName("weight")]
        public float Weight { get; set; }

        [JsonPropertyName("price")]
        public int BasePrice { get; set; }

        [JsonPropertyName("maxStack")]
        public int MaxStack { get; set; }
        #endregion

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
