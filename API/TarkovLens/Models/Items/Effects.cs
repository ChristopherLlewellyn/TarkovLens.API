using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;

namespace TarkovLens.Models.Items
{
    public class Effects
    {
        [JsonPropertyName("energy")]
        public EffectsDetails Energy { get; set; }

        [JsonPropertyName("hydration")]
        public EffectsDetails Hydration { get; set; }

        [JsonPropertyName("health")]
        public EffectsDetails Health { get; set; }

        [JsonPropertyName("bloodloss")]
        public EffectsDetails Bloodloss { get; set; }

        [JsonPropertyName("lightBleeding")]
        public EffectsDetails LightBleeding { get; set; }

        [JsonPropertyName("heavyBleeding")]
        public EffectsDetails HeavyBleeding { get; set; }

        [JsonPropertyName("fracture")]
        public EffectsDetails Fracture { get; set; }

        [JsonPropertyName("contusion")]
        public EffectsDetails Contusion { get; set; }

        [JsonPropertyName("toxication")]
        public EffectsDetails Toxication { get; set; }

        [JsonPropertyName("radExposure")]
        public EffectsDetails RadExposure { get; set; }

        [JsonPropertyName("pain")]
        public EffectsDetails Pain { get; set; }

        [JsonPropertyName("destroyedPart")]
        public EffectsDetails DestroyedPart { get; set; }
    }

    public class EffectsDetails
    {
        [JsonPropertyName("resourceCosts")]
        public int ResourceCosts { get; set; }

        [JsonPropertyName("fadeIn")]
        public int FadeIn { get; set; }

        [JsonPropertyName("fadeOut")]
        public int FadeOut { get; set; }

        [JsonPropertyName("chance")]
        public float Chance { get; set; }

        [JsonPropertyName("delay")]
        public int Delay { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("isPercent")]
        public bool isPercent { get; set; }

        [JsonPropertyName("removes")]
        public bool Removes { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }
    }
}
