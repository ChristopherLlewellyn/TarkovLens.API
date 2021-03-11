using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Models.Characters
{
    public class BodyPart
    {
        public BodyPart(int maxHp, bool isVital)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
            IsVital = isVital;
        }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public bool IsVital { get; set; }
    }
}
