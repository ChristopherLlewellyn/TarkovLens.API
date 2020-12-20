using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TarkovLens.Models.Items
{
    public class Filter
    {
        [JsonPropertyName("ammunition")]
        public List<string> AmmunitionIds { get; set; }

        [JsonPropertyName("armor")]
        public List<string> ArmorIds { get; set; }

        [JsonPropertyName("barter")]
        public List<string> BarterIds { get; set; }

        [JsonPropertyName("clothing")]
        public List<string> ClothingIds { get; set; }

        [JsonPropertyName("common")]
        public List<string> CommonIds { get; set; }

        [JsonPropertyName("container")]
        public List<string> ContainerIds { get; set; }

        [JsonPropertyName("food")]
        public List<string> FoodIds { get; set; }

        [JsonPropertyName("grenade")]
        public List<string> GrenadeIds { get; set; }

        [JsonPropertyName("headphone")]
        public List<string> HeadphoneIds { get; set; }

        [JsonPropertyName("key")]
        public List<string> KeyIds { get; set; }

        [JsonPropertyName("magazine")]
        public List<string> MagazineIds { get; set; }

        [JsonPropertyName("map")]
        public List<string> MapIds { get; set; }

        [JsonPropertyName("medical")]
        public List<string> MedicalIds { get; set; }

        [JsonPropertyName("melee")]
        public List<string> MeleeIds { get; set; }

        [JsonPropertyName("modification")]
        public List<string> ModificationIds { get; set; }

        [JsonPropertyName("modificationBarrel")]
        public List<string> ModificationBarrelIds { get; set; }

        [JsonPropertyName("modificationBipod")]
        public List<string> ModificationBipodIds { get; set; }

        [JsonPropertyName("modificationCharge")]
        public List<string> ModificationChargeIds { get; set; }

        [JsonPropertyName("modificationDevice")]
        public List<string> ModificationDeviceIds { get; set; }

        [JsonPropertyName("modificationForegrip")]
        public List<string> ModificationForegripIds { get; set; }

        [JsonPropertyName("modificationGasblock")]
        public List<string> ModificationGasblockIds { get; set; }

        [JsonPropertyName("modificationGoggles")]
        public List<string> ModificationGogglesIds { get; set; }

        [JsonPropertyName("modificationHandguard")]
        public List<string> ModificationHandguardIds { get; set; }

        [JsonPropertyName("modificationLauncher")]
        public List<string> ModificationLauncherIds { get; set; }

        [JsonPropertyName("modificationMount")]
        public List<string> ModificationMountIds { get; set; }

        [JsonPropertyName("modificationMuzzle")]
        public List<string> ModificationMuzzleIds { get; set; }

        [JsonPropertyName("modificationPistolgrip")]
        public List<string> ModificationPistolgripIds { get; set; }

        [JsonPropertyName("modificationReceiver")]
        public List<string> ModificationReceiverIds { get; set; }

        [JsonPropertyName("modificationSight")]
        public List<string> ModificationSightIds { get; set; }

        [JsonPropertyName("modificationSightSpecial")]
        public List<string> ModificationSightSpecialIds { get; set; }

        [JsonPropertyName("modificationStock")]
        public List<string> ModificationStockIds { get; set; }

        [JsonPropertyName("money")]
        public List<string> Money { get; set; }
    }
}
