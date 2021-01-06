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

namespace TarkovLens.Services.Item
{
    public interface IItemService
    {
        public IItem GetItemById(string id);
        public List<IItem> GetItemsByName(string name);
        public List<T> GetItemsByKindAndName<T>(string name) where T : IItem;
        public List<Ammunition> GetAmmunitionByCaliber(string caliber, string name);
    }

    public class ItemService : IItemService
    {
        private readonly IDocumentSession session;

        public ItemService(IDocumentSession documentSession)
        {
            session = documentSession;
        }

        public IItem GetItemById(string id)
        {
            var item = session.Load<IItem>(id);
            if (item.IsNotNull())
                session.Advanced.IgnoreChangesFor(item);

            return item;
        }

        public List<IItem> GetItemsByName(string name)
        {
            var query = session.Query<IItem, Items_ByName_ForAll>();

            var words = name.Split().Select(x => x).ToList();
            foreach (var word in words)
            {
                query = query.Search(x => x.Name, $"*{word}*", options: SearchOptions.And);
            }

            var items = query.ToList();
            return items;
        }
        
        /// <summary>
        /// Get items from a particular collection.
        /// </summary>
        /// <typeparam name="T">The collection to query.</typeparam>
        /// <param name="name">Optional: filter the items by name.</param>
        /// <returns>A list of items from the specified collection.</returns>
        public List<T> GetItemsByKindAndName<T>(string name = null) where T : IItem
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
                var words = name.Split().Select(x => x).ToList();
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
