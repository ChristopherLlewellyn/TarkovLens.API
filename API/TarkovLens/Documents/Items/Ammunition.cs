using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Documents.Items
{
    public class Ammunition : IItem
    {
        public string Id { get; set; }
        public string BlightbusterIcon => $"https://raw.githubusercontent.com/Blightbuster/EfTIcons/master/uid/{BsgId}.png";

        #region Tarkov-Database fields shared between all items

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

        [JsonPropertyName("price")]
        public int BasePrice { get; set; }

        [JsonPropertyName("maxStack")]
        public int MaxStack { get; set; }
        #endregion

        #region Tarkov-Market fields
        public int LastLowestMarketPrice { get; set; }
        public int Avg24hPrice { get; set; }
        public int Avg7daysPrice { get; set; }
        public DateTime Updated { get; set; }
        public double Diff24h { get; set; }
        public double Diff7days { get; set; }
        public string Icon { get; set; }
        public string WikiLink { get; set; }
        public string Img { get; set; }
        public string ImgBig { get; set; }
        #endregion

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

        public virtual void CopyFrom<T>(T other) where T : IItem
        {
            var props = typeof(Ammunition)
                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanWrite);
            foreach (var prop in props)
            {
                var source = prop.GetValue(other);
                prop.SetValue(this, source);
            }
        }
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
