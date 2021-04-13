using System;
using TarkovLens.Enums;

namespace TarkovLens.Interfaces
{
    public interface IItem
    {
        #region Properties
        public string Id { get; set; }
        public string BlightbusterIcon { get; }

        #region Tarkov-Database fields shared between all items
        public string BsgId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public KindOfItem KindOfItem { get; set; }
        public float Weight { get; set; }
        public int BasePrice { get; set; }
        public int MaxStack { get; set; }
        #endregion

        #region Price and Image data
        public int Avg24hPrice { get; set; }
        public string WikiLink { get; set; }
        public string Icon { get; set; }
        public string Img { get; set; }
        public string ImgBig { get; set; }
        #endregion

        #endregion

        #region Methods
        public void CopyFrom<T>(T other) where T : IItem;
        #endregion
    }
}
