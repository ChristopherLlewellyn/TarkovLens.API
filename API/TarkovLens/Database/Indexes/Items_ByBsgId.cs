using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Characters;
using TarkovLens.Documents.Items;

namespace TarkovLens.Indexes
{
    public class Items_ByBsgId : AbstractMultiMapIndexCreationTask
    {
        public Items_ByBsgId()
        {
            AddMap<Ammunition>(collection => from document in collection select new { document.BsgId });

            AddMap<Armor>(collection => from document in collection select new { document.BsgId });

            AddMap<Backpack>(collection => from document in collection select new { document.BsgId });

            AddMap<Barter>(collection => from document in collection select new { document.BsgId });

            AddMap<Clothing>(collection => from document in collection select new { document.BsgId });

            AddMap<Common>(collection => from document in collection select new { document.BsgId });

            AddMap<Container>(collection => from document in collection select new { document.BsgId });

            AddMap<Firearm>(collection => from document in collection select new { document.BsgId });

            AddMap<Food>(collection => from document in collection select new { document.BsgId });

            AddMap<Grenade>(collection => from document in collection select new { document.BsgId });

            AddMap<Headphone>(collection => from document in collection select new { document.BsgId });

            AddMap<Key>(collection => from document in collection select new { document.BsgId });

            AddMap<Magazine>(collection => from document in collection select new { document.BsgId });

            AddMap<Map>(collection => from document in collection select new { document.BsgId });

            AddMap<Medical>(collection => from document in collection select new { document.BsgId });

            AddMap<Melee>(collection => from document in collection select new { document.BsgId });

            AddMap<Modification>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationBarrel>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationBipod>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationCharge>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationDevice>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationForegrip>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationGasblock>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationGoggles>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationHandguard>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationLauncher>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationMount>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationMuzzle>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationPistolgrip>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationReceiver>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationSight>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationSightSpecial>(collection => from document in collection select new { document.BsgId });

            AddMap<ModificationStock>(collection => from document in collection select new { document.BsgId });

            AddMap<Money>(collection => from document in collection select new { document.BsgId });

            AddMap<Tacticalrig>(collection => from document in collection select new { document.BsgId });
        }
    }
}
