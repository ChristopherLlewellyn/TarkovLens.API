using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Interfaces;

namespace TarkovLens.Services.TarkovDatabase
{
    public class GetItemsByKindResponse<T> where T : IItem
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
    }
}
