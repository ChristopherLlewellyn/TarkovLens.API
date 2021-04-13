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
using TarkovLens.Services.TarkovDatabase;
using TarkovLens.Services.TarkovMarket;
using TarkovLens.Services.TarkovTools;

namespace TarkovLens.Services
{
    public interface IItemUpdaterService
    {
        public Task UpdateAllItemsAsync();
    }

    public class ItemUpdaterService : IItemUpdaterService
    {
        private readonly IDocumentSession session;
        private ITarkovDatabaseService _tarkovDatabaseService;
        private ITarkovMarketService _tarkovMarketService;
        private ITarkovToolsService _tarkovToolsService;

        public ItemUpdaterService(IDocumentSession documentSession,
                              ITarkovDatabaseService tarkovDatabaseService,
                              ITarkovMarketService tarkovMarketService,
                              ITarkovToolsService tarkovToolsService)
        {
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovMarketService = tarkovMarketService;
            _tarkovToolsService = tarkovToolsService;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task UpdateAllItemsAsync()
        {
            #region Fetch items from Tarkov-Database
            string token = await _tarkovDatabaseService.GetNewAuthTokenAsync();

            List<Ammunition> ammunitions = await _tarkovDatabaseService.GetItemsByKindAsync<Ammunition>(token, KindOfItem.Ammunition);
            List<Armor> armors = await _tarkovDatabaseService.GetItemsByKindAsync<Armor>(token, KindOfItem.Armor);
            List<Backpack> backpacks = await _tarkovDatabaseService.GetItemsByKindAsync<Backpack>(token, KindOfItem.Backpack);
            List<Barter> barters = await _tarkovDatabaseService.GetItemsByKindAsync<Barter>(token, KindOfItem.Barter);
            List<Clothing> clothings = await _tarkovDatabaseService.GetItemsByKindAsync<Clothing>(token, KindOfItem.Clothing);
            List<Common> commons = await _tarkovDatabaseService.GetItemsByKindAsync<Common>(token, KindOfItem.Common);
            List<Container> containers = await _tarkovDatabaseService.GetItemsByKindAsync<Container>(token, KindOfItem.Container);
            List<Firearm> firearms = await _tarkovDatabaseService.GetItemsByKindAsync<Firearm>(token, KindOfItem.Firearm);
            List<Food> foods = await _tarkovDatabaseService.GetItemsByKindAsync<Food>(token, KindOfItem.Food);
            List<Grenade> grenades = await _tarkovDatabaseService.GetItemsByKindAsync<Grenade>(token, KindOfItem.Grenade);
            List<Headphone> headphones = await _tarkovDatabaseService.GetItemsByKindAsync<Headphone>(token, KindOfItem.Headphone);
            List<Key> keys = await _tarkovDatabaseService.GetItemsByKindAsync<Key>(token, KindOfItem.Key);
            List<Magazine> magazines = await _tarkovDatabaseService.GetItemsByKindAsync<Magazine>(token, KindOfItem.Magazine);
            List<Map> maps = await _tarkovDatabaseService.GetItemsByKindAsync<Map>(token, KindOfItem.Map);
            List<Medical> medicals = await _tarkovDatabaseService.GetItemsByKindAsync<Medical>(token, KindOfItem.Medical);
            List<Melee> melees = await _tarkovDatabaseService.GetItemsByKindAsync<Melee>(token, KindOfItem.Melee);
            List<Modification> modifications = await _tarkovDatabaseService.GetItemsByKindAsync<Modification>(token, KindOfItem.Modification);
            List<ModificationBarrel> modificationBarrels = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBarrel>(token, KindOfItem.ModificationBarrel);
            List<ModificationBipod> modificationBipods = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBipod>(token, KindOfItem.ModificationBipod);
            List<ModificationCharge> modificationCharges = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationCharge>(token, KindOfItem.ModificationCharge);
            List<ModificationDevice> modificationDevices = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationDevice>(token, KindOfItem.ModificationDevice);
            List<ModificationForegrip> modificationForegrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationForegrip>(token, KindOfItem.ModificationForegrip);
            List<ModificationGasblock> modificationGasblocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGasblock>(token, KindOfItem.ModificationGasblock);
            List<ModificationGoggles> modificationGoggles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGoggles>(token, KindOfItem.ModificationGoggles);
            List<ModificationHandguard> modificationHandguards = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationHandguard>(token, KindOfItem.ModificationHandguard);
            List<ModificationLauncher> modificationLaunchers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationLauncher>(token, KindOfItem.ModificationLauncher);
            List<ModificationMount> modificationMounts = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMount>(token, KindOfItem.ModificationMount);
            List<ModificationMuzzle> modificationMuzzles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMuzzle>(token, KindOfItem.ModificationMuzzle);
            List<ModificationPistolgrip> modificationPistolgrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationPistolgrip>(token, KindOfItem.ModificationPistolgrip);
            List<ModificationReceiver> modificationReceivers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationReceiver>(token, KindOfItem.ModificationReceiver);
            List<ModificationSight> modificationSights = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSight>(token, KindOfItem.ModificationSight);
            List<ModificationSightSpecial> modificationSightSpecials = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSightSpecial>(token, KindOfItem.ModificationSightSpecial);
            List<ModificationStock> modificationStocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationStock>(token, KindOfItem.ModificationStock);
            List<Money> moneys = await _tarkovDatabaseService.GetItemsByKindAsync<Money>(token, KindOfItem.Money);
            List<Tacticalrig> tacticalrigs = await _tarkovDatabaseService.GetItemsByKindAsync<Tacticalrig>(token, KindOfItem.Tacticalrig);
            #endregion

            #region Fetch additional info from Tarkov-Tools and unify the data
            List<TarkovToolsItem> tarkovToolsItems = await _tarkovToolsService.GetItemsByTypeAsync(ItemType.any);

            ammunitions = MapItemsData(ammunitions, tarkovToolsItems).ToList();
            armors = MapItemsData(armors, tarkovToolsItems).ToList();
            backpacks = MapItemsData(backpacks, tarkovToolsItems).ToList();
            barters = MapItemsData(barters, tarkovToolsItems).ToList();
            clothings = MapItemsData(clothings, tarkovToolsItems).ToList();
            commons = MapItemsData(commons, tarkovToolsItems).ToList();
            containers = MapItemsData(containers, tarkovToolsItems).ToList();
            firearms = MapItemsData(firearms, tarkovToolsItems).ToList();
            foods = MapItemsData(foods, tarkovToolsItems).ToList();
            grenades = MapItemsData(grenades, tarkovToolsItems).ToList();
            headphones = MapItemsData(headphones, tarkovToolsItems).ToList();
            keys = MapItemsData(keys, tarkovToolsItems).ToList();
            magazines = MapItemsData(magazines, tarkovToolsItems).ToList();
            maps = MapItemsData(maps, tarkovToolsItems).ToList();
            medicals = MapItemsData(medicals, tarkovToolsItems).ToList();
            melees = MapItemsData(melees, tarkovToolsItems).ToList();
            modifications = MapItemsData(modifications, tarkovToolsItems).ToList();
            modificationBarrels = MapItemsData(modificationBarrels, tarkovToolsItems).ToList();
            modificationBipods = MapItemsData(modificationBipods, tarkovToolsItems).ToList();
            modificationCharges = MapItemsData(modificationCharges, tarkovToolsItems).ToList();
            modificationDevices = MapItemsData(modificationDevices, tarkovToolsItems).ToList();
            modificationForegrips = MapItemsData(modificationForegrips, tarkovToolsItems).ToList();
            modificationGasblocks = MapItemsData(modificationGasblocks, tarkovToolsItems).ToList();
            modificationGoggles = MapItemsData(modificationGoggles, tarkovToolsItems).ToList();
            modificationHandguards = MapItemsData(modificationHandguards, tarkovToolsItems).ToList();
            modificationLaunchers = MapItemsData(modificationLaunchers, tarkovToolsItems).ToList();
            modificationMounts = MapItemsData(modificationMounts, tarkovToolsItems).ToList();
            modificationMuzzles = MapItemsData(modificationMuzzles, tarkovToolsItems).ToList();
            modificationPistolgrips = MapItemsData(modificationPistolgrips, tarkovToolsItems).ToList();
            modificationReceivers = MapItemsData(modificationReceivers, tarkovToolsItems).ToList();
            modificationSights = MapItemsData(modificationSights, tarkovToolsItems).ToList();
            modificationSightSpecials = MapItemsData(modificationSightSpecials, tarkovToolsItems).ToList();
            modificationStocks = MapItemsData(modificationStocks, tarkovToolsItems).ToList();
            moneys = MapItemsData(moneys, tarkovToolsItems).ToList();
            tacticalrigs = MapItemsData(tacticalrigs, tarkovToolsItems).ToList();
            #endregion

            #region Create and update items
            CreateOrUpdateItems(ammunitions);
            CreateOrUpdateItems(armors);
            CreateOrUpdateItems(backpacks);
            CreateOrUpdateItems(barters);
            CreateOrUpdateItems(clothings);
            CreateOrUpdateItems(commons);
            CreateOrUpdateItems(containers);
            CreateOrUpdateItems(firearms);
            CreateOrUpdateItems(foods);
            CreateOrUpdateItems(grenades);
            CreateOrUpdateItems(headphones);
            CreateOrUpdateItems(keys);
            CreateOrUpdateItems(magazines);
            CreateOrUpdateItems(maps);
            CreateOrUpdateItems(medicals);
            CreateOrUpdateItems(melees);
            CreateOrUpdateItems(modifications);
            CreateOrUpdateItems(modificationBarrels);
            CreateOrUpdateItems(modificationBipods);
            CreateOrUpdateItems(modificationCharges);
            CreateOrUpdateItems(modificationDevices);
            CreateOrUpdateItems(modificationForegrips);
            CreateOrUpdateItems(modificationGasblocks);
            CreateOrUpdateItems(modificationGoggles);
            CreateOrUpdateItems(modificationHandguards);
            CreateOrUpdateItems(modificationLaunchers);
            CreateOrUpdateItems(modificationMounts);
            CreateOrUpdateItems(modificationMuzzles);
            CreateOrUpdateItems(modificationPistolgrips);
            CreateOrUpdateItems(modificationReceivers);
            CreateOrUpdateItems(modificationSights);
            CreateOrUpdateItems(modificationSightSpecials);
            CreateOrUpdateItems(modificationStocks);
            CreateOrUpdateItems(moneys);
            CreateOrUpdateItems(tacticalrigs);
            #endregion

            session.SaveChanges();
        }

        private void CreateOrUpdateItems<T>(IEnumerable<T> items) where T : IItem
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

        private IEnumerable<T> MapItemsData<T>(IEnumerable<T> tarkovDatabaseItems, IEnumerable<TarkovToolsItem> tarkovToolsItems) where T : IItem
        {
            foreach (var tarkovDatabaseItem in tarkovDatabaseItems)
            {
                var tarkovToolsItem = tarkovToolsItems.Where(x => x.Id == tarkovDatabaseItem.BsgId).FirstOrDefault();
                if (tarkovToolsItem.IsNotNull())
                {
                    tarkovDatabaseItem.Avg24hPrice = tarkovToolsItem.Avg24hPrice;
                    tarkovDatabaseItem.Icon = tarkovToolsItem.IconLink ?? "";
                    tarkovDatabaseItem.Img = tarkovToolsItem.IconLink ?? "";
                    tarkovDatabaseItem.ImgBig = tarkovToolsItem.ImageLink ?? "";
                    tarkovDatabaseItem.WikiLink = tarkovToolsItem.WikiLink;
                }
            }

            return tarkovDatabaseItems;
        }
    }
}
