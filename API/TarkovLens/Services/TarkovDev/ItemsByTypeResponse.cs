using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Services.TarkovDev
{
    public class ItemsByTypeResponse
    {
        [JsonProperty("items")]
        public List<TarkovDevItem> Items { get; set; }
    }
}
