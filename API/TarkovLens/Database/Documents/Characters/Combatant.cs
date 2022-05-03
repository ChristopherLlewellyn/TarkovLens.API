using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Enums;
using TarkovLens.Interfaces;
using TarkovLens.Models.Characters;

namespace TarkovLens.Documents.Characters
{
    public class Combatant : ICharacter, ICombatant
    {
        public Combatant(
            CharacterType type,
            string name,
            string nickname,
            string description,
            string portrait,
            Location location,
            HealthStatus healthStatus)
        {
            Type = type;
            Name = name;
            Nickname = nickname;
            Description = description;
            Portrait = portrait;
            Location = location;
            HealthStatus = healthStatus;
        }

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

        [JsonPropertyName("healthStatus")]
        public HealthStatus HealthStatus { get; set; }
    }
}
