using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Documents.Characters;

namespace TarkovLens.Indexes
{
    public class Characters_ByType : AbstractMultiMapIndexCreationTask
    {
        public Characters_ByType()
        {
            AddMap<Combatant>(combatants => from c in combatants select new { c.Type });

            AddMap<Trader>(traders => from t in traders select new { t.Type });
        }
    }
}
