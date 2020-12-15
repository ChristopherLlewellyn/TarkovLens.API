using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Documents.Items
{
    public interface IItem
    {
        public string Id { get; set; }
        public string BsgId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public KindOfItem KindOfItem { get; set; }
        public float Weight { get; set; }
    }
}
