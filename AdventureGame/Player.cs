using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Player : ICharacter
    {

        private int health_;
        public int Health { get; set; }
        public Weapon PlayerWeapon { get; set; }

        public Player(int health)
        {
            Health = health;
        }
        
        /// <summary>
        /// Heals the player by whatever number healthRecovered is when this method is called.
        /// </summary>
        /// <param name="healthRecovered"></param>
        /// <returns></returns>
        public string Heal(int healthRecovered)
        {
            string healthMessage = string.Empty;
            if (healthRecovered + Health >= 100)
            {
                healthRecovered = 100 - Health;

                Health = 100;
                healthMessage = $"r health is capped at 100.";

            }
            else
            {
                Health += healthRecovered;
                healthMessage = $"r health is now {Health}.";

            }
            return $"Recovered {healthRecovered} health. You{healthMessage}";
        }

        /// <summary>
        /// Damages the player by whatever number healthRecovered is when this method is called.
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        public string TakeDamage(int damageTaken)
        {
            string healthMessage = string.Empty;
            if (Health < damageTaken)
            {
                Health = 0;
                healthMessage = $" lost all your health!";

            }
            else
            {
                Health -= damageTaken;
                healthMessage = $" are now at {Health} health.";

            }
            return $"You took {damageTaken} damage. You{healthMessage}";
        }

        /// <summary>
        /// Displays the player's stats at the top of the screen.
        /// </summary>
        /// <returns></returns>
        public string DisplayStats()
        {
            return $"\nHealth - {Health}";
        }

        /// <summary>
        /// Moves the player through the maze in a certain direction when this method is called.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="player"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        /// <param name="direction"></param>
        public void MovePlayer(string[,] grid, Player player, int playerX, int playerY, string direction)
        {

            int xDirection = 0;
            int yDirection = 0;

            if (direction == "right")
            {
                xDirection = 1;
                if (playerX < grid.GetLength(0) - 1 && grid[playerX + xDirection, playerY + yDirection] != "#")
                {
                    MoveToNewTile();
                }
            }
            else if (direction == "left")
            {
                xDirection = -1;
                if (playerX > 0 && grid[playerX + xDirection, playerY + yDirection] != "#")
                {
                    MoveToNewTile();
                }
            }
            else if (direction == "down")
            {
                yDirection = 1;
                if (playerY < grid.GetLength(1) - 1 && grid[playerX + xDirection, playerY + yDirection] != "#")
                {
                    MoveToNewTile();
                }
            }
            else
            {
                yDirection = -1;
                if (playerY > 0 && grid[playerX + xDirection, playerY + yDirection] != "#")
                {
                    MoveToNewTile();
                }
            }

            void MoveToNewTile()
            {
                // If you hit M tile, do a fight.
                if (grid[playerX + xDirection, playerY + yDirection] == "M")
                {
                    Console.Clear();
                    FightMonster(player);
                    Console.WriteLine("\n[Press any key to continue]");
                    Console.ReadKey();
                }
                // If you hit P tile, drink a health potion.
                else if (grid[playerX + xDirection, playerY + yDirection] == "P")
                {
                    Console.Clear();
                    DrinkPotion(player, "health");
                    Console.WriteLine("\n[Press any key to continue]");
                    Console.ReadKey();
                }
                // Be careful of lowercase p's.
                else if (grid[playerX + xDirection, playerY + yDirection] == "₱")
                {
                    Console.Clear();
                    DrinkPotion(player, "damage");
                    Console.WriteLine("\n[Press any key to continue]");
                    Console.ReadKey();
                }
                grid[playerX, playerY] = ".";
                grid[playerX + xDirection, playerY + yDirection] = "*";

            }

        }

        /// <summary>
        /// Player drinks a potion when this method is called.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="potType"></param>
        public void DrinkPotion(Player player, string potType)
        {
            Random randPotion = new Random();

            if (potType == "health")
            {
                HealthPotion healPotion = new HealthPotion();
                healPotion.PotionIntensity = randPotion.Next(15, 30);
                Console.WriteLine(healPotion.AffectPlayer(player));
            }
            else
            {
                DamagePotion damagePotion = new DamagePotion();
                damagePotion.PotionIntensity = randPotion.Next(10, 20);
                Console.WriteLine(damagePotion.AffectPlayer(player));
            }
        }

        /// <summary>
        /// Player fights a monster when this method is called.
        /// </summary>
        /// <param name="player"></param>
        public static void FightMonster(Player player)
        {
            // Make a new monster
            Monster enemy = new Monster();
            enemy.Health = 100;
            //  Print a message.
            Console.WriteLine("You come across a monster and it wants to battle you.");
            // While both the player and monster are alive, to this
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine($"You attack the monster. {enemy.TakeDamage(15)}");
                Thread.Sleep(300);
                if (enemy.Health > 0)
                {
                    Console.WriteLine($"The monster takes its turn. {player.TakeDamage(15)}");
                    Thread.Sleep(300);
                }
            }
            // Whoever's health is 0 loses.
            if (player.Health < 1)
            {
                Console.WriteLine("The monster got you!");
            }
            else
            {
                Console.WriteLine("You beat the monster!");
            }
        }

    }
}
