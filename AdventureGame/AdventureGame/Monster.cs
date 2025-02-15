using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Monster : ICharacter
    {
        public int Health { get; set; }

        /// <summary>
        /// Damages the monster by whatever number healthRecovered is when this method is called.
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        public string TakeDamage(int damageTaken, string damageType)
        {
            if (Health < damageTaken)
            {
                Health = 0;
            }
            else
            {
                Health -= damageTaken;
            }
            return $"It took {damageTaken} damage. It is now at {Health} HP!";
        }
    }
}
