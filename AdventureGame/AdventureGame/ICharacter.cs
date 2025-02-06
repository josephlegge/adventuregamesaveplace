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
        /// Method that heals a character.
        /// </summary>
        /// <param name="healthRecovered"></param>
        /// <returns></returns>
        string Heal(int healthRecovered);
        /// <summary>
        /// Method that damages a character.
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        string TakeDamage(int damageTaken);

    }
}
