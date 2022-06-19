using Hangfire;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Interfaces;
using TarkovLens.Services.Item;
using TarkovLens.Services.TarkovDatabase;
using TarkovLens.Services.TarkovDev;

namespace TarkovLens.Services
{
    public interface IItemUpdaterService
    {
        public Task UpdateItemsTask();
    }

    public class ItemUpdaterService : IItemUpdaterService
    {
        private ITarkovDatabaseService _tarkovDatabaseService;
        private ITarkovDevService _tarkovDevService;
        private IItemRepository _itemRepository;

        public ItemUpdaterService(ITarkovDatabaseService tarkovDatabaseService,
                              ITarkovDevService tarkovDevService,
                              IItemRepository itemRepository)
        {
            _tarkovDatabaseService = tarkovDatabaseService;
            _tarkovDevService = tarkovDevService;
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
                await UpdateItems(tarkovDatabaseToken, updateAdditionalDataOnly: false);

                var documentId = currentMetadata?.Id;
                currentMetadata.CopyFrom(newMetadata);
                currentMetadata.Id = documentId;

                _itemRepository.StoreItemKindsMetadata(currentMetadata, saveChanges: true);
            }
            else
            {
                await UpdateItems(null, updateAdditionalDataOnly: true);
            }
        }

        private async Task UpdateItems(string tarkovDatabaseToken, bool updateAdditionalDataOnly)
        {
            List<Ammunition> ammunitions = new List<Ammunition>();
            List<Armor> armors = new List<Armor>();
            List<Backpack> backpacks = new List<Backpack>();
            List<Barter> barters = new List<Barter>();
            List<Clothing> clothings = new List<Clothing>();
            List<Common> commons = new List<Common>();
            List<Container> containers = new List<Container>();
            List<Firearm> firearms = new List<Firearm>();
            List<Food> foods = new List<Food>();
            List<Grenade> grenades = new List<Grenade>();
            List<Headphone> headphones = new List<Headphone>();
            List<Key> keys = new List<Key>();
            List<Magazine> magazines = new List<Magazine>();
            List<Map> maps = new List<Map>();
            List<Medical> medicals = new List<Medical>();
            List<Melee> melees = new List<Melee>();
            List<Modification> modifications = new List<Modification>();
            List<ModificationBarrel> modificationBarrels = new List<ModificationBarrel>();
            List<ModificationBipod> modificationBipods = new List<ModificationBipod>();
            List<ModificationCharge> modificationCharges = new List<ModificationCharge>();
            List<ModificationDevice> modificationDevices = new List<ModificationDevice>();
            List<ModificationForegrip> modificationForegrips = new List<ModificationForegrip>();
            List<ModificationGasblock> modificationGasblocks = new List<ModificationGasblock>();
            List<ModificationGoggles> modificationGoggles = new List<ModificationGoggles>();
            List<ModificationHandguard> modificationHandguards = new List<ModificationHandguard>();
            List<ModificationLauncher> modificationLaunchers = new List<ModificationLauncher>();
            List<ModificationMount> modificationMounts = new List<ModificationMount>();
            List<ModificationMuzzle> modificationMuzzles = new List<ModificationMuzzle>();
            List<ModificationPistolgrip> modificationPistolgrips = new List<ModificationPistolgrip>();
            List<ModificationReceiver> modificationReceivers = new List<ModificationReceiver>();
            List<ModificationSight> modificationSights = new List<ModificationSight>();
            List<ModificationSightSpecial> modificationSightSpecials = new List<ModificationSightSpecial>();
            List<ModificationStock> modificationStocks = new List<ModificationStock>();
            List<Money> moneys = new List<Money>();
            List<Tacticalrig> tacticalrigs = new List<Tacticalrig>();

            if (updateAdditionalDataOnly)
            {
                #region Fetch items from our database
                _itemRepository.IncreaseMaxNumberOfRequestsPerSession(30);

                ammunitions = _itemRepository.GetItemsByKind<Ammunition>();
                armors = _itemRepository.GetItemsByKind<Armor>();
                backpacks = _itemRepository.GetItemsByKind<Backpack>();
                barters = _itemRepository.GetItemsByKind<Barter>();
                clothings = _itemRepository.GetItemsByKind<Clothing>();
                commons = _itemRepository.GetItemsByKind<Common>();
                containers = _itemRepository.GetItemsByKind<Container>();
                firearms = _itemRepository.GetItemsByKind<Firearm>();
                foods = _itemRepository.GetItemsByKind<Food>();
                grenades = _itemRepository.GetItemsByKind<Grenade>();
                headphones = _itemRepository.GetItemsByKind<Headphone>();
                keys = _itemRepository.GetItemsByKind<Key>();
                magazines = _itemRepository.GetItemsByKind<Magazine>();
                maps = _itemRepository.GetItemsByKind<Map>();
                medicals = _itemRepository.GetItemsByKind<Medical>();
                melees = _itemRepository.GetItemsByKind<Melee>();
                modifications = _itemRepository.GetItemsByKind<Modification>();
                modificationBarrels = _itemRepository.GetItemsByKind<ModificationBarrel>();
                modificationBipods = _itemRepository.GetItemsByKind<ModificationBipod>();
                modificationCharges = _itemRepository.GetItemsByKind<ModificationCharge>();
                modificationDevices = _itemRepository.GetItemsByKind<ModificationDevice>();
                modificationForegrips = _itemRepository.GetItemsByKind<ModificationForegrip>();
                modificationGasblocks = _itemRepository.GetItemsByKind<ModificationGasblock>();
                modificationGoggles = _itemRepository.GetItemsByKind<ModificationGoggles>();
                modificationHandguards = _itemRepository.GetItemsByKind<ModificationHandguard>();
                modificationLaunchers = _itemRepository.GetItemsByKind<ModificationLauncher>();
                modificationMounts = _itemRepository.GetItemsByKind<ModificationMount>();
                modificationMuzzles = _itemRepository.GetItemsByKind<ModificationMuzzle>();
                modificationPistolgrips = _itemRepository.GetItemsByKind<ModificationPistolgrip>();
                modificationReceivers = _itemRepository.GetItemsByKind<ModificationReceiver>();
                modificationSights = _itemRepository.GetItemsByKind<ModificationSight>();
                modificationSightSpecials = _itemRepository.GetItemsByKind<ModificationSightSpecial>();
                modificationStocks = _itemRepository.GetItemsByKind<ModificationStock>();
                moneys = _itemRepository.GetItemsByKind<Money>();
                tacticalrigs = _itemRepository.GetItemsByKind<Tacticalrig>();
                #endregion
            }
            else
            {
                #region Fetch items from Tarkov-Database
                ammunitions = await _tarkovDatabaseService.GetItemsByKindAsync<Ammunition>(tarkovDatabaseToken, KindOfItem.Ammunition);
                armors = await _tarkovDatabaseService.GetItemsByKindAsync<Armor>(tarkovDatabaseToken, KindOfItem.Armor);
                backpacks = await _tarkovDatabaseService.GetItemsByKindAsync<Backpack>(tarkovDatabaseToken, KindOfItem.Backpack);
                barters = await _tarkovDatabaseService.GetItemsByKindAsync<Barter>(tarkovDatabaseToken, KindOfItem.Barter);
                clothings = await _tarkovDatabaseService.GetItemsByKindAsync<Clothing>(tarkovDatabaseToken, KindOfItem.Clothing);
                commons = await _tarkovDatabaseService.GetItemsByKindAsync<Common>(tarkovDatabaseToken, KindOfItem.Common);
                containers = await _tarkovDatabaseService.GetItemsByKindAsync<Container>(tarkovDatabaseToken, KindOfItem.Container);
                firearms = await _tarkovDatabaseService.GetItemsByKindAsync<Firearm>(tarkovDatabaseToken, KindOfItem.Firearm);
                foods = await _tarkovDatabaseService.GetItemsByKindAsync<Food>(tarkovDatabaseToken, KindOfItem.Food);
                grenades = await _tarkovDatabaseService.GetItemsByKindAsync<Grenade>(tarkovDatabaseToken, KindOfItem.Grenade);
                headphones = await _tarkovDatabaseService.GetItemsByKindAsync<Headphone>(tarkovDatabaseToken, KindOfItem.Headphone);
                keys = await _tarkovDatabaseService.GetItemsByKindAsync<Key>(tarkovDatabaseToken, KindOfItem.Key);
                magazines = await _tarkovDatabaseService.GetItemsByKindAsync<Magazine>(tarkovDatabaseToken, KindOfItem.Magazine);
                maps = await _tarkovDatabaseService.GetItemsByKindAsync<Map>(tarkovDatabaseToken, KindOfItem.Map);
                medicals = await _tarkovDatabaseService.GetItemsByKindAsync<Medical>(tarkovDatabaseToken, KindOfItem.Medical);
                melees = await _tarkovDatabaseService.GetItemsByKindAsync<Melee>(tarkovDatabaseToken, KindOfItem.Melee);
                modifications = await _tarkovDatabaseService.GetItemsByKindAsync<Modification>(tarkovDatabaseToken, KindOfItem.Modification);
                modificationBarrels = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBarrel>(tarkovDatabaseToken, KindOfItem.ModificationBarrel);
                modificationBipods = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationBipod>(tarkovDatabaseToken, KindOfItem.ModificationBipod);
                modificationCharges = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationCharge>(tarkovDatabaseToken, KindOfItem.ModificationCharge);
                modificationDevices = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationDevice>(tarkovDatabaseToken, KindOfItem.ModificationDevice);
                modificationForegrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationForegrip>(tarkovDatabaseToken, KindOfItem.ModificationForegrip);
                modificationGasblocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGasblock>(tarkovDatabaseToken, KindOfItem.ModificationGasblock);
                modificationGoggles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationGoggles>(tarkovDatabaseToken, KindOfItem.ModificationGoggles);
                modificationHandguards = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationHandguard>(tarkovDatabaseToken, KindOfItem.ModificationHandguard);
                modificationLaunchers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationLauncher>(tarkovDatabaseToken, KindOfItem.ModificationLauncher);
                modificationMounts = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMount>(tarkovDatabaseToken, KindOfItem.ModificationMount);
                modificationMuzzles = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationMuzzle>(tarkovDatabaseToken, KindOfItem.ModificationMuzzle);
                modificationPistolgrips = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationPistolgrip>(tarkovDatabaseToken, KindOfItem.ModificationPistolgrip);
                modificationReceivers = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationReceiver>(tarkovDatabaseToken, KindOfItem.ModificationReceiver);
                modificationSights = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSight>(tarkovDatabaseToken, KindOfItem.ModificationSight);
                modificationSightSpecials = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationSightSpecial>(tarkovDatabaseToken, KindOfItem.ModificationSightSpecial);
                modificationStocks = await _tarkovDatabaseService.GetItemsByKindAsync<ModificationStock>(tarkovDatabaseToken, KindOfItem.ModificationStock);
                moneys = await _tarkovDatabaseService.GetItemsByKindAsync<Money>(tarkovDatabaseToken, KindOfItem.Money);
                tacticalrigs = await _tarkovDatabaseService.GetItemsByKindAsync<Tacticalrig>(tarkovDatabaseToken, KindOfItem.Tacticalrig);
                #endregion
            }

            #region Fetch prices, images, etc (additional data) from Tarkov-Tools and unify
            List<TarkovDevItem> itemsWithAdditionalData = await _tarkovDevService.GetAllItemsAsync();

            ammunitions = CombineItemsData(ammunitions, itemsWithAdditionalData).ToList();
            armors = CombineItemsData(armors, itemsWithAdditionalData).ToList();
            backpacks = CombineItemsData(backpacks, itemsWithAdditionalData).ToList();
            barters = CombineItemsData(barters, itemsWithAdditionalData).ToList();
            clothings = CombineItemsData(clothings, itemsWithAdditionalData).ToList();
            commons = CombineItemsData(commons, itemsWithAdditionalData).ToList();
            containers = CombineItemsData(containers, itemsWithAdditionalData).ToList();
            firearms = CombineItemsData(firearms, itemsWithAdditionalData).ToList();
            foods = CombineItemsData(foods, itemsWithAdditionalData).ToList();
            grenades = CombineItemsData(grenades, itemsWithAdditionalData).ToList();
            headphones = CombineItemsData(headphones, itemsWithAdditionalData).ToList();
            keys = CombineItemsData(keys, itemsWithAdditionalData).ToList();
            magazines = CombineItemsData(magazines, itemsWithAdditionalData).ToList();
            maps = CombineItemsData(maps, itemsWithAdditionalData).ToList();
            medicals = CombineItemsData(medicals, itemsWithAdditionalData).ToList();
            melees = CombineItemsData(melees, itemsWithAdditionalData).ToList();
            modifications = CombineItemsData(modifications, itemsWithAdditionalData).ToList();
            modificationBarrels = CombineItemsData(modificationBarrels, itemsWithAdditionalData).ToList();
            modificationBipods = CombineItemsData(modificationBipods, itemsWithAdditionalData).ToList();
            modificationCharges = CombineItemsData(modificationCharges, itemsWithAdditionalData).ToList();
            modificationDevices = CombineItemsData(modificationDevices, itemsWithAdditionalData).ToList();
            modificationForegrips = CombineItemsData(modificationForegrips, itemsWithAdditionalData).ToList();
            modificationGasblocks = CombineItemsData(modificationGasblocks, itemsWithAdditionalData).ToList();
            modificationGoggles = CombineItemsData(modificationGoggles, itemsWithAdditionalData).ToList();
            modificationHandguards = CombineItemsData(modificationHandguards, itemsWithAdditionalData).ToList();
            modificationLaunchers = CombineItemsData(modificationLaunchers, itemsWithAdditionalData).ToList();
            modificationMounts = CombineItemsData(modificationMounts, itemsWithAdditionalData).ToList();
            modificationMuzzles = CombineItemsData(modificationMuzzles, itemsWithAdditionalData).ToList();
            modificationPistolgrips = CombineItemsData(modificationPistolgrips, itemsWithAdditionalData).ToList();
            modificationReceivers = CombineItemsData(modificationReceivers, itemsWithAdditionalData).ToList();
            modificationSights = CombineItemsData(modificationSights, itemsWithAdditionalData).ToList();
            modificationSightSpecials = CombineItemsData(modificationSightSpecials, itemsWithAdditionalData).ToList();
            modificationStocks = CombineItemsData(modificationStocks, itemsWithAdditionalData).ToList();
            moneys = CombineItemsData(moneys, itemsWithAdditionalData).ToList();
            tacticalrigs = CombineItemsData(tacticalrigs, itemsWithAdditionalData).ToList();
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

        private IEnumerable<T> CombineItemsData<T>(IEnumerable<T> items, IEnumerable<TarkovDevItem> itemsWithAdditionalData) where T : IItem
        {
            foreach (var item in items)
            {
                var itemWithAdditionalData = itemsWithAdditionalData.Where(x => x.BsgId == item.BsgId).FirstOrDefault();
                if (itemWithAdditionalData.IsNotNull())
                {
                    item.Avg24hPrice = itemWithAdditionalData.Avg24hPrice;
                    item.LastLowPrice = itemWithAdditionalData.LastLowPrice;
                    item.ChangeLast48h = itemWithAdditionalData.ChangeLast48h;
                    item.ChangeLast48hPercent = itemWithAdditionalData.ChangeLast48hPercent;
                    item.Low24hPrice = itemWithAdditionalData.Low24hPrice;
                    item.High24hPrice = itemWithAdditionalData.High24hPrice;
                    item.Icon = itemWithAdditionalData.IconLink ?? "";
                    item.Img = itemWithAdditionalData.IconLink ?? "";
                    item.ImgBig = itemWithAdditionalData.ImageLink ?? "";
                    item.GridImg = itemWithAdditionalData.GridImageLink ?? "";
                    item.WikiLink = itemWithAdditionalData.WikiLink;

                    item.SellToTraderPrices = new List<Models.Items.TraderPrice>();
                    foreach (var sellToTraderPrice in itemWithAdditionalData.SellFor)
                    {
                        item.SellToTraderPrices.Add(new Models.Items.TraderPrice
                        {
                            Price = sellToTraderPrice.Price,
                            Currency = sellToTraderPrice.Currency,
                            Trader = new Models.Items.Trader
                            {
                                Name = sellToTraderPrice.Vendor.Name
                            }
                        });
                    }

                    item.BuyFromTraderPrices = new List<Models.Items.TraderPrice>();
                    foreach (var buyFromTraderPrice in itemWithAdditionalData.BuyFor)
                    {
                        item.BuyFromTraderPrices.Add(new Models.Items.TraderPrice
                        {
                            Price = buyFromTraderPrice.Price,
                            Currency = buyFromTraderPrice.Currency,
                            Trader = new Models.Items.Trader
                            {
                                Name = buyFromTraderPrice.Vendor.Name
                            }
                        });
                    }
                }
            }

            return items;
        }
    }
}
