using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Sword : Weapon
    {

        /// <summary>
        /// Sword Constructor
        /// </summary>
        /// <param name="damage"></param>
        public Sword(int damage)
        {
            ItemName = "Sword";
            Damage = damage;
        }
    }
}
