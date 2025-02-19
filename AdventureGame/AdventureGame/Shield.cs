using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    /// <summary>
    /// Class for shields.
    /// </summary>
    internal class Shield : Item
    {
        /// <summary>
        /// Shield's uses property
        /// </summary>
        public int Uses { get; set; }
        /// <summary>
        ///  Shield Constructor
        /// </summary>
        /// <param name="uses"></param>
        public Shield(int uses)
        {
            Uses = uses;
            ItemName = "Shield";
        }

        /// <summary>
        /// Method that damages the shield when it's called
        /// </summary>
        /// <returns></returns>
        public string Damage()
        {
            // Take a use away and return a message depending on how many uses it has left.
            if (Uses > 0)
            {
                Uses--;
                return "and your shield was damaged.";
            }
            else
            {
                return "and your shield broke!";
            }
        }
    }
}
