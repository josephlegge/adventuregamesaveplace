using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{


    internal interface ICharacter
    {

        /// <summary>
        /// The health property.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Method that damages a character.
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        string TakeDamage(int damageTaken, string damageType);

    }
}
