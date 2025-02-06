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
            return "Ow";
        }
    }
}
