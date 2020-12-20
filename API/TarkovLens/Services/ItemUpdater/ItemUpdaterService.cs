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

        public ItemUpdaterService(IDocumentSession documentSession,
                              ITarkovDatabaseService tarkovDatabaseService, ITarkovMarketService tarkovMarketService)
        {
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovMarketService = tarkovMarketService;
        }

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
            List<ModificationBipod> modificationBipods= await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBipod>(token, KindOfItem.ModificationBipod);
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
            List<ModificationSightSpecial> modificationSightSpecials= await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSightSpecial>(token, KindOfItem.ModificationSightSpecial);
            List<ModificationStock> modificationStocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationStock>(token, KindOfItem.ModificationStock);
            List<Money> moneys = await _tarkovDatabaseService.GetItemsByKindAsync<Money>(token, KindOfItem.Money);
            List<Tacticalrig> tacticalrigs = await _tarkovDatabaseService.GetItemsByKindAsync<Tacticalrig>(token, KindOfItem.Tacticalrig);
            #endregion

            #region Fetch items from Tarkov-Market and unify the data
            List<TarkovMarketItem> tarkovMarketItems = await _tarkovMarketService.GetAllItemsAsync();

            ammunitions = MapItemsData(ammunitions, tarkovMarketItems).ToList();
            armors = MapItemsData(armors, tarkovMarketItems).ToList();
            backpacks = MapItemsData(backpacks, tarkovMarketItems).ToList();
            barters = MapItemsData(barters, tarkovMarketItems).ToList();
            clothings = MapItemsData(clothings, tarkovMarketItems).ToList();
            commons = MapItemsData(commons, tarkovMarketItems).ToList();
            containers = MapItemsData(containers, tarkovMarketItems).ToList();
            firearms = MapItemsData(firearms, tarkovMarketItems).ToList();
            foods = MapItemsData(foods, tarkovMarketItems).ToList();
            grenades = MapItemsData(grenades, tarkovMarketItems).ToList();
            headphones = MapItemsData(headphones, tarkovMarketItems).ToList();
            keys = MapItemsData(keys, tarkovMarketItems).ToList();
            magazines = MapItemsData(magazines, tarkovMarketItems).ToList();
            maps = MapItemsData(maps, tarkovMarketItems).ToList();
            medicals = MapItemsData(medicals, tarkovMarketItems).ToList();
            melees = MapItemsData(melees, tarkovMarketItems).ToList();
            modifications = MapItemsData(modifications, tarkovMarketItems).ToList();
            modificationBarrels = MapItemsData(modificationBarrels, tarkovMarketItems).ToList();
            modificationBipods = MapItemsData(modificationBipods, tarkovMarketItems).ToList();
            modificationCharges = MapItemsData(modificationCharges, tarkovMarketItems).ToList();
            modificationDevices = MapItemsData(modificationDevices, tarkovMarketItems).ToList();
            modificationForegrips = MapItemsData(modificationForegrips, tarkovMarketItems).ToList();
            modificationGasblocks = MapItemsData(modificationGasblocks, tarkovMarketItems).ToList();
            modificationGoggles = MapItemsData(modificationGoggles, tarkovMarketItems).ToList();
            modificationHandguards = MapItemsData(modificationHandguards, tarkovMarketItems).ToList();
            modificationLaunchers = MapItemsData(modificationLaunchers, tarkovMarketItems).ToList();
            modificationMounts = MapItemsData(modificationMounts, tarkovMarketItems).ToList();
            modificationMuzzles = MapItemsData(modificationMuzzles, tarkovMarketItems).ToList();
            modificationPistolgrips = MapItemsData(modificationPistolgrips, tarkovMarketItems).ToList();
            modificationReceivers = MapItemsData(modificationReceivers, tarkovMarketItems).ToList();
            modificationSights = MapItemsData(modificationSights, tarkovMarketItems).ToList();
            modificationSightSpecials = MapItemsData(modificationSightSpecials, tarkovMarketItems).ToList();
            modificationStocks = MapItemsData(modificationStocks, tarkovMarketItems).ToList();
            moneys = MapItemsData(moneys, tarkovMarketItems).ToList();
            tacticalrigs = MapItemsData(tacticalrigs, tarkovMarketItems).ToList();
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
            session.Advanced.MaxNumberOfRequestsPerSession += 1;
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
            List<T> newItems = new List<T>();
            var existingItemsBsgIds = existingItems.Select(x => x.BsgId).ToList();
            foreach (var item in items)
            {
                if (!existingItemsBsgIds.Contains(item.BsgId))
                {
                    session.Store(item);
                }
            }
        }

        private IEnumerable<T> MapItemsData<T>(IEnumerable<T> tarkovDatabaseItems, IEnumerable<TarkovMarketItem> tarkovMarketItems) where T : IItem
        {
            foreach (var tarkovDatabaseItem in tarkovDatabaseItems)
            {
                // First need to check if there's a non-functional version because, for firearms, this will be what tarkov-database gives us
                // for a few guns that can be stripped down to where they're no longer functioning (e.g. M4A1, TX-15, SA-58)
                // Then we need to check for a functioning version if a non-functional version wasn't found.
                // If we didn't do this then we could end up with market prices for a functioning M4A1 on a non-functioning M4A1.
                var marketItem = tarkovMarketItems.Where(x => x.BsgId == tarkovDatabaseItem.BsgId && x.IsFunctional == false).FirstOrDefault();
                if (marketItem.IsNull())
                {
                    marketItem = tarkovMarketItems.Where(x => x.BsgId == tarkovDatabaseItem.BsgId).FirstOrDefault();
                }
                
                if (marketItem.IsNotNull())
                {
                    tarkovDatabaseItem.Avg24hPrice = marketItem.Avg24hPrice;
                    tarkovDatabaseItem.Avg7daysPrice = marketItem.Avg7daysPrice;
                    tarkovDatabaseItem.Diff24h = marketItem.Diff24h;
                    tarkovDatabaseItem.Diff7days = marketItem.Diff7days;
                    tarkovDatabaseItem.Icon = marketItem.Icon;
                    tarkovDatabaseItem.Img = marketItem.Img;
                    tarkovDatabaseItem.ImgBig = marketItem.ImgBig;
                    tarkovDatabaseItem.LastLowestMarketPrice = marketItem.Price;
                    tarkovDatabaseItem.WikiLink = marketItem.WikiLink;
                    tarkovDatabaseItem.Updated = marketItem.Updated;
                }
            }

            return tarkovDatabaseItems;
        }
    }
}
