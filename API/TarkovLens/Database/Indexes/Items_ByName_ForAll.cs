using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Indexes
{
    public class Items_ByName_ForAll : AbstractMultiMapIndexCreationTask
    {
        public Items_ByName_ForAll()
        {
            AddMapForAll<BaseItem>(items => from i in items select new { i.Name });
        }
    }
}
