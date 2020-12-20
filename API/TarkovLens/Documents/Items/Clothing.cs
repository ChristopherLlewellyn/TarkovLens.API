using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Documents.Items
{
    public class Clothing : BaseItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("blocking")]
        public string[] Blocking { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }
    }
}
