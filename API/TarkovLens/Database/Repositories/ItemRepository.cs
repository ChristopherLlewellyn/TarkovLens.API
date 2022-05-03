using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Enums;
using TarkovLens.Helpers;
using TarkovLens.Helpers.ExtensionMethods;
using TarkovLens.Indexes;
using TarkovLens.Interfaces;
using TarkovLens.Models.Items;

namespace TarkovLens.Services.Item
{
    public interface IItemRepository
    {
        public List<IItem> GetAllItems();
        public IItem GetItemById(string id);
        public IItem GetItemByBsgId(string bsgId);
        public List<BaseItem> GetItemsByName(string name);
        public List<T> GetItemsByKind<T>() where T : IItem;
        public List<T> GetItemsByKindAndName<T>(string name) where T : IItem;
        public List<Ammunition> GetAmmunitionByCaliber(string caliber, string name);
    }

    public class ItemRepository : IItemRepository
    {
        private readonly IDocumentSession session;

        public ItemRepository(IDocumentSession documentSession)
        {
            session = documentSession;
        }

        public List<IItem> GetAllItems()
        {
            return session.Query<IItem, Items_ByBsgId>().ToList();
        }

        public IItem GetItemById(string id)
        {
            var item = session.Load<IItem>(id);
            if (item.IsNotNull())
                session.Advanced.IgnoreChangesFor(item);

            return item;
        }

        public IItem GetItemByBsgId(string bsgId)
        {
            var item = session
                .Query<IItem, Items_ByBsgId>()
                .Where(x => x.BsgId == bsgId)
                .FirstOrDefault();
            return item;
        }

        public List<BaseItem> GetItemsByName(string name)
        {
            var query = session.Query<BaseItem, Items_ByName_ForAll>();

            var words = name.Split().Select(x => x).ToList();
            foreach (var word in words)
            {
                query = query.Search(x => x.Name, $"*{word}*", options: SearchOptions.And);
            }

            var items = query.ToList();
            items = items.OrderBy(x => x.Name.Length).ToList();
            return items;
        }

        public List<T> GetItemsByKind<T>() where T : IItem
        {
            List<T> items = session.Query<T>().ToList();
            return items;
        }

        /// <summary>
        /// Get items from a particular collection.
        /// </summary>
        /// <typeparam name="T">The collection to query.</typeparam>
        /// <param name="name">Filter the items by name.</param>
        /// <returns>A list of items from the specified collection.</returns>
        public List<T> GetItemsByKindAndName<T>(string name) where T : IItem
        {
            var itemsQuery = session.Query<T>();

            if (name.IsNotNull())
            {
                var words = name.Split().Select(x => x).ToList();
                foreach (var word in words)
                {
                    itemsQuery = itemsQuery.Search(x => x.Name, $"*{word}*", options: SearchOptions.And);
                }
            }

            List<T> items = itemsQuery.ToList();
            return items;
        }

        public List<Ammunition> GetAmmunitionByCaliber(string caliber, string name = null)
        {
            var query = session.Query<Ammunition>();

            if (caliber.IsNotNullOrEmpty())
            {
                var words = caliber.Split().Select(x => x).ToList();
                foreach (var word in words)
                {
                    query = query.Search(x => x.Caliber, $"*{caliber}*", options: SearchOptions.And);
                }
            }

            if (name.IsNotNullOrEmpty())
            {
                var words = name.Split().Select(x => x).ToList();
                foreach (var word in words)
                {
                    query = query.Search(x => x.Name, $"*{word}*", options: SearchOptions.And);
                }
            }

            var ammunitions = query.ToList();
            return ammunitions;
        }
    }
}
