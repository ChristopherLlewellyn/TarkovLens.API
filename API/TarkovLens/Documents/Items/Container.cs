using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Container : BaseItem
    {
        public Grids Grids { get; set; }
    }

    public class Grids
    {
        public string Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string MaxWeight { get; set; }
        public Filter Filter { get; set; }
    }
}
