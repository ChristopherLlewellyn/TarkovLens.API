using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Interfaces;

namespace TarkovLens.Documents.Characters
{
    public class Trader : ICharacter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public CharacterType Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("portrait")]
        public string Portrait { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
