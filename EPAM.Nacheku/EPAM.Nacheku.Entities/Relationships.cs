using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Entities
{
    public struct Relationships
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public RelationType Relation { get; set; }
        public bool Confirmed { get; set; }
    }

    [Flags]
    public enum RelationType : byte
    {
        Friend = 0,
        Cousin = 2,
        Kin = 4, //родня
        Buddy = 8,//пердун(дружище)
        BuddyBuddy = 16,//вась-вась
        Familiar = 32,
        Pal = 64,//собутыльник
        Mate = 128//товарищ
    }
}
