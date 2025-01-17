﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Collections;


public static class Extensions
{

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng)
    {
        var e = source.ToArray();
        for (var i = e.Length - 1; i >= 0; i--)
        {
            var swapIndex = rng.Next(i + 1);
            yield return e[swapIndex];
            e[swapIndex] = e[i];
        }
    }

    public static CellState OppositeWall(this CellState orig)
    {
        return (CellState)(((int)orig >> 2) | ((int)orig << 2)) & CellState.Initial;
    }

    public static bool HasFlag(this CellState cs, CellState flag)
    {
        return ((int)cs & (int)flag) != 0;
    }
}

[System.Flags]
public enum CellState
{
    Top = 1,
    Right = 2,
    Bottom = 4,
    Left = 8,
    Visited = 128,
    Initial = Top | Right | Bottom | Left,
}

public struct RemoveWallAction
{
    public Vector2 Neighbour;
    public CellState Wall;
}




public class Maze
{
    private readonly CellState[,] _cells;
    private readonly int _width;
    private readonly int _height;
    private readonly System.Random _rng;

    public Maze(int width, int height)
    {
        _width = width;
        _height = height;
        _cells = new CellState[width, height];
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                _cells[x, y] = CellState.Initial;
        _rng = new System.Random();
        VisitCell(_rng.Next(width), _rng.Next(height));
    }

    public CellState this[int x, int y]
    {
        get { return _cells[x, y]; }
        set { _cells[x, y] = value; }
    }

    public IEnumerable<RemoveWallAction> GetNeighbours(Vector2 p)
    {
        if (p.x > 0) yield return new RemoveWallAction { Neighbour = new Vector2(p.x - 1, p.y), Wall = CellState.Left };
        if (p.y > 0) yield return new RemoveWallAction { Neighbour = new Vector2(p.x, p.y - 1), Wall = CellState.Top };
        if (p.x < _width - 1) yield return new RemoveWallAction { Neighbour = new Vector2(p.x + 1, p.y), Wall = CellState.Right };
        if (p.y < _height - 1) yield return new RemoveWallAction { Neighbour = new Vector2(p.x, p.y + 1), Wall = CellState.Bottom };
    }

    public void VisitCell(int x, int y)
    {
        this[x, y] |= CellState.Visited;
        foreach (var p in GetNeighbours(new Vector2(x, y)).Shuffle(_rng).Where(z => !(this[(int)z.Neighbour.x, (int)z.Neighbour.y].HasFlag(CellState.Visited))))
        {
            this[x, y] -= p.Wall;
            this[(int)p.Neighbour.x, (int)p.Neighbour.y] -= p.Wall.OppositeWall();
            VisitCell((int)p.Neighbour.x, (int)p.Neighbour.y);
        }
    }

    

    public ArrayList Display()
    {
        ArrayList mazeRaw = new ArrayList();
        
        var firstLine = string.Empty;
        for (var y = 0; y < _height; y++)
        {
            var sbTop = new StringBuilder();
            var sbMid = new StringBuilder();
            for (var x = 0; x < _width; x++)
            {
                sbTop.Append(this[x, y].HasFlag(CellState.Top) ? "##" : "# ");
                sbMid.Append(this[x, y].HasFlag(CellState.Left) ? "# " : "  ");
                //sbTop.Append(this[x, y].HasFlag(CellState.Top) ? "#" : " ");
                //sbMid.Append(this[x, y].HasFlag(CellState.Left) ? "#" : " ");
            }
            if (firstLine == string.Empty) firstLine = sbTop.ToString();
            mazeRaw.Add(sbTop + "#");
            mazeRaw.Add(sbMid + "#");
        }
        mazeRaw.Add(firstLine + "#");
        return mazeRaw;
        
    }



}


public class MazeGen : MonoBehaviour
{
    public static ArrayList mazeRaw = new ArrayList();
    public ArrayList create(int size) { 
        size += 1;
        size /= 2;
        var maze = new Maze(size, size);  //width x height

        mazeRaw = maze.Display();
        return mazeRaw;
    }

    public static bool solve(ArrayList Maze, int row, int col, int frow, int fcol)
    {

        StringBuilder str = new StringBuilder(Maze[frow].ToString());//Set Below
        str[fcol] = 'F';
        Maze[frow] = str;

        char right = '#';
        char left = '#';
        char up = '#';
        char down = '#';

        if ((col + 1) < Maze[row].ToString().Length)
        {
            right = Maze[row].ToString()[col + 1];
        }
        if ((col - 1) > 0)
        {
            left = Maze[row].ToString()[col - 1];
        }
        if ((row - 1) > 0)
        {
            up = Maze[row - 1].ToString()[col];
        }
        if ((row + 1) < Maze[row].ToString().Length)
        {
            down = Maze[row + 1].ToString()[col];
        }
        //Debug.WriteLine("Right: " + right +", Left: " + left +", Above: " + up + ", Below: " + down);




        if (right == 'F' || left == 'F' || up == 'F' || down == 'F')
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = '.';
            Maze[row] = str;

            string file = Application.dataPath + "Maze.txt";
            StreamWriter writer = new StreamWriter(file);
            for (int i = 0; i < Maze.Count; i++)
            {
                for (int j = 0; j < Maze.Count; j++)
                {
                    writer.Write(Maze[i].ToString()[j]);
                }
                writer.WriteLine("");
            }
            writer.Close();
            return true;
        }


        bool solved = false;

        if (right == ' ' && !solved)
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = '.';
            Maze[row] = str;
            solved = solve(Maze, row, col + 1, frow, fcol);
        }
        if (down == ' ' && !solved)
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = '.';
            Maze[row] = str;
            solved = solve(Maze, row + 1, col, frow, fcol);
        }
        if (left == ' ' && !solved)
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = '.';
            Maze[row] = str;
            solved = solve(Maze, row, col - 1, frow, fcol);
        }
        if (up == ' ' && !solved)
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = '.';
            Maze[row] = str;
            solved = solve(Maze, row - 1, col, frow, fcol);
        }

        if (!solved && (Maze[row].ToString()[col] == '.'))
        {
            str = new StringBuilder(Maze[row].ToString());//Set Current position as .
            str[col] = ' ';
            Maze[row] = str;
            //solve(Maze, prevrow, prevcol, frow, fcol, row, col);
        }
        return solved;
    }
}




