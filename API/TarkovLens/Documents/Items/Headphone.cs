using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Headphone : BaseItem
    {
        [JsonPropertyName("ambientVol")]
        public int AmbientVol { get; set; }

        [JsonPropertyName("dryVol")]
        public int DryVol { get; set; }

        [JsonPropertyName("distortion")]
        public float Distortion { get; set; }

        [JsonPropertyName("hpf")]
        public Hpf Hpf { get; set; }

        [JsonPropertyName("compressor")]
        public Compressor Compressor { get; set; }
    }

    public class Hpf
    {
        [JsonPropertyName("cutoffFreq")]
        public int CutoffFreq { get; set; }

        [JsonPropertyName("resonance")]
        public float Resonance { get; set; }
    }

    public class Compressor
    {
        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("gain")]
        public int Gain { get; set; }

        [JsonPropertyName("release")]
        public int Release { get; set; }

        [JsonPropertyName("treshhold")]
        public int Threshold { get; set; }

        [JsonPropertyName("volume")]
        public int Volume { get; set; }
    }
}
