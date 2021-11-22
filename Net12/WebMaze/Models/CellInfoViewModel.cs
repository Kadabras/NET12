﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models
{
    public class CellInfoViewModel
    {
        public string Url { get; set; }
        public string Desc { get; set; }
        public bool CanStep { get; set; }

        private string[] _stringArray = new string[4];
        public string[] ShortDesc { get { return _stringArray; } }
        public string Spec { get; set; }
        public List<string> ShortsDescriptions { get; set; } = new List<string>();
    }
}
