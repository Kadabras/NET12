﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze
{
    public class Trap : BaseCell
    {
        public Trap(int x, int y, MazeLevel maze) : base(x, y, maze) { }

        public override bool TryToStep()
        {
            Maze.Hero.Health--;
            Maze[X, Y] = new Ground(X, Y, Maze);

            return true;
        }
    }
}
