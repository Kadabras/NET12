﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Models.ValidationAttributes;

namespace WebMaze.Models
{
    public class SuggestedEnemysViewModel
    {
        public long Id { get; set; }

        [SwearWord("Cunt")]
        public string Name { get; set; }       
        public string Url { get; set; }
        [SwearWord("Cunt")]
        [WordCount(3)]
        public string Description { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public int GlobalUserRating { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
