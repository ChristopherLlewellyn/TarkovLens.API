using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Documents.Items
{
    public class Clothing : IItem
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

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("blocking")]
        public string[] Blocking { get; set; }

        [JsonPropertyName("penalties")]
        public Penalties Penalties { get; set; }

        public virtual void CopyFrom<T>(T other) where T : IItem
        {
            var props = typeof(Clothing)
                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanWrite);
            foreach (var prop in props)
            {
                var source = prop.GetValue(other);
                prop.SetValue(this, source);
            }
        }
    }
}
