using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class ModificationMuzzle : BaseModification
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("velocity")]
        public float Velocity { get; set; }
    }
}
