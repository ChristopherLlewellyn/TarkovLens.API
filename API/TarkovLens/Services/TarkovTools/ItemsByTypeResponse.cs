using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Services.TarkovTools
{
    public class ItemsByTypeResponse
    {
        [JsonProperty("itemsByType")]
        public List<TarkovToolsItem> ItemsByType { get; set; }
    }
}
