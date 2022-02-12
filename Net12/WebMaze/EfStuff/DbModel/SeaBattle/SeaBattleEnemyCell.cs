﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.EfStuff.DbModel.SeaBattle
{
    public class SeaBattleEnemyCell : BaseModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ShipLength { get; set; }
        public bool ShipHere { get; set; }
        public bool Hit { get; set; }
        public virtual SeaBattleEnemyField Field { get; set; }
    }
}
