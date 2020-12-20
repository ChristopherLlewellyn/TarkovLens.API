using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Ammunition : BaseItem
    {
        [JsonPropertyName("caliber")]
        public string Caliber { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tracer")]
        public bool Tracer { get; set; }

        [JsonPropertyName("tracerColor")]
        public string TracerColor { get; set; }

        [JsonPropertyName("subsonic")]
        public bool Subsonic { get; set; }

        [JsonPropertyName("velocity")]
        public float Velocity { get; set; }

        [JsonPropertyName("ballisticCoef")]
        public float BallisticCoefficient { get; set; }

        [JsonPropertyName("damage")]
        public float Damage { get; set; }

        [JsonPropertyName("penetration")]
        public float Penetration { get; set; }

        [JsonPropertyName("armorDamage")]
        public float ArmorDamage { get; set; }

        [JsonPropertyName("fragmentation")]
        public Fragmentation Fragmentation { get; set; }

        [JsonPropertyName("projectiles")]
        public int Projectiles { get; set; }

        [JsonPropertyName("weaponModifier")]
        public WeaponModifier WeaponModifier { get; set; }
    }

    public class Fragmentation
    {
        [JsonPropertyName("chance")]
        public float Chance { get; set; }

        [JsonPropertyName("min")]
        public int Min { get; set; }

        [JsonPropertyName("max")]
        public float Max { get; set; }
    }

    public class WeaponModifier
    {
        [JsonPropertyName("accuracy")]
        public float Accuracy { get; set; }

        [JsonPropertyName("recoil")]
        public float Recoil { get; set; }
    }
}
