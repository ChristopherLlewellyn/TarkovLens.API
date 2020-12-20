using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class Key : BaseItem
    {
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
