using System;
using System.Collections.Generic;
using TarkovLens.Enums;
using TarkovLens.Models.Items;

namespace TarkovLens.Interfaces
{
    public interface IItem
    {
        public string Id { get; set; }
        public string BlightbusterIcon { get; }


        /// Tarkov-Database fields shared between all items
        public string BsgId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public KindOfItem KindOfItem { get; set; }
        public float Weight { get; set; }
        public int BasePrice { get; set; }
        public int MaxStack { get; set; }


        /// Price and Image data
        public int Avg24hPrice { get; set; }
        public int LastLowPrice { get; set; }
        public decimal ChangeLast48h { get; set; }
        public int Low24hPrice { get; set; }
        public int High24hPrice { get; set; }
        public string WikiLink { get; set; }
        public string Icon { get; set; }
        public string Img { get; set; }
        public string ImgBig { get; set; }
        public string GridImg { get; set; }
        public List<SellToTraderPrice> SellToTraderPrices { get; set; }


        public void CopyFrom<T>(T other) where T : IItem;
    }
}
