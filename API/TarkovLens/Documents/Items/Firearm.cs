using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Enums.Services.TarkovDatabase;

namespace TarkovLens.Documents.Items
{
    public class Firearm : IItem
    {
        #region IItem fields
        public string Id { get; set; }

        [JsonPropertyName("_id")]
        public string BsgId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("_kind")]
        public KindOfItem KindOfItem { get; set; }

        [JsonPropertyName("weight")]
        public float Weight { get; set; }
        #endregion

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("caliber")]
        public string Caliber { get; set; }

        [JsonPropertyName("ergonomics")]
        public float Ergonomics { get; set; }

        [JsonPropertyName("recoilVertical")]
        public float RecoilVertical { get; set; }

        [JsonPropertyName("recoilHorizontal")]
        public float RecoilHorizontal { get; set; }

        [JsonPropertyName("velocity")]
        public float Velocity { get; set; }

        [JsonPropertyName("modes")]
        public string[] Modes { get; set; }

        [JsonPropertyName("rof")]
        public int RateOfFire { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("effectiveDist")]
        public float EffectiveDistance { get; set; }
    }
}
