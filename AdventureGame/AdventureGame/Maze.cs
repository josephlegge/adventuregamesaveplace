using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AdventureGame;

using static System.Net.Mime.MediaTypeNames;

public class Maze
{

    /// <summary>
    /// Random object.
    /// </summary>
    Random rand = new Random();

    /// <summary>
    /// Properties
    /// </summary>
    public int X { get; set; }
    public int Y { get; set; }
    public string[,] Grid { get; set; }
    public bool Escape { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public Maze(int X, int Y)
    {
        X = rand.Next(40, 50);
        Y = rand.Next(20, 25);
        Grid = new string[X, Y];
        Escape = false;
    }

    [Flags]
    public enum Direction
    {
        None = 1,
        N = 2,
        S = 4,
        E = 8,
        W = 16
    }

    /// <summary>
    /// Method that generates the "maze" (it's not a maze yet).
    /// </summary>
    /// <returns></returns>
    public string GenerateMaze()
    {

        Grid[1, 1] = "* ";
        Grid[Grid.GetLength(0) - 2, Grid.GetLength(1) - 2] = "[]";
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                // Recursive Start. Delete if tutorial doesn't help
                if (Grid[x, y] != "* " && Grid[x, y] != "[]")
                {
                    {
                        int chance = rand.Next(0, 300);
                        if (chance < 1)
                        {
                            Grid[x, y] = "⨸ ";
                        }
                        else if (chance < 3)
                        {
                            Grid[x, y] = "₱ ";
                        }
                        else if (chance < 5)
                        {
                            Grid[x, y] = "M ";
                        }
                        else if (chance < 7)
                        {
                            Grid[x, y] = "P ";
                        }
                        else if (chance < 9)
                        {
                            Grid[x, y] = "ↆ ";
                        }
                        
                        else if (chance < 300)
                        {
                            Grid[x, y] = ". ";
                        }
                    }
                    if (x == 0 || x == Grid.GetLength(0) - 1 || y == 0 || y == Grid.GetLength(1)-1)
                    {
                        Grid[x, y] = "⊡ ";
                    }
                }

            }

        }








        return "Maze Generated";
    }

    public string PrintMaze(string[,] grid)
    {
        string gridMessage = "";

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            gridMessage += "\n";
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                gridMessage += grid[x, y];

            }
        }
        return gridMessage;
    }

}
