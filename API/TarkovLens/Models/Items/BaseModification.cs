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
        public float Recoil { get; set; }

        [JsonPropertyName("ergonomicsFP")]
        public float ErgonomicsFP { get; set; }

        [JsonPropertyName("ergonomics")]
        public int Ergonomics { get; set; }

        /// <summary>
        /// 1 means yes, 2 means no
        /// </summary>
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
