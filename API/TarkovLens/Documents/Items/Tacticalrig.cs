using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Tacticalrig : BaseItem
    {
        [JsonPropertyName("grids")]
        public List<StorageGrid> Grids { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }

        [JsonPropertyName("armor")]
        public ArmorProperties Armor { get; set; }
    }
}
