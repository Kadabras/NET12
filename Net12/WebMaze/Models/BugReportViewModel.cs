﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Models.ValidationAttributes;

namespace WebMaze.Models
{
    public class BugReportViewModel
    {
        public string CreaterName { get; set; }

        [Required(ErrorMessage = "fff")]
        [BadWordsBugReports("damn", "hate", "ass")]
        [MinLength(10, ErrorMessage = "This description is too short, write more please :)")]
        public string Description { get; set; }
    }
}
