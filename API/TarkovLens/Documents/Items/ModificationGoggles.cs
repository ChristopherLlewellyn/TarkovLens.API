using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class ModificationGoggles : BaseModification
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
