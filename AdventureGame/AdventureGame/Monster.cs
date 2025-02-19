using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    /// <summary>
    /// Class for monsters.
    /// </summary>
    internal class Monster : ICharacter
    {
        /// <summary>
        /// Monster's health property
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Monster()
        {
            Health = 60;
        }

        /// <summary>
        /// Damages the monster by whatever number healthRecovered is when this method is called.
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        public string TakeDamage(int damageTaken, string damageType)
        {
            // If damage taken was greater than the monster's health, just set it to 0.
            if (Health < damageTaken)
            {
                Health = 0;
            }
            // If not, just take that much health away.
            else
            {
                Health -= damageTaken;
            }
            // Return a message.
            return $"It took {damageTaken} damage. It is now at {Health} HP!";
        }
    }
}
