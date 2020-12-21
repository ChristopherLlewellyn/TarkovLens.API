using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Items;
using TarkovLens.Models.Items;

namespace TarkovLens.Indexes
{
    public class Item_Smart_Search : AbstractMultiMapIndexCreationTask<Item_Smart_Search.Result>
    {
        public class Result
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Image { get; set; }

            public object Collection { get; set; }

            public string[] Content { get; set; }
        }

        public class Projection
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Image { get; set; }

            public string Collection { get; set; }
        }

        public Item_Smart_Search()
        {
            #region Maps
            AddMapForAll<BaseItem>(items => from i in items
                                         select new Result
                                         {
                                             Id = i.Id,
                                             Content = new[]
                                             {
                                                 i.Name
                                             },
                                             Name = i.Name,
                                             Image = i.Img,
                                             Collection = MetadataFor(i)["@collection"]
                                         });
            #endregion

            // mark 'Content' field as analyzed which enables full text search operations
            Index(x => x.Content, FieldIndexing.Search);

            // storing fields so when projection (e.g. ProjectInto)
            // requests only those fields
            // then data will come from index only, not from storage
            Store(x => x.Id, FieldStorage.Yes);
            Store(x => x.Name, FieldStorage.Yes);
            Store(x => x.Image, FieldStorage.Yes);
            Store(x => x.Collection, FieldStorage.Yes);
        }
    }
}
