using System;
using System.Collections.Generic;
using AdventureGame;
using static System.Net.Mime.MediaTypeNames;

public class Maze
{

    Random rand = new Random();

    public int X { get; set; }
    public int Y { get; set; }
    public string[,] Grid { get; set; }

    public Maze(int X, int Y)
    {
        
        X = rand.Next(25,30);
        Y = rand.Next(20, 25);

        Grid = new string[X,Y];
    }

    public string GenerateMaze()
    {

        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                if (x == 0 || y == 0 || x == Grid.GetLength(0) - 1 || y == Grid.GetLength(1) - 1)
                {
                    Grid[x, y] = "#";
                }
                else
                {
                    int chance = rand.Next(0, 215);
                    if (chance < 10)
                    {
                        Grid[x, y] = "M";
                    }
                    else if (chance < 20)
                    {
                        Grid[x, y] = "P";
                    }
                    else if (chance < 24)
                    {
                        Grid[x, y] = "₱";
                    }
                    else if (chance < 215)
                    {
                        Grid[x, y] = ".";
                    }
                }
            }
            // * kinda looks like a person if you look closely 
            // "Entrance tile"?
            Grid[1, 1] = "*";
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
