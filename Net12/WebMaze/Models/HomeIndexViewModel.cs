﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.Models.CurrencyDto;

namespace WebMaze.Models
{
    public class HomeIndexViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<CurrencyRateDto> Rates { get; set; }
    }
}
