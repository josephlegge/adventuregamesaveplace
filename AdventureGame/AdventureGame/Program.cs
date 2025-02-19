

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
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

            List<string> crowns = new List<string>(); // too tired to make crowns an actual object and i dont really think i need to 
            bool quit = false;
            int totalPoints = 0;

            // Instructions

            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Tr∊asure Maz∊");
            Console.WriteLine("\nↆ is a sword. Some do more damage than others.\n⨸ is a shield. You have a 1/3 chance to block an attack 3 times.");
            Console.WriteLine("\nP is a health potion. Keep an eye out for fakes.\nM is a monster. Watch out for them when you have low health.");
            Console.WriteLine("\n* is your player. Use wasd/↑←↓→ to move.\n[] is the escape tile. Touch it to win.");
            Console.WriteLine("\n. is gold. collect it all to get a crown in each maze. Gold resets when you lose, but crowns don't.");

            // Wait for input and then start the game.
            Console.ReadKey();

            // Game Loop
            while (quit == false)
            {
                Console.Clear();

                // Create/Reset player
                Player player = new Player(health: 100);

                // Variables for first menu.
                int selection = 0;
                bool entered = false;

                // Set them to these before you have any crowns.
                string easy = "\nSmall ";
                string normal = "\nMedium ";
                string hard = "\nLarge ";
                string leave = "\nLeave Game";

                // These methods change the color and text of an option depending on what crowns you have.
                void ChangeEasy(string marker)
                {
                    if (crowns.Contains("easy") == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nSmall {marker} 👑");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"\nSmall {marker}");
                    }
                }
                void ChangeNormal(string marker)
                {
                    if (crowns.Contains("normal") == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Medium {marker} 👑");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"Medium {marker}");
                    }
                }
                void ChangeHard(string marker)
                {
                    if (crowns.Contains("hard") == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Large {marker} 👑");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"Large {marker}");
                    }
                }
                void ChangeLeave(string marker)
                {
                    if (crowns.Count() == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nWin {marker}👑");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"\nLeave {marker}");
                    }
                }

                // Grid size
                int X = 2 * rand.Next(5, 7);
                int Y = 2 * rand.Next(5, 7);

                // Smaller Game Loop
                while (entered == false)
                {

                    // Reset board and then display options and what the user is selecting.
                    Console.ResetColor();
                    Console.WriteLine($"Gold: {totalPoints}");

                    if (selection == 0)
                    {
                        ChangeEasy("*");
                        ChangeNormal("");
                        ChangeHard("");
                        ChangeLeave("");

                        X = 2 * rand.Next(5, 7);
                        Y = 2 * rand.Next(5, 7);
                        quit = false;
                    }
                    else if (selection == 1)
                    {
                        ChangeEasy("");
                        ChangeNormal("*");
                        ChangeHard("");
                        ChangeLeave("");

                        X = 2 * rand.Next(7, 13);
                        Y = 2 * rand.Next(7, 13);
                        quit = false;
                    }
                    else if (selection == 2)
                    {
                        ChangeEasy("");
                        ChangeNormal("");
                        ChangeHard("* (fullscreen recommended)");
                        ChangeLeave("");

                        X = 2 * rand.Next(15, 21);
                        Y = 2 * rand.Next(15, 21);
                        quit = false;
                    }
                    else
                    {
                        ChangeEasy("");
                        ChangeNormal("");
                        ChangeHard("");
                        ChangeLeave("* ");
                        quit = true;
                    }
                  
                    // Let the player move the marker up or down, or let the player select leave the game
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    if (selection > 0 && keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W)
                    {
                        selection = selection - 1;

                    }
                    else if (selection < 3 && keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S)
                    {
                        selection = selection + 1;

                    }
                    
                    // This will break the loop. Whatever the selected and quit variable is determines what happens next
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        entered = true;
                    }
                    Console.Clear();
                }

                // If quit isn't false, then build the maze 
                if (quit == false)
                {
                    // Create maze using random X and Y values for the width and height
                    Maze maze = new Maze(X, Y);
                    string[,] grid = maze.Grid;

                    maze.GenerateMaze();
                    maze.CountPoints();

                    // Game loop
                    while (player.Health > 0 && player.Escaped == false)
                    {

                        // Update player's position. i can't decide if this goes in maze or player class for now it stays here
                        int playerX = 0;
                        int playerY = 0;
                        // Find the player's position every time.
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

                        ConsoleKeyInfo keyInfo;
                        if (player.Busy == false)
                        {
                            // Clear graphics
                            Console.Clear();
                            Console.ResetColor();

                            // Reset graphics
                            Console.WriteLine(player.DisplayStats(maze.TotalPoints));
                            if (player.Points != maze.TotalPoints)
                            {
                                Console.WriteLine($"Gold: {player.Points}/{maze.TotalPoints}");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Gold: {player.Points}/{maze.TotalPoints} 👑");
                                Console.ResetColor();
                            }

                            Console.WriteLine(maze.RefreshMaze());
                            Console.WriteLine(player.PlayerNotification);

                            // Move the player.
                            keyInfo = Console.ReadKey(intercept: true);
                            player.MovePlayer(grid, player, playerX, playerY, keyInfo);

                        }
                        else
                        {

                            // Make a new monster
                            Monster enemy = new Monster();
                            enemy.Health = rand.Next(8, 10) * 10;
                            //  Print a message.
                            Console.WriteLine("You run into a monster and it attacks you.");
                            // While both the player and monster are alive, to this
                            while (player.Health > 0 && enemy.Health > 0)
                            {

                                Console.WriteLine($"You attack the monster. {enemy.TakeDamage(8 + player.PlayerWeapon.Damage, "enemy")}");
                                Thread.Sleep(300);
                                if (enemy.Health > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"The monster takes its turn. {player.TakeDamage(10, "enemy")}");
                                    Console.ResetColor();

                                    Thread.Sleep(300);
                                }
                            }
                            // Whoever's health is 0 loses.
                            if (player.Health < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The monster won the fight.");
                            }
                            // If player wins, give them 20 points 
                            else
                            {
                                player.Points += 20;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("You beat the monster!");
                                player.PlayerNotification = "You beat the monster!";
                                Console.ResetColor();
                                Console.WriteLine("[Press any key to continue.]");
                            }
                            // Flush out the input thing and wait for input
                            while (Console.KeyAvailable)
                            {
                                Console.ReadKey();
                            }
                            Console.ReadKey();
                            // Make player not busy anymore
                            player.Busy = false;


                        }
                    }
                    // If loop ended because player lost.
                    if (player.Health == 0)
                    {
                        totalPoints = 0;
                        Console.WriteLine("You lose. (Press enter to try again)");
                        Console.ReadLine();
                    }
                    // If loop ended because player escaped.
                    else
                    {

                        // Clear the screen. 
                        Console.Clear();
                        Console.WriteLine($"You escaped the maze!");

                        // Add player's points to the total points saved.
                        totalPoints = totalPoints + player.Points;

                        // Variable to check if points were left
                        Console.WriteLine($"\nScore: {player.Points}");

                        // Check if any points ". " or from monsters are left in the maze.

                        // Print a message depending on if points are left or not.
                        if (maze.CountPoints() == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("You found all the gold! 👑 (Press enter to continue)");

                            // If player won an easy, medium or hard maze, add the crown for it.
                            if (selection == 0 && crowns.Contains("easy") == false)
                            {
                                crowns.Add("easy");
                            }
                            else if (selection == 1 && crowns.Contains("normal") == false)
                            {
                                crowns.Add("normal");
                            }
                            else if (selection == 2 && crowns.Contains("hard") == false)
                            {
                                crowns.Add("hard");
                            }
                        }
                        // Else tell them that they missed points.
                        else
                        {
                            Console.WriteLine($"You missed {maze.TotalPoints} gold. (Press enter to continue)");
                        }
                        // Give the player a chance to read something without accidentally skipping it.
                        Console.ReadLine();
                    }
                }
            }

            // If loop is broken because the player won all crowns, display a message.
            if (crowns.Count() == 3)
            {
                Console.ResetColor();
                Console.WriteLine($"You mastered all the mazes and left with {totalPoints} gold, so you beat the game!");
            }
            // Displayer a different message if they just leave.
            else
            {
                Console.WriteLine($"You escaped the maze with {crowns.Count()} crowns and {totalPoints} points.");
            }
        }
    }
}

