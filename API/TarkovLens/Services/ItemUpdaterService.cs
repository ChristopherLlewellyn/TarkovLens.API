using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Models.Services.TarkovMarket;

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
            List<Firearm> firearms = await _tarkovDatabaseService.GetItemsByKindAsync<Firearm>(token, KindOfItem.Firearm);
            #endregion

            #region Fetch items from Tarkov-Market and unify the data
            List<TarkovMarketItem> tarkovMarketItems = await _tarkovMarketService.GetAllItemsAsync();

            ammunitions = MapItemsData(ammunitions, tarkovMarketItems).ToList();
            armors = MapItemsData(armors, tarkovMarketItems).ToList();
            backpacks = MapItemsData(backpacks, tarkovMarketItems).ToList();
            barters = MapItemsData(barters, tarkovMarketItems).ToList();
            clothings = MapItemsData(clothings, tarkovMarketItems).ToList();
            firearms = MapItemsData(firearms, tarkovMarketItems).ToList();
            #endregion

            #region Create and update items
            CreateOrUpdateItems(ammunitions);
            CreateOrUpdateItems(armors);
            CreateOrUpdateItems(backpacks);
            CreateOrUpdateItems(barters);
            CreateOrUpdateItems(clothings);
            CreateOrUpdateItems(firearms);
            #endregion

            session.SaveChanges();
        }

        private void CreateOrUpdateItems<T>(IEnumerable<T> items) where T : IItem
        {
            var bsgIds = items.Select(x => x.BsgId).ToList();
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
                var marketItem = tarkovMarketItems.Where(x => x.BsgId == tarkovDatabaseItem.BsgId).FirstOrDefault();
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
