﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze
{
    public class Bless : BaseCell
    {
        public Bless(int x, int y, IMazeLevel maze) : base(x,y,maze) { }

        public override bool TryToStep()
        {
            Maze.Hero.Hp = Maze.Hero.Max_hp;            
            return true;
        }
    }
}
