using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    public class GridModifier
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}
