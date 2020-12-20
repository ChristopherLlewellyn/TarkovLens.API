using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    public class Slots
    {
        [JsonPropertyName("muzzle_00")]
        public Slot Muzzle { get; set; }

        // TODO: add the rest of the slots
    }

    public class Slot
    {
        [JsonPropertyName("filter")]
        public Filter Filter { get; set; }

        [JsonPropertyName("required")]
        public bool Required { get; set; }
    }
}
