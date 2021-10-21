﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze.Cells
{
    class Bless : BaseCell
    {
        public Bless(int x, int y, MazeLevel maze) : base(x,y,maze) { }

        public override bool TryToStep()
        {
            Maze.Hero.hp = 100;   
            return true;
        }
    }
}
