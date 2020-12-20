using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class ModificationSight : BaseModification
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("magnification")]
        public List<string> Magnification { get; set; }

        [JsonPropertyName("variableZoom")]
        public bool VariableZoom { get; set; }

        [JsonPropertyName("zeroDistances")]
        public List<int> ZeroDistances { get; set; }
    }
}
