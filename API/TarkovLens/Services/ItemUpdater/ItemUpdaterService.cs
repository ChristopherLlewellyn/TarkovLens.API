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
        private IItemRepository _itemRepository;

        public ItemUpdaterService(IDocumentSession documentSession,
                              ITarkovDatabaseService tarkovDatabaseService,
                              ITarkovToolsService tarkovToolsService,
                              IItemRepository itemRepository)
        {
            session = documentSession;
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovToolsService = tarkovToolsService;
            _itemRepository = itemRepository;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task UpdateItemsTask()
        {
            string tarkovDatabaseToken = await _tarkovDatabaseService.GetNewAuthTokenAsync();

            // Check for updates
            var currentMetadata = _itemRepository.GetItemKindsMetadata();
            var newMetadata = await _tarkovDatabaseService.GetItemKindsMetadataAsync(tarkovDatabaseToken);

            // Update items
            // If Tarkov-Database has modified its data, let's fetch it all and update our data
            if (currentMetadata.IsNull() || newMetadata.Modified > currentMetadata.Modified)
            {
                await UpdateAllItemAndPriceData(tarkovDatabaseToken);

                var documentId = currentMetadata?.Id;
                currentMetadata.CopyFrom(newMetadata);
                currentMetadata.Id = documentId;

                _itemRepository.StoreItemKindsMetadata(currentMetadata, saveChanges: true);
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
            _itemRepository.IncreaseMaxNumberOfRequestsPerSession(20);

            #region Fetch items from our database
            List<Ammunition> ammunitions = _itemRepository.GetItemsByKind<Ammunition>();
            List<Armor> armors = _itemRepository.GetItemsByKind<Armor>();
            List<Backpack> backpacks = _itemRepository.GetItemsByKind<Backpack>();
            List<Barter> barters = _itemRepository.GetItemsByKind<Barter>();
            List<Clothing> clothings = _itemRepository.GetItemsByKind<Clothing>();
            List<Common> commons = _itemRepository.GetItemsByKind<Common>();
            List<Container> containers = _itemRepository.GetItemsByKind<Container>();
            List<Firearm> firearms = _itemRepository.GetItemsByKind<Firearm>();
            List<Food> foods = _itemRepository.GetItemsByKind<Food>();
            List<Grenade> grenades = _itemRepository.GetItemsByKind<Grenade>();
            List<Headphone> headphones = _itemRepository.GetItemsByKind<Headphone>();
            List<Key> keys = _itemRepository.GetItemsByKind<Key>();
            List<Magazine> magazines = _itemRepository.GetItemsByKind<Magazine>();
            List<Map> maps = _itemRepository.GetItemsByKind<Map>();
            List<Medical> medicals = _itemRepository.GetItemsByKind<Medical>();
            List<Melee> melees = _itemRepository.GetItemsByKind<Melee>();
            List<Modification> modifications = _itemRepository.GetItemsByKind<Modification>();
            List<ModificationBarrel> modificationBarrels = _itemRepository.GetItemsByKind<ModificationBarrel>();
            List<ModificationBipod> modificationBipods = _itemRepository.GetItemsByKind<ModificationBipod>();
            List<ModificationCharge> modificationCharges = _itemRepository.GetItemsByKind<ModificationCharge>();
            List<ModificationDevice> modificationDevices = _itemRepository.GetItemsByKind<ModificationDevice>();
            List<ModificationForegrip> modificationForegrips = _itemRepository.GetItemsByKind<ModificationForegrip>();
            List<ModificationGasblock> modificationGasblocks = _itemRepository.GetItemsByKind<ModificationGasblock>();
            List<ModificationGoggles> modificationGoggles = _itemRepository.GetItemsByKind<ModificationGoggles>();
            List<ModificationHandguard> modificationHandguards = _itemRepository.GetItemsByKind<ModificationHandguard>();
            List<ModificationLauncher> modificationLaunchers = _itemRepository.GetItemsByKind<ModificationLauncher>();
            List<ModificationMount> modificationMounts = _itemRepository.GetItemsByKind<ModificationMount>();
            List<ModificationMuzzle> modificationMuzzles = _itemRepository.GetItemsByKind<ModificationMuzzle>();
            List<ModificationPistolgrip> modificationPistolgrips = _itemRepository.GetItemsByKind<ModificationPistolgrip>();
            List<ModificationReceiver> modificationReceivers = _itemRepository.GetItemsByKind<ModificationReceiver>();
            List<ModificationSight> modificationSights = _itemRepository.GetItemsByKind<ModificationSight>();
            List<ModificationSightSpecial> modificationSightSpecials = _itemRepository.GetItemsByKind<ModificationSightSpecial>();
            List<ModificationStock> modificationStocks = _itemRepository.GetItemsByKind<ModificationStock>();
            List<Money> moneys = _itemRepository.GetItemsByKind<Money>();
            List<Tacticalrig> tacticalrigs = _itemRepository.GetItemsByKind<Tacticalrig>();
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

            _itemRepository.SaveChanges();
        }

        private void CreateUpdateDeleteItems<T>(IEnumerable<T> items) where T : IItem
        {
            var bsgIds = items.Select(x => x.BsgId).ToList();
            _itemRepository.IncreaseMaxNumberOfRequestsPerSession(1); // Naughty, but this will only happen for as many different item kinds there are
            var existingItems = _itemRepository.GetItemsByBsgIds<T>(bsgIds);

            // Update existing items
            for (var i = 0; i < existingItems.Count; i++)
            {
                var item = items.Where(x => x.BsgId == existingItems[i].BsgId).FirstOrDefault();
                if (item.IsNotNull())
                {
                    var documentId = existingItems[i].Id;
                    existingItems[i].CopyFrom(item);

                    existingItems[i].Id = documentId;
                    _itemRepository.StoreItem(existingItems[i]);
                    _itemRepository.AddMarketPriceTimeSeries(existingItems[i]);
                }
            }

            // Create new items
            var existingItemsBsgIds = existingItems.Select(x => x.BsgId).ToList();
            foreach (var item in items)
            {
                if (!existingItemsBsgIds.Contains(item.BsgId))
                {
                    _itemRepository.StoreItem(item);
                    _itemRepository.AddMarketPriceTimeSeries(item);
                }
            }

            // Delete any items BSG has deleted (don't know if they do this but just in case)
            var itemsBsgIds = items.Select(x => x.BsgId).ToList();
            foreach (var existingItem in existingItems)
            {
                if (!itemsBsgIds.Contains(existingItem.BsgId))
                {
                    _itemRepository.DeleteItem(existingItem);
                }
            }
        }

        private IEnumerable<T> CombineItemsData<T>(IEnumerable<T> items, IEnumerable<TarkovToolsItem> tarkovToolsItems) where T : IItem
        {
            foreach (var item in items)
            {
                var tarkovToolsItem = tarkovToolsItems.Where(x => x.BsgId == item.BsgId).FirstOrDefault();
                if (tarkovToolsItem.IsNotNull())
                {
                    item.Avg24hPrice = tarkovToolsItem.Avg24hPrice;
                    item.LastLowPrice = tarkovToolsItem.LastLowPrice;
                    item.ChangeLast48h = tarkovToolsItem.ChangeLast48h;
                    item.Low24hPrice = tarkovToolsItem.Low24hPrice;
                    item.High24hPrice = tarkovToolsItem.High24hPrice;
                    item.Icon = tarkovToolsItem.IconLink ?? "";
                    item.Img = tarkovToolsItem.IconLink ?? "";
                    item.ImgBig = tarkovToolsItem.ImageLink ?? "";
                    item.GridImg = tarkovToolsItem.GridImageLink ?? "";
                    item.WikiLink = tarkovToolsItem.WikiLink;

                    item.SellToTraderPrices = new List<Models.Items.SellToTraderPrice>();
                    foreach (var sellToTraderPrice in tarkovToolsItem.SellToTraderPrices)
                    {
                        item.SellToTraderPrices.Add(new Models.Items.SellToTraderPrice
                        {
                            Price = sellToTraderPrice.Price.IsNotNull() ? sellToTraderPrice.Price : 0,
                            Trader = new Models.Items.Trader
                            {
                                BsgId = sellToTraderPrice.Trader.BsgId,
                                Name = sellToTraderPrice.Trader.Name
                            }
                        });
                    }
                }
            }

            return items;
        }
    }
}
