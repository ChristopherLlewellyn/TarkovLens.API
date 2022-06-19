﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Interfaces;

namespace TarkovLens.Models.Items
{
    public class BaseItem : IItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// This may not actually link to an image. Certain items (ones that can be modified, e.g. Altyn) will not have an image.
        /// </summary>
        [JsonPropertyName("blightbusterIcon")]
        public string BlightbusterIcon => $"https://raw.githubusercontent.com/Blightbuster/EfTIcons/master/uid/{BsgId}.png";


        /// Tarkov-Database fields shared between all items

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

        [JsonPropertyName("rarity")]
        public Rarity Rarity { get; set; }

        [JsonPropertyName("grid")]
        public Grid Grid { get; set; }


        /// Price and Image data

        [JsonPropertyName("avg24hPrice")]
        public int Avg24hPrice { get; set; }

        [JsonPropertyName("lastLowPrice")]
        public int LastLowPrice { get; set; }

        [JsonPropertyName("changeLast48h")]
        public decimal ChangeLast48h { get; set; }

        [JsonPropertyName("changeLast48hPercent")]
        public decimal ChangeLast48hPercent { get; set; }

        [JsonPropertyName("low24hPrice")]
        public int Low24hPrice { get; set; }

        [JsonPropertyName("high24hPrice")]
        public int High24hPrice { get; set; }

        [JsonPropertyName("wikiLink")]
        public string WikiLink { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("imgBig")]
        public string ImgBig { get; set; }

        [JsonPropertyName("gridImg")]
        public string GridImg { get; set; }

        [JsonPropertyName("sellToTraderPrices")]
        public List<TraderPrice> SellToTraderPrices { get; set; }

        [JsonPropertyName("buyFromTraderPrices")]
        public List<TraderPrice> BuyFromTraderPrices { get; set; }


        public virtual void CopyFrom<T>(T other) where T : IItem
        {
            var props = typeof(T)
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
