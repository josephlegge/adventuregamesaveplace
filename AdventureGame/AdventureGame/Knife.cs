using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    /// <summary>
    /// Class for the old knife the player spawns with.
    /// </summary>
    internal class Knife : Weapon
    {
        /// <summary>
        /// Knife Constuctor (You start out with this weapon.
        /// </summary>
        /// <param name="damage"></param>
        public Knife()
        {
            ItemName = "Old Knife";
            Damage = 4;
        }
    }
}
