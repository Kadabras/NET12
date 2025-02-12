﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze
{
    public class TeleportOut : BaseCell, ITeleportOut
    {
        public TeleportOut(int x, int y, MazeLevel maze) : base(x, y, maze) { }

        public override bool TryToStep()
        {
            return true;
        }
    }
}
