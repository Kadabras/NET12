﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Net12.Maze
{
    public abstract class BaseCell : IBaseCell
    {
        public long Id { get; set; }
        public IMazeLevel Maze { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public BaseCell(int x, int y, IMazeLevel maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }


       public abstract bool TryToStep();

    }
}
