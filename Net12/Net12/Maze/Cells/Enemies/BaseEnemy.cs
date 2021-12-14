﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze.Cells.Enemies
{
    public abstract class BaseEnemy : BaseCell
    {
        public int Hp { get; set; } = 50;
        public BaseEnemy(int x, int y, IMazeLevel maze) : base(x, y, maze) 
        {
            X = x;
            Y = y;
        }

        public abstract void Step();

        public override bool TryToStep()
        {
            if((Maze.Hero.X == X && Math.Abs(Maze.Hero.Y-Y) == 1) || (Maze.Hero.Y == Y && Math.Abs(Maze.Hero.X - X) == 1))
            {
                Maze.Hero.Hp -= 10;
                Hp -= 10;
                if(Hp <= 0)
                {
                    Maze.Enemies.Remove(this);
                }
            }
            return false;
        }
    }
}
