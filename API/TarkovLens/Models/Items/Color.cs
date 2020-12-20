using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    public class Color
    {
        [JsonPropertyName("r")]
        public int R { get; set; }

        [JsonPropertyName("g")]
        public int G { get; set; }

        [JsonPropertyName("b")]
        public int B { get; set; }

        [JsonPropertyName("a")]
        public int A { get; set; }
    }
}
