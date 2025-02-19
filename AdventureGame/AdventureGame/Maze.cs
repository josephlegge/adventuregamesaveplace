using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AdventureGame;

using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AdventureGame
{
    /// <summary>
    /// Class for the mazes.
    /// </summary>
    public class Maze
    {

        /// <summary>
        /// Random object.
        /// </summary>
        Random rand = new Random();

        /// <summary>
        /// Maze X size
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Maze Y size
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Maze Grid
        /// </summary>
        public string[,] Grid { get; set; }
        /// <summary>
        /// Total Points
        /// </summary>
        public int TotalPoints { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public Maze(int x, int y)
        {
            // Grid size
            X = x;
            Y = y;

            // Grid
            Grid = new string[X, Y];
        }
        /// <summary>
        /// Method that generates the "maze" (it's not a maze yet)
        /// </summary>
        /// <returns></returns>
        public string GenerateMaze()
        {
            // Recursive Backtracking Start.
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    Grid[x, y] = "◙ ";
                }
            }
            List<(int, int)> GetNeighbors(int x, int y)
            {
                List<(int, int)> neighbors = new List<(int, int)>();
                if (x - 2 > 4)
                {
                    if (Grid[x - 2, y] == "◙ ")
                    {
                        var left = (x - 2, y);
                        neighbors.Add(left);
                    }
                }
                if (x + 2 < Grid.GetLength(0) - 2)
                {
                    if (Grid[x + 2, y] == "◙ ")
                    {
                        var right = (x + 2, y);
                        neighbors.Add(right);
                    }
                }
                if (y - 2 > 0)
                {
                    if (Grid[x, y - 2] == "◙ ")
                    {
                        var up = (x, y - 2);
                        neighbors.Add(up);
                    }
                }
                if (y + 2 < Grid.GetLength(1) - 2)
                {
                    if (Grid[x, y + 2] == "◙ ")
                    {
                        var down = (x, y + 2);
                        neighbors.Add(down);
                    }

                }

                return neighbors;
            }

            // Start from Grid[1, 1]
            int startX = 1;
            int startY = 1;
            Grid[startX, startY] = ". ";


            List<(int, int)> neighbors = GetNeighbors(startX, startY);
            Stack<(int, int)> path = new Stack<(int, int)>();
            path.Push((startX, startY));
            RecursiveSection(startX, startY);
            void RecursiveSection(int x, int y)
            {
                List<(int, int)> neighbors = GetNeighbors(x, y);
                while (path.Count > 0)
                {
                    if (neighbors.Count > 0)
                    {
                        path.Push((x, y));
                        var randomNeighbor = neighbors[rand.Next(0, neighbors.Count())];
                        int nx = randomNeighbor.Item1;
                        int ny = randomNeighbor.Item2;

                        Grid[(x + nx) / 2, (y + ny) / 2] = ". ";
                        Grid[nx, ny] = ". ";
                        RecursiveSection(nx, ny);
                    }
                    else
                    {


                        path.Pop();
                        if (path.Count > 0)
                        {
                            RecursiveSection(path.Peek().Item1, path.Peek().Item2);
                        }
                    }
                }
            }
            // End


            // This is the player
            Grid[1, 1] = "* ";
            // This is the exit
            Grid[Grid.GetLength(0) - 3, Grid.GetLength(1) - 3] = "[]";
            // This lets the player continue exploring the maze if the exit is in the path
            Grid[Grid.GetLength(0) - 4, Grid.GetLength(1) - 4] = ". ";
            // Loop through to get x and y
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {

                    if (Grid[x, y] != "* " && Grid[x, y] != "[]" && Grid[x, y] != "◙ ")
                    {
                        {
                            int chance = rand.Next(0, 300);

                            // Chances all items have of spawning on every tile
                            if (chance < 2)
                            {
                                Grid[x, y] = "⨸ ";
                            }
                            else if (chance < 5)
                            {
                                Grid[x, y] = "₱ ";
                            }

                            else if (chance < 10)
                            {
                                Grid[x, y] = "M ";
                            }

                            else if (chance < 20)
                            {
                                Grid[x, y] = "P ";
                            }
                            else if (chance < 25)
                            {
                                Grid[x, y] = "ↆ ";
                            }
                            else if (chance < 300)
                            {
                                Grid[x, y] = ". ";
                            }
                        }
                        // Make sure to add walls on all sides


                    }
                    if (x == Grid.GetLength(0) - 1 || y == Grid.GetLength(1) - 1)
                    {
                        Grid[x, y] = string.Empty;
                    }

                }

            }
            return "Maze Generated";
        }
        /// <summary>
        /// Refresh the maze
        /// </summary>
        /// <returns></returns>
        public string RefreshMaze()
        {

            string gridMessage = "";
            List<string> excludeChars = new List<string>();
            List<string> badChars = new List<string>();

            excludeChars = ["* ", "P ", "ↆ ", "⨸ ", "₱ ", "M ", "[]", "◙ ", ". "];
            badChars = ["M ", "₱ "];
            int traps = 0;

            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                gridMessage += "\n";
                for (int x = 0; x < Grid.GetLength(0); x++)
                {

                    if (badChars.Contains(Grid[x, y]))
                    {
                        traps += 1;
                    }

                    gridMessage += Grid[x, y];
                }
            }

            if (rand.Next(0, 1000) < 10 && traps > 1)
            {
                RandomLoot(excludeChars);
            }

            return gridMessage;
        }
        /// <summary>
        ///  Cound the points
        /// </summary>
        /// <returns></returns>
        public int CountPoints()
        {
            TotalPoints = 0;
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    if (Grid[x, y] == ". ")
                    {
                        TotalPoints = TotalPoints + 1;
                    }
                    else if (Grid[x, y] == "M ")
                    {
                        TotalPoints = TotalPoints + 20;
                    }
                }
            }
            return TotalPoints;
        }

        /// <summary>
        /// Low chance to spawn a healing potion if there are still monsters
        /// </summary>
        /// <param name="excludeChars"></param>
        public void RandomLoot(List<string> excludeChars)
        {
            bool done = false;
            bool blankSpace = false;

            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {

                    if (Grid[x, y] == "  ")
                    {
                        blankSpace = true;
                    }
                }
            }
            while (done == false && blankSpace == true)
            {
                int randX = rand.Next(0, Grid.GetLength(0));
                int randY = rand.Next(0, Grid.GetLength(1));
                if (Grid[randX, randY] == "  ")
                {
                    Grid[randX, randY] = "P ";
                    done = true;
                }
            }
        }
    }
}