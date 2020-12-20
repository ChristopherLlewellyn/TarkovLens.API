using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Magazine : BaseModification
    {
        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("caliber")]
        public string Caliber { get; set; }

        [JsonPropertyName("modifier")]
        public ReloadModifier ReloadModifier { get; set; }
    }

    public class ReloadModifier
    {
        [JsonPropertyName("checkTime")]
        public int CheckTime { get; set; }

        [JsonPropertyName("loadUnload")]
        public int LoadUnload { get; set; }
    }
}
