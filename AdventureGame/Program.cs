

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventureGame
{
    class Program
    {
        public static void Main()
        {

            // Looked up how to allow symbols.
            Console.OutputEncoding = Encoding.UTF8;

            // New random object
            Random rand = new Random();

            // Create player
            Player player = new Player(health: 100);

            // Create maze
            Maze maze = new Maze(5, 5);
            string[,] grid = maze.Grid;
            maze.GenerateMaze();

            // Game loop
            while (player.Health > 0)
            {
                
                // Update player's position. i can't decide if this goes in maze or player class for now it stays here
                int playerX = 0;
                int playerY = 0;
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (grid[x, y] == "*")
                        {
                            playerX = x;
                            playerY = y;
                        }
                    }
                }

                // Reload graphics.
                Console.Clear();
                Console.WriteLine(player.DisplayStats());
                Console.WriteLine(maze.PrintMaze(grid));

                // Moves the player.
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "right");
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "left");
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "down");
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "up");
                }
                // Flush out the readkey thingy
                while (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(intercept: true);
                }
            }
            Console.WriteLine("You lose.");
        }
        // Move Player
        /*
        public static void MovePlayer(string[,] grid, Player player, int playerX, int playerY, string direction)
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

        */

        
        // Take Potion
        /*
        public static void DrinkPotion(Player player, string potType)
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

        */

       
        // Fight Monster
        /*
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
        public static void LoadImage(string[,] grid, Player player)
        {
            string gridMessage = "";

            Console.WriteLine(player.DisplayStats());

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                gridMessage += "\n";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    gridMessage += grid[x, y];

                }
            }
            Console.WriteLine(gridMessage);
        }
        */
    }
}

