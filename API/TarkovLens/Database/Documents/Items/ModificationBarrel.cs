using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class ModificationBarrel : BaseModification
    {
        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("velocity")]
        public float Velocity { get; set; }

        [JsonPropertyName("suppressor")]
        public bool Suppressor { get; set; }
    }
}
