using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Interfaces
{
    public interface ICharacter
    {
        public string Id { get; set; }
        public CharacterType Type { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public string Portrait { get; set; }
        public Location Location { get; set; }
    }
}
