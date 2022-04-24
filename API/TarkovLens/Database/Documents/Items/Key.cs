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
        /// <summary>
        /// The value here from Tarkov Database seems to always be "UNIMPLEMENTED", so isn't currently useful.
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("usage")]
        public List<string> Usage { get; set; }

        [JsonPropertyName("maps")]
        public List<string> Maps { get; set; }

        public override void CopyFrom<T>(T other)
        {
            // Temporarily store the values of properties that we don't want to overwrite
            var usage_temp = Usage.ToList();
            var maps_temp = Maps.ToList();

            // Copy values
            var props = typeof(T)
                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanWrite);
            foreach (var prop in props)
            {
                var source = prop.GetValue(other);
                prop.SetValue(this, source);
            }

            // Reinstate the values of properties we don't want to overwrite
            Usage = usage_temp;
            Maps = maps_temp;
        }
    }
}
