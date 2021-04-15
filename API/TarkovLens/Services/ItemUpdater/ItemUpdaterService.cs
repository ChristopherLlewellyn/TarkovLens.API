using Hangfire;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Interfaces;
using TarkovLens.Services.Item;
using TarkovLens.Services.TarkovDatabase;
using TarkovLens.Services.TarkovMarket;
using TarkovLens.Services.TarkovTools;

namespace TarkovLens.Services
{
    public interface IItemUpdaterService
    {
        public Task UpdateItemsTask();
    }

    public class ItemUpdaterService : IItemUpdaterService
    {
        private readonly IDocumentSession session;
        private ITarkovDatabaseService _tarkovDatabaseService;
        private ITarkovToolsService _tarkovToolsService;
        private IItemService _itemService;

        public ItemUpdaterService(IDocumentSession documentSession,
                              ITarkovDatabaseService tarkovDatabaseService,
                              ITarkovToolsService tarkovToolsService,
                              IItemService itemService)
        {
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovToolsService = tarkovToolsService;
            _itemService = itemService;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task UpdateItemsTask()
        {
            string tarkovDatabaseToken = await _tarkovDatabaseService.GetNewAuthTokenAsync();

            // Check for updates
            var currentMetadata = session.Query<ItemKindsMetadata>().FirstOrDefault();
            var newMetadata = await _tarkovDatabaseService.GetItemKindsMetadataAsync(tarkovDatabaseToken);

            // Update items
            // If Tarkov-Database has modified its data, lets fetch it all and update our data
            if (currentMetadata.IsNull() || newMetadata.Modified > currentMetadata.Modified)
            {
                await UpdateAllItemAndPriceData(tarkovDatabaseToken);

                newMetadata.Id = currentMetadata?.Id;
                session.Store(newMetadata);
                session.SaveChanges();
            }
            else
            {
                await UpdatePriceDataOnly();
            }
        }

        private async Task UpdateAllItemAndPriceData(string tarkovDatabaseToken)
        {
            #region Fetch items from Tarkov-Database
            List<Ammunition> ammunitions = await _tarkovDatabaseService.GetItemsByKindAsync<Ammunition>(tarkovDatabaseToken, KindOfItem.Ammunition);
            List<Armor> armors = await _tarkovDatabaseService.GetItemsByKindAsync<Armor>(tarkovDatabaseToken, KindOfItem.Armor);
            List<Backpack> backpacks = await _tarkovDatabaseService.GetItemsByKindAsync<Backpack>(tarkovDatabaseToken, KindOfItem.Backpack);
            List<Barter> barters = await _tarkovDatabaseService.GetItemsByKindAsync<Barter>(tarkovDatabaseToken, KindOfItem.Barter);
            List<Clothing> clothings = await _tarkovDatabaseService.GetItemsByKindAsync<Clothing>(tarkovDatabaseToken, KindOfItem.Clothing);
            List<Common> commons = await _tarkovDatabaseService.GetItemsByKindAsync<Common>(tarkovDatabaseToken, KindOfItem.Common);
            List<Container> containers = await _tarkovDatabaseService.GetItemsByKindAsync<Container>(tarkovDatabaseToken, KindOfItem.Container);
            List<Firearm> firearms = await _tarkovDatabaseService.GetItemsByKindAsync<Firearm>(tarkovDatabaseToken, KindOfItem.Firearm);
            List<Food> foods = await _tarkovDatabaseService.GetItemsByKindAsync<Food>(tarkovDatabaseToken, KindOfItem.Food);
            List<Grenade> grenades = await _tarkovDatabaseService.GetItemsByKindAsync<Grenade>(tarkovDatabaseToken, KindOfItem.Grenade);
            List<Headphone> headphones = await _tarkovDatabaseService.GetItemsByKindAsync<Headphone>(tarkovDatabaseToken, KindOfItem.Headphone);
            List<Key> keys = await _tarkovDatabaseService.GetItemsByKindAsync<Key>(tarkovDatabaseToken, KindOfItem.Key);
            List<Magazine> magazines = await _tarkovDatabaseService.GetItemsByKindAsync<Magazine>(tarkovDatabaseToken, KindOfItem.Magazine);
            List<Map> maps = await _tarkovDatabaseService.GetItemsByKindAsync<Map>(tarkovDatabaseToken, KindOfItem.Map);
            List<Medical> medicals = await _tarkovDatabaseService.GetItemsByKindAsync<Medical>(tarkovDatabaseToken, KindOfItem.Medical);
            List<Melee> melees = await _tarkovDatabaseService.GetItemsByKindAsync<Melee>(tarkovDatabaseToken, KindOfItem.Melee);
            List<Modification> modifications = await _tarkovDatabaseService.GetItemsByKindAsync<Modification>(tarkovDatabaseToken, KindOfItem.Modification);
            List<ModificationBarrel> modificationBarrels = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBarrel>(tarkovDatabaseToken, KindOfItem.ModificationBarrel);
            List<ModificationBipod> modificationBipods = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBipod>(tarkovDatabaseToken, KindOfItem.ModificationBipod);
            List<ModificationCharge> modificationCharges = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationCharge>(tarkovDatabaseToken, KindOfItem.ModificationCharge);
            List<ModificationDevice> modificationDevices = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationDevice>(tarkovDatabaseToken, KindOfItem.ModificationDevice);
            List<ModificationForegrip> modificationForegrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationForegrip>(tarkovDatabaseToken, KindOfItem.ModificationForegrip);
            List<ModificationGasblock> modificationGasblocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGasblock>(tarkovDatabaseToken, KindOfItem.ModificationGasblock);
            List<ModificationGoggles> modificationGoggles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGoggles>(tarkovDatabaseToken, KindOfItem.ModificationGoggles);
            List<ModificationHandguard> modificationHandguards = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationHandguard>(tarkovDatabaseToken, KindOfItem.ModificationHandguard);
            List<ModificationLauncher> modificationLaunchers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationLauncher>(tarkovDatabaseToken, KindOfItem.ModificationLauncher);
            List<ModificationMount> modificationMounts = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMount>(tarkovDatabaseToken, KindOfItem.ModificationMount);
            List<ModificationMuzzle> modificationMuzzles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMuzzle>(tarkovDatabaseToken, KindOfItem.ModificationMuzzle);
            List<ModificationPistolgrip> modificationPistolgrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationPistolgrip>(tarkovDatabaseToken, KindOfItem.ModificationPistolgrip);
            List<ModificationReceiver> modificationReceivers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationReceiver>(tarkovDatabaseToken, KindOfItem.ModificationReceiver);
            List<ModificationSight> modificationSights = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSight>(tarkovDatabaseToken, KindOfItem.ModificationSight);
            List<ModificationSightSpecial> modificationSightSpecials = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSightSpecial>(tarkovDatabaseToken, KindOfItem.ModificationSightSpecial);
            List<ModificationStock> modificationStocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationStock>(tarkovDatabaseToken, KindOfItem.ModificationStock);
            List<Money> moneys = await _tarkovDatabaseService.GetItemsByKindAsync<Money>(tarkovDatabaseToken, KindOfItem.Money);
            List<Tacticalrig> tacticalrigs = await _tarkovDatabaseService.GetItemsByKindAsync<Tacticalrig>(tarkovDatabaseToken, KindOfItem.Tacticalrig);
            #endregion

            #region Fetch price and image data from Tarkov-Tools and unify with Tarkov-Database data
            List<TarkovToolsItem> tarkovToolsItems = await _tarkovToolsService.GetItemsByTypeAsync(ItemType.any);

            ammunitions = CombineItemsData(ammunitions, tarkovToolsItems).ToList();
            armors = CombineItemsData(armors, tarkovToolsItems).ToList();
            backpacks = CombineItemsData(backpacks, tarkovToolsItems).ToList();
            barters = CombineItemsData(barters, tarkovToolsItems).ToList();
            clothings = CombineItemsData(clothings, tarkovToolsItems).ToList();
            commons = CombineItemsData(commons, tarkovToolsItems).ToList();
            containers = CombineItemsData(containers, tarkovToolsItems).ToList();
            firearms = CombineItemsData(firearms, tarkovToolsItems).ToList();
            foods = CombineItemsData(foods, tarkovToolsItems).ToList();
            grenades = CombineItemsData(grenades, tarkovToolsItems).ToList();
            headphones = CombineItemsData(headphones, tarkovToolsItems).ToList();
            keys = CombineItemsData(keys, tarkovToolsItems).ToList();
            magazines = CombineItemsData(magazines, tarkovToolsItems).ToList();
            maps = CombineItemsData(maps, tarkovToolsItems).ToList();
            medicals = CombineItemsData(medicals, tarkovToolsItems).ToList();
            melees = CombineItemsData(melees, tarkovToolsItems).ToList();
            modifications = CombineItemsData(modifications, tarkovToolsItems).ToList();
            modificationBarrels = CombineItemsData(modificationBarrels, tarkovToolsItems).ToList();
            modificationBipods = CombineItemsData(modificationBipods, tarkovToolsItems).ToList();
            modificationCharges = CombineItemsData(modificationCharges, tarkovToolsItems).ToList();
            modificationDevices = CombineItemsData(modificationDevices, tarkovToolsItems).ToList();
            modificationForegrips = CombineItemsData(modificationForegrips, tarkovToolsItems).ToList();
            modificationGasblocks = CombineItemsData(modificationGasblocks, tarkovToolsItems).ToList();
            modificationGoggles = CombineItemsData(modificationGoggles, tarkovToolsItems).ToList();
            modificationHandguards = CombineItemsData(modificationHandguards, tarkovToolsItems).ToList();
            modificationLaunchers = CombineItemsData(modificationLaunchers, tarkovToolsItems).ToList();
            modificationMounts = CombineItemsData(modificationMounts, tarkovToolsItems).ToList();
            modificationMuzzles = CombineItemsData(modificationMuzzles, tarkovToolsItems).ToList();
            modificationPistolgrips = CombineItemsData(modificationPistolgrips, tarkovToolsItems).ToList();
            modificationReceivers = CombineItemsData(modificationReceivers, tarkovToolsItems).ToList();
            modificationSights = CombineItemsData(modificationSights, tarkovToolsItems).ToList();
            modificationSightSpecials = CombineItemsData(modificationSightSpecials, tarkovToolsItems).ToList();
            modificationStocks = CombineItemsData(modificationStocks, tarkovToolsItems).ToList();
            moneys = CombineItemsData(moneys, tarkovToolsItems).ToList();
            tacticalrigs = CombineItemsData(tacticalrigs, tarkovToolsItems).ToList();
            #endregion

            #region Create/Update/Delete items
            CreateUpdateDeleteItems(ammunitions);
            CreateUpdateDeleteItems(armors);
            CreateUpdateDeleteItems(backpacks);
            CreateUpdateDeleteItems(barters);
            CreateUpdateDeleteItems(clothings);
            CreateUpdateDeleteItems(commons);
            CreateUpdateDeleteItems(containers);
            CreateUpdateDeleteItems(firearms);
            CreateUpdateDeleteItems(foods);
            CreateUpdateDeleteItems(grenades);
            CreateUpdateDeleteItems(headphones);
            CreateUpdateDeleteItems(keys);
            CreateUpdateDeleteItems(magazines);
            CreateUpdateDeleteItems(maps);
            CreateUpdateDeleteItems(medicals);
            CreateUpdateDeleteItems(melees);
            CreateUpdateDeleteItems(modifications);
            CreateUpdateDeleteItems(modificationBarrels);
            CreateUpdateDeleteItems(modificationBipods);
            CreateUpdateDeleteItems(modificationCharges);
            CreateUpdateDeleteItems(modificationDevices);
            CreateUpdateDeleteItems(modificationForegrips);
            CreateUpdateDeleteItems(modificationGasblocks);
            CreateUpdateDeleteItems(modificationGoggles);
            CreateUpdateDeleteItems(modificationHandguards);
            CreateUpdateDeleteItems(modificationLaunchers);
            CreateUpdateDeleteItems(modificationMounts);
            CreateUpdateDeleteItems(modificationMuzzles);
            CreateUpdateDeleteItems(modificationPistolgrips);
            CreateUpdateDeleteItems(modificationReceivers);
            CreateUpdateDeleteItems(modificationSights);
            CreateUpdateDeleteItems(modificationSightSpecials);
            CreateUpdateDeleteItems(modificationStocks);
            CreateUpdateDeleteItems(moneys);
            CreateUpdateDeleteItems(tacticalrigs);
            #endregion

            session.SaveChanges();
        }

        private async Task UpdatePriceDataOnly()
        {
            session.Advanced.MaxNumberOfRequestsPerSession += 20;

            #region Fetch items from our database
            List<Ammunition> ammunitions = _itemService.GetItemsByKind<Ammunition>();
            List<Armor> armors = _itemService.GetItemsByKind<Armor>();
            List<Backpack> backpacks = _itemService.GetItemsByKind<Backpack>();
            List<Barter> barters = _itemService.GetItemsByKind<Barter>();
            List<Clothing> clothings = _itemService.GetItemsByKind<Clothing>();
            List<Common> commons = _itemService.GetItemsByKind<Common>();
            List<Container> containers = _itemService.GetItemsByKind<Container>();
            List<Firearm> firearms = _itemService.GetItemsByKind<Firearm>();
            List<Food> foods = _itemService.GetItemsByKind<Food>();
            List<Grenade> grenades = _itemService.GetItemsByKind<Grenade>();
            List<Headphone> headphones = _itemService.GetItemsByKind<Headphone>();
            List<Key> keys = _itemService.GetItemsByKind<Key>();
            List<Magazine> magazines = _itemService.GetItemsByKind<Magazine>();
            List<Map> maps = _itemService.GetItemsByKind<Map>();
            List<Medical> medicals = _itemService.GetItemsByKind<Medical>();
            List<Melee> melees = _itemService.GetItemsByKind<Melee>();
            List<Modification> modifications = _itemService.GetItemsByKind<Modification>();
            List<ModificationBarrel> modificationBarrels = _itemService.GetItemsByKind<ModificationBarrel>();
            List<ModificationBipod> modificationBipods = _itemService.GetItemsByKind<ModificationBipod>();
            List<ModificationCharge> modificationCharges = _itemService.GetItemsByKind<ModificationCharge>();
            List<ModificationDevice> modificationDevices = _itemService.GetItemsByKind<ModificationDevice>();
            List<ModificationForegrip> modificationForegrips = _itemService.GetItemsByKind<ModificationForegrip>();
            List<ModificationGasblock> modificationGasblocks = _itemService.GetItemsByKind<ModificationGasblock>();
            List<ModificationGoggles> modificationGoggles = _itemService.GetItemsByKind<ModificationGoggles>();
            List<ModificationHandguard> modificationHandguards = _itemService.GetItemsByKind<ModificationHandguard>();
            List<ModificationLauncher> modificationLaunchers = _itemService.GetItemsByKind<ModificationLauncher>();
            List<ModificationMount> modificationMounts = _itemService.GetItemsByKind<ModificationMount>();
            List<ModificationMuzzle> modificationMuzzles = _itemService.GetItemsByKind<ModificationMuzzle>();
            List<ModificationPistolgrip> modificationPistolgrips = _itemService.GetItemsByKind<ModificationPistolgrip>();
            List<ModificationReceiver> modificationReceivers = _itemService.GetItemsByKind<ModificationReceiver>();
            List<ModificationSight> modificationSights = _itemService.GetItemsByKind<ModificationSight>();
            List<ModificationSightSpecial> modificationSightSpecials = _itemService.GetItemsByKind<ModificationSightSpecial>();
            List<ModificationStock> modificationStocks = _itemService.GetItemsByKind<ModificationStock>();
            List<Money> moneys = _itemService.GetItemsByKind<Money>();
            List<Tacticalrig> tacticalrigs = _itemService.GetItemsByKind<Tacticalrig>();
            #endregion

            #region Fetch price and image data from Tarkov-Tools and unify with our existing data
            List<TarkovToolsItem> tarkovToolsItems = await _tarkovToolsService.GetItemsByTypeAsync(ItemType.any);

            ammunitions = CombineItemsData(ammunitions, tarkovToolsItems).ToList();
            armors = CombineItemsData(armors, tarkovToolsItems).ToList();
            backpacks = CombineItemsData(backpacks, tarkovToolsItems).ToList();
            barters = CombineItemsData(barters, tarkovToolsItems).ToList();
            clothings = CombineItemsData(clothings, tarkovToolsItems).ToList();
            commons = CombineItemsData(commons, tarkovToolsItems).ToList();
            containers = CombineItemsData(containers, tarkovToolsItems).ToList();
            firearms = CombineItemsData(firearms, tarkovToolsItems).ToList();
            foods = CombineItemsData(foods, tarkovToolsItems).ToList();
            grenades = CombineItemsData(grenades, tarkovToolsItems).ToList();
            headphones = CombineItemsData(headphones, tarkovToolsItems).ToList();
            keys = CombineItemsData(keys, tarkovToolsItems).ToList();
            magazines = CombineItemsData(magazines, tarkovToolsItems).ToList();
            maps = CombineItemsData(maps, tarkovToolsItems).ToList();
            medicals = CombineItemsData(medicals, tarkovToolsItems).ToList();
            melees = CombineItemsData(melees, tarkovToolsItems).ToList();
            modifications = CombineItemsData(modifications, tarkovToolsItems).ToList();
            modificationBarrels = CombineItemsData(modificationBarrels, tarkovToolsItems).ToList();
            modificationBipods = CombineItemsData(modificationBipods, tarkovToolsItems).ToList();
            modificationCharges = CombineItemsData(modificationCharges, tarkovToolsItems).ToList();
            modificationDevices = CombineItemsData(modificationDevices, tarkovToolsItems).ToList();
            modificationForegrips = CombineItemsData(modificationForegrips, tarkovToolsItems).ToList();
            modificationGasblocks = CombineItemsData(modificationGasblocks, tarkovToolsItems).ToList();
            modificationGoggles = CombineItemsData(modificationGoggles, tarkovToolsItems).ToList();
            modificationHandguards = CombineItemsData(modificationHandguards, tarkovToolsItems).ToList();
            modificationLaunchers = CombineItemsData(modificationLaunchers, tarkovToolsItems).ToList();
            modificationMounts = CombineItemsData(modificationMounts, tarkovToolsItems).ToList();
            modificationMuzzles = CombineItemsData(modificationMuzzles, tarkovToolsItems).ToList();
            modificationPistolgrips = CombineItemsData(modificationPistolgrips, tarkovToolsItems).ToList();
            modificationReceivers = CombineItemsData(modificationReceivers, tarkovToolsItems).ToList();
            modificationSights = CombineItemsData(modificationSights, tarkovToolsItems).ToList();
            modificationSightSpecials = CombineItemsData(modificationSightSpecials, tarkovToolsItems).ToList();
            modificationStocks = CombineItemsData(modificationStocks, tarkovToolsItems).ToList();
            moneys = CombineItemsData(moneys, tarkovToolsItems).ToList();
            tacticalrigs = CombineItemsData(tacticalrigs, tarkovToolsItems).ToList();
            #endregion

            #region Create and update items
            CreateUpdateDeleteItems(ammunitions);
            CreateUpdateDeleteItems(armors);
            CreateUpdateDeleteItems(backpacks);
            CreateUpdateDeleteItems(barters);
            CreateUpdateDeleteItems(clothings);
            CreateUpdateDeleteItems(commons);
            CreateUpdateDeleteItems(containers);
            CreateUpdateDeleteItems(firearms);
            CreateUpdateDeleteItems(foods);
            CreateUpdateDeleteItems(grenades);
            CreateUpdateDeleteItems(headphones);
            CreateUpdateDeleteItems(keys);
            CreateUpdateDeleteItems(magazines);
            CreateUpdateDeleteItems(maps);
            CreateUpdateDeleteItems(medicals);
            CreateUpdateDeleteItems(melees);
            CreateUpdateDeleteItems(modifications);
            CreateUpdateDeleteItems(modificationBarrels);
            CreateUpdateDeleteItems(modificationBipods);
            CreateUpdateDeleteItems(modificationCharges);
            CreateUpdateDeleteItems(modificationDevices);
            CreateUpdateDeleteItems(modificationForegrips);
            CreateUpdateDeleteItems(modificationGasblocks);
            CreateUpdateDeleteItems(modificationGoggles);
            CreateUpdateDeleteItems(modificationHandguards);
            CreateUpdateDeleteItems(modificationLaunchers);
            CreateUpdateDeleteItems(modificationMounts);
            CreateUpdateDeleteItems(modificationMuzzles);
            CreateUpdateDeleteItems(modificationPistolgrips);
            CreateUpdateDeleteItems(modificationReceivers);
            CreateUpdateDeleteItems(modificationSights);
            CreateUpdateDeleteItems(modificationSightSpecials);
            CreateUpdateDeleteItems(modificationStocks);
            CreateUpdateDeleteItems(moneys);
            CreateUpdateDeleteItems(tacticalrigs);
            #endregion

            session.SaveChanges();
        }

        private void CreateUpdateDeleteItems<T>(IEnumerable<T> items) where T : IItem
        {
            var bsgIds = items.Select(x => x.BsgId).ToList();
            session.Advanced.MaxNumberOfRequestsPerSession += 1; // Naughty, but this will only happen for as many different item kinds there are
            var existingItems = session
                .Query<T>()
                .Where(x => x.BsgId.In(bsgIds))
                .ToList();

            // Update existing items
            for (var i = 0; i < existingItems.Count; i++)
            {
                var item = items.Where(x => x.BsgId == existingItems[i].BsgId).FirstOrDefault();
                if (item.IsNotNull())
                {
                    existingItems[i].CopyFrom(item);
                    session.Store(existingItems[i]);
                }
            }

            // Create new items
            var existingItemsBsgIds = existingItems.Select(x => x.BsgId).ToList();
            foreach (var item in items)
            {
                if (!existingItemsBsgIds.Contains(item.BsgId))
                {
                    session.Store(item);
                }
            }

            // Delete any items BSG has deleted (don't know if they do this but just in case)
            var itemsBsgIds = items.Select(x => x.BsgId).ToList();
            foreach (var existingItem in existingItems)
            {
                if (!itemsBsgIds.Contains(existingItem.BsgId))
                {
                    session.Delete(existingItem);
                }
            }
        }

        private IEnumerable<T> CombineItemsData<T>(IEnumerable<T> items, IEnumerable<TarkovToolsItem> tarkovToolsItems) where T : IItem
        {
            foreach (var item in items)
            {
                var tarkovToolsItem = tarkovToolsItems.Where(x => x.Id == item.BsgId).FirstOrDefault();
                if (tarkovToolsItem.IsNotNull())
                {
                    item.Avg24hPrice = tarkovToolsItem.Avg24hPrice;
                    item.Icon = tarkovToolsItem.IconLink ?? "";
                    item.Img = tarkovToolsItem.IconLink ?? "";
                    item.ImgBig = tarkovToolsItem.ImageLink ?? "";
                    item.WikiLink = tarkovToolsItem.WikiLink;
                }
            }

            return items;
        }
    }
}
