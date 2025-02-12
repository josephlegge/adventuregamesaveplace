using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Monster : ICharacter
    {
        private int health_;
        public int Health { get; set; }

        public string Heal(int healthRecovered)
        {
            return "yay";
        }

        public string TakeDamage(int damageTaken)
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
