﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze.Cells
{
    public class Puddle : BaseCell
    {
        public Puddle(int x, int y, IMazeLevel maze):base(x,y,maze)
        {
        }

        public override bool TryToStep()
        {
            Maze.Hero.CurrentFatigue++;
            Maze.Message = "+1Fatigue";
            return true;
        }
    }
}
