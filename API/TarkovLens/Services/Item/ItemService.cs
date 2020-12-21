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
            var items = session
                    .Query<IItem, Items_ByName_ForAll>()
                    .Search(x => x.Name, $"*{name}*") // The stars (*) on either side mean it can match anywhere in the string
                    .ToList();

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
                itemsQuery = itemsQuery.Search(x => x.Name, $"*{name}*");
            }

            List<T> items = itemsQuery.ToList();
            return items;
        }
    }
}
