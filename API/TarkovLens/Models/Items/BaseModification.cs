using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    public class BaseModification : BaseItem
    {
        [JsonPropertyName("accuracy")]
        public int Accuracy { get; set; }

        [JsonPropertyName("recoil")]
        public int Recoil { get; set; }

        [JsonPropertyName("ergonomicsFP")]
        public int ErgonomicsFP { get; set; }

        [JsonPropertyName("ergonomics")]
        public int Ergonomics { get; set; }

        [JsonPropertyName("raidModdable")]
        public int RaidModdable { get; set; }

        [JsonPropertyName("gridModifier")]
        public GridModifier GridModifier { get; set; }

        [JsonPropertyName("slots")]
        public Slots Slots { get; set; }

        [JsonPropertyName("compatibility")]
        public Filter Compatibility { get; set; }

        [JsonPropertyName("conflicts")]
        public Filter Conflicts { get; set; }
    }
}
