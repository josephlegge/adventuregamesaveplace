using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal abstract class Potion
    {

        /// <summary>
        /// The strength of the potion.
        /// </summary>
        public int PotionIntensity { get; set; }

        public virtual string AffectPlayer(Player player)
        {
            return "E";
        }


    }
}
