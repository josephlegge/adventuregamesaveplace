using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Player : ICharacter
    {

        private int health_;
        public int Health { get; set; }



        public string Heal(int healthRecovered)
        {
            if (healthRecovered + Health >= 100)
            {
                Console.WriteLine("Test");
                Health = 100;
                healthRecovered = 100 - healthRecovered;

            }
            else
            {
                Console.WriteLine("Test2");
                Health += healthRecovered;
            }
            return $"Recovered {healthRecovered} health! You are now at {Health} HP!";
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
            return $"Took {damageTaken} damage! You are now at {Health} HP!";
        }


    }
}
