using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    internal class Player : ICharacter
    {

        Random rand = new Random();
        /// <summary>
        /// Player's health stat.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Player's best weapon.
        /// </summary>
        public Weapon PlayerWeapon { get; set; }
        public Shield PlayerShield { get; set; }
        public bool Escaped { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="health"></param>
        public Player(int health)
        {
            PlayerWeapon = new Knife(2);
            Health = health;
            Escaped = false;
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
        public string TakeDamage(int damageTaken, string damageType)
        {
            // The message that will be returned when you take damage.
            string healthMessage = string.Empty;

            // If an enemy attacks you, you have a change to block with a shield if you have one.
            if (damageType == "enemy" && PlayerShield != null)
            {

                // Depending on a random change and if the player's shield still has uses, block all damage.
                if (rand.Next(0, 3) == 0 && PlayerShield.Uses > 0)
                {
                    damageTaken = 0;
                    healthMessage = $"You blocked all the damage with your shield {PlayerShield.Damage()}";
                    if (PlayerShield.Uses == 0)
                    {
                        PlayerShield = null;
                    }
                }
            }

            // If the attack wasn't blocked then still take damage.
            if (damageTaken > 0)
            {
                if (Health < damageTaken)
                {
                    Health = 0;
                    healthMessage = $"You took {damageTaken} and lost all your health!";

                }
                else
                {
                    Health -= damageTaken;
                    healthMessage = $"You took {damageTaken} and are now at {Health} health.";
                }
            }
  

            return healthMessage;


        }

        /// <summary>
        /// Displays the player's stats at the top of the screen.
        /// </summary>
        /// <returns></returns>
        public string DisplayStats()
        {
            string statsMessage = $"\nHealth: {Health}";

            if (PlayerWeapon != null)
            {
                statsMessage += $"\n{PlayerWeapon.ItemName}: 8+({PlayerWeapon.Damage}) = {8 + PlayerWeapon.Damage} ";
            }
            if (PlayerShield != null)
            {
                statsMessage += $"\nShield Durability: {PlayerShield.Uses}";
            }
            return statsMessage;
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
                if (playerX < grid.GetLength(0) - 1 && grid[playerX + xDirection, playerY + yDirection] != "⊡ ")
                {
                    MoveToNewTile();
                }
            }
            else if (direction == "left")
            {
                xDirection = -1;
                if (playerX > 0 && grid[playerX + xDirection, playerY + yDirection] != "⊡ ")
                {
                    MoveToNewTile();
                }
            }
            else if (direction == "down")
            {
                yDirection = 1;
                if (playerY < grid.GetLength(1) - 1 && grid[playerX + xDirection, playerY + yDirection] != "⊡ ")
                {
                    MoveToNewTile();
                }
            }
            else
            {
                yDirection = -1;
                if (playerY > 0 && grid[playerX + xDirection, playerY + yDirection] != "⊡ ")
                {
                    MoveToNewTile();
                }
            }

            void MoveToNewTile()
            {
                // If you hit M tile, do a fight.
                if (grid[playerX + xDirection, playerY + yDirection] == "M ")
                {
                    Console.Clear();
                    FightMonster(player);
                }
                // If you hit P tile, drink a health potion.
                else if (grid[playerX + xDirection, playerY + yDirection] == "P ")
                {
                    Console.Clear();
                    DrinkPotion(player, "health");
                }
                // Be careful of lowercase p's.
                else if (grid[playerX + xDirection, playerY + yDirection] == "₱ ")
                {
                    Console.Clear();
                    DrinkPotion(player, "damage");
                }
                // Get Sword
                else if (grid[playerX + xDirection, playerY + yDirection] == "ↆ ")
                {
                    int damage = rand.Next(5, 8);

                    Sword collectedWeapon = new Sword(damage);

                    if (collectedWeapon.Damage > PlayerWeapon.Damage)
                    {
                        PlayerWeapon = collectedWeapon;


                        Console.Clear();
                        Program.WriteMessage($"Picked up a sword that deals {damage} damage.", true, Health);
                    }
                    else
                    {
                        Console.Clear();
                        Program.WriteMessage($"Your own weapon is better than this one.", true, Health);
                    }
                    
                }
                // Get Sword
                else if (grid[playerX + xDirection, playerY + yDirection] == "⨸ ")
                {
                    Shield collectedShield = new Shield(3);
                    PlayerShield = collectedShield;
                    Console.Clear();
                    Program.WriteMessage("Picked up a shield. It Gives a chance to block attacks", true, Health);
                }
                // Escape!
                else if (grid[playerX + xDirection, playerY + yDirection] == "[]")
                {
                    player.Escaped = true;
                }
                grid[playerX, playerY] = ". ";
                grid[playerX + xDirection, playerY + yDirection] = "* ";

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
                healPotion.Strength = randPotion.Next(15, 30);
                Program.WriteMessage(healPotion.AffectPlayer(player), true, Health);
            }
            else
            {
                DamagePotion damagePotion = new DamagePotion();
                damagePotion.Strength = randPotion.Next(10, 20);
                Program.WriteMessage(damagePotion.AffectPlayer(player), true, Health);
            }
        }

        /// <summary>
        /// Player fights a monster when this method is called.
        /// </summary>
        /// <param name="player"></param>
        public void FightMonster(Player player)
        {
            // Make a new monster
            Monster enemy = new Monster();
            enemy.Health = 100;
            //  Print a message.
            Program.WriteMessage("You run into a monster and it attacks you.", false, player.Health);
            // While both the player and monster are alive, to this
            while (player.Health > 0 && enemy.Health > 0)
            {
                Program.WriteMessage($"You attack the monster. {enemy.TakeDamage(8+PlayerWeapon.Damage, "enemy")}", false, player.Health);
                Thread.Sleep(300);
                if (enemy.Health > 0)
                {
                    Program.WriteMessage($"The monster takes its turn. {player.TakeDamage(10, "enemy")}", false, player.Health);
                    Thread.Sleep(300);
                }
            }
            // Whoever's health is 0 loses.
            if (player.Health < 1)
            {
                Program.WriteMessage("The monster beat you!", false, player.Health);
            }
            else
            {
                Program.WriteMessage("You beat the monster!", true, player.Health);
            }
        }

    }
}
