﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Desc { get; set; }
        public string ReleaseDate { get; set; }

        public string Link { get; set; }
    }
}