﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze
{
    public class HealPotion : BaseCell
    {
        public HealPotion(int x, int y, IMazeLevel maze) : base(x, y, maze) { }

        public override bool TryToStep()
        {
            if (Maze.Hero.Hp <= 90)
                Maze.Hero.Hp = Maze.Hero.Hp + 10;
            else
                Maze.Hero.Hp = 100;

            Maze[X, Y] = new Ground(X, Y, Maze);
            return true;
        }
    }
}
