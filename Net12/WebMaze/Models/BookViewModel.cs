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
        public string CreatorName { get; set; }
        public string Desc { get; set; }
        public string ReleaseDate { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }

        public string PublicationDate { get; set; } = DateTime.Now.ToShortDateString();
    }
}