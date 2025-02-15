using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Knife : Weapon
    {
        public Knife(int damage)
        {
            ItemName = "Old Knife";
            Damage = damage;
        }
    }
}
