﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.EfStuff.DbModel.SeaBattle
{
    public class SeaBattleField : BaseModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ShipCount { get; set; }
        public long LastHitToShip { get; set; }
        public bool IsEnemyField { get; set; }
        public virtual SeaBattleGame Game { get; set; }
        public virtual List<SeaBattleCell> Cells { get; set; }
    }
}
