using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventureGame
{
    /// <summary>
    /// Class for the player.
    /// </summary>
    internal class Player : ICharacter
    {
        Random rand = new Random();
        

        /// <summary>
        /// Player's best weapon
        /// </summary>
        public Weapon PlayerWeapon { get; set; }
        /// <summary>
        /// Player's shield
        /// </summary>
        public Shield PlayerShield { get; set; }

        /// <summary>
        /// Player's points property
        /// </summary>   
        public int Points { get; set; }
        /// <summary>
        /// Player's health property
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Bool that just checks if player escaped the maze
        /// </summary>
        public bool Escaped { get; set; }
        /// <summary>
        /// Check if the player is busy in a fight
        /// </summary>
        public bool Busy { get; set; }
        /// <summary>
        /// The notification that will pop up at the bottom of the screen whenever the player does something. (It's printed in Main() in the program still)
        /// </summary>
        public string PlayerNotification { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="health"></param>
        public Player(int health)
        {
            PlayerWeapon = new Knife();
            Health = health;
            Escaped = false;
            Busy = false;
        }

        /// <summary>
        /// Heals the player by whatever number healthRecovered is when this method is called
        /// </summary>
        /// <param name="healthRecovered"></param>
        /// <returns></returns>
        public string Heal(int healthRecovered)
        {
            string healthMessage = string.Empty;

            // Do some math stuff to make sure player's health doesn't go over 100.
            if (healthRecovered + Health >= 100)
            {
                healthRecovered = 100 - Health;

                Health = 100;
                healthMessage = $"Your health is capped at 100.";
            }
            else
            {
                Health += healthRecovered;
                healthMessage = $"Your health is now {Health}.";

            }
            return $" Recovered {healthRecovered} health. {healthMessage}";
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
                if (rand.Next(0, 2) == 0 && PlayerShield.Uses > 0)
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
                // Do some math stuff to make sure player's health doesn't go below 0.
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
        public string DisplayStats(int points)
        {
            // statsMessage starts as an empty message that will be added onto depending on what the player has and then returned.
            string statsMessage = $"\nHealth: {Health}";

            if (PlayerWeapon != null)
            {
                statsMessage += $" ::: {PlayerWeapon.ItemName}: 8+({PlayerWeapon.Damage}) = {8 + PlayerWeapon.Damage} ";
            }
            if (PlayerShield != null)
            {
                statsMessage += $" ::: Shield Durability: {PlayerShield.Uses}";
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
        public void MovePlayer(string[,] grid, Player player, int playerX, int playerY, ConsoleKeyInfo keyInfo)
        {
            // Move the player in a direction as long as there are no walls. Then check what the player runs into and trigger it.
            int xDirection = 0;
            int yDirection = 0;
            // Player moves right
            if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                xDirection = 1;
                if (playerX < grid.GetLength(0) - 1 && grid[playerX + xDirection, playerY + yDirection] != "◙ ")
                {
                    MoveToNewTile();
                }
            }
            // Player moves left
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)

            {
                xDirection = -1;
                if (playerX > 0 && grid[playerX + xDirection, playerY + yDirection] != "◙ ")
                {
                    MoveToNewTile();
                }
            }
            // Player moves down
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)

            {
                yDirection = 1;
                if (playerY < grid.GetLength(1) - 1 && grid[playerX + xDirection, playerY + yDirection] != "◙ ")
                {
                    MoveToNewTile();
                }
            }
            // Player moves up
            else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                yDirection = -1;
                if (playerY > 0 && grid[playerX + xDirection, playerY + yDirection] != "◙ ")
                {
                    MoveToNewTile();
                }
            }
            // This will check what the new tile is, use/change it, and then move the player there.
            void MoveToNewTile()
            {
                if (grid[playerX + xDirection, playerY + yDirection] == ". " || grid[playerX + xDirection, playerY + yDirection] == "  ")
                {
                    Console.Clear();
                    PlayerNotification = "";
                    if (grid[playerX + xDirection, playerY + yDirection] == ". ")
                    {
                        Points += 1;
                    }
                }
                // If you hit M tile, start a fight.
                else if (grid[playerX + xDirection, playerY + yDirection] == "M ")
                {
                    Console.Clear();
                    Busy = true;
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
                    int damage = rand.Next(4, 9);

                    Sword collectedWeapon = new Sword(damage);

                    if (collectedWeapon.Damage > PlayerWeapon.Damage)
                    {
                        PlayerWeapon = collectedWeapon;
                        Console.Clear();
                        PlayerNotification = $"Picked up a sword that deals {damage} damage.";

                    }
                    else
                    {
                        Console.Clear();
                        PlayerNotification = "Your own weapon is better than this one.";

                    }

                }
                // Get Shield
                else if (grid[playerX + xDirection, playerY + yDirection] == "⨸ ")
                {
                    Shield collectedShield = new Shield(3);
                    PlayerShield = collectedShield;
                    Console.Clear();
                    PlayerNotification = "You found a shield. It gives a chance to dodge attacks.";
                }
                // Escape!
                else if (grid[playerX + xDirection, playerY + yDirection] == "[]")
                {
                    player.Escaped = true;
                }
                grid[playerX, playerY] = "  ";
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
            // This picks the potion's strength
            Random randPotion = new Random();

            if (potType == "health")
            {
                HealthPotion healPotion = new HealthPotion();
                healPotion.Strength = randPotion.Next(15, 30);
                PlayerNotification = healPotion.AffectPlayer(player);
            }
            else
            {
                DamagePotion damagePotion = new DamagePotion();
                damagePotion.Strength = randPotion.Next(3, 9);
                PlayerNotification = damagePotion.AffectPlayer(player);

            }
        }
    }
}
