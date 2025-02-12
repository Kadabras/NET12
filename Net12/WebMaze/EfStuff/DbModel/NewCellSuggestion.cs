﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.EfStuff.DbModel
{
    public class NewCellSuggestion : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MoneyChange { get; set; }
        public int HealtsChange { get; set; }
        public int FatigueChange { get; set; }
        public string Url { get; set; }

        public virtual User Creater { get; set; }

        public virtual User Approver { get; set; }
    }
}
