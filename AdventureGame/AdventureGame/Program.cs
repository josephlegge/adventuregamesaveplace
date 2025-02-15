

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
            while (player.Health > 0 && player.Escaped == false)
            {

                // Update player's position. i can't decide if this goes in maze or player class for now it stays here
                int playerX = 0;
                int playerY = 0;
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (grid[x, y] == "* ")
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
                if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "right");
                }
                else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "left");
                }
                else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "down");
                }
                else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
                {
                    player.MovePlayer(grid, player, playerX, playerY, "up");
                }
                // Flush out the readkey thingy
                while (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(intercept: true);
                }
            }
            // If loop ended because player lost.
            if (player.Health == 0)
            {
                Console.WriteLine("You lose.");
            }
            // If loop ended because player escaped.
            else
            {
                Console.Clear();
                Console.WriteLine("You escaped the maze and win!");
            }
        }

        /// <summary>
        /// Write a message from another class or method easier.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cont"></param>
        /// <param name="health"></param>
        public static void WriteMessage(string message, bool cont, int health)
        {
            Console.WriteLine(message);
            if (cont == true && health > 0)
            {
                Console.WriteLine("[Press any key to continue.]");
                Console.ReadKey();
            }
        }
    }
}

