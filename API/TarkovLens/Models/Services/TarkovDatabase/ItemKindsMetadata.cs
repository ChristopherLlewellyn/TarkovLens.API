using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Enums.Services.TarkovDatabase
{
    // ItemKindsResponse itemKindsResponse = JsonSerializer.Deserialize<ItemKindsResponse>(myJsonResponse);
    public class ItemKindsMetadata
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("modified")]
        public int Modified { get; set; }

        [JsonPropertyName("kinds")]
        public KindsMetadata KindsMetadata { get; set; }
    }

    #region Item kinds
    public class KindsMetadata
    {
        [JsonPropertyName("ammunition")]
        public Metadata Ammunition { get; set;  }

        [JsonPropertyName("armor")]
        public Metadata Armor { get; set; }

        [JsonPropertyName("backpack")]
        public Metadata Backpack { get; set; }

        [JsonPropertyName("barter")]
        public Metadata Barter { get; set; }

        [JsonPropertyName("clothing")]
        public Metadata Clothing { get; set; }

        [JsonPropertyName("common")]
        public Metadata Common { get; set; }

        [JsonPropertyName("container")]
        public Metadata Container { get; set; }

        [JsonPropertyName("firearm")]
        public Metadata Firearm { get; set; }

        [JsonPropertyName("food")]
        public Metadata Food { get; set; }

        [JsonPropertyName("grenade")]
        public Metadata Grenade { get; set; }

        [JsonPropertyName("headphone")]
        public Metadata Headphone { get; set; }

        [JsonPropertyName("key")]
        public Metadata Key { get; set; }

        [JsonPropertyName("magazine")]
        public Metadata Magazine { get; set; }

        [JsonPropertyName("map")]
        public Metadata Map { get; set; }

        [JsonPropertyName("medical")]
        public Metadata Medical { get; set; }

        [JsonPropertyName("melee")]
        public Metadata Melee { get; set; }

        [JsonPropertyName("modification")]
        public Metadata Modification { get; set; }

        [JsonPropertyName("modificationBarrel")]
        public Metadata ModificationBarrel { get; set; }

        [JsonPropertyName("modificationBipod")]
        public Metadata ModificationBipod { get; set; }

        [JsonPropertyName("modificationCharge")]
        public Metadata ModificationCharge { get; set; }

        [JsonPropertyName("modificationDevice")]
        public Metadata ModificationDevice { get; set; }

        [JsonPropertyName("modificationForegrip")]
        public Metadata ModificationForegrip { get; set; }

        [JsonPropertyName("modificationGasblock")]
        public Metadata ModificationGasblock { get; set; }

        [JsonPropertyName("modificationGoggles")]
        public Metadata ModificationGoggles { get; set; }

        [JsonPropertyName("modificationHandguard")]
        public Metadata ModificationHandguard { get; set; }

        [JsonPropertyName("modificationLauncher")]
        public Metadata ModificationLauncher { get; set; }

        [JsonPropertyName("modificationMount")]
        public Metadata ModificationMount { get; set; }

        [JsonPropertyName("modificationMuzzle")]
        public Metadata ModificationMuzzle { get; set; }

        [JsonPropertyName("modificationPistolgrip")]
        public Metadata ModificationPistolgrip { get; set; }

        [JsonPropertyName("modificationReceiver")]
        public Metadata ModificationReceiver { get; set; }

        [JsonPropertyName("modificationSight")]
        public Metadata ModificationSight { get; set; }

        [JsonPropertyName("modificationSightSpecial")]
        public Metadata ModificationSightSpecial { get; set; }

        [JsonPropertyName("modificationStock")]
        public Metadata ModificationStock { get; set; }

        [JsonPropertyName("money")]
        public Metadata Money { get; set; }

        [JsonPropertyName("tacticalrig")]
        public Metadata Tacticalrig { get; set; }
    }
    #endregion

    public class Metadata
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("modified")]
        public int Modified { get; set; }
    }
}
