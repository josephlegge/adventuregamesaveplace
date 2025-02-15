
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal abstract class Potion : Item
    {

        /// <summary>
        /// The strength of the potion.
        /// </summary>
        public int Strength { get; set; }

        public virtual string AffectPlayer(Player player)
        {
            return $"You drink a bottle of water.";
        }


    }   
}
