using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Models.Characters;

namespace TarkovLens.Interfaces
{
    public interface ICombatant
    {
        public HealthStatus HealthStatus { get; set; }
    }
}
