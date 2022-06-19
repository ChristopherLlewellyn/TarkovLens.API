using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Database.Repositories
{
    public interface IRavenRepository
    {
        public void IncreaseMaxNumberOfRequestsPerSession(int increase);
        public void SaveChanges();
    }
}
