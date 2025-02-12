﻿using System.Collections.Generic;
using WebMaze.EfStuff.DbModel;

namespace WebMaze.Models
{
    public class GroupListViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public User Creator { get; set; }
        public List<UserInGroupViewModel> Users { get; set; }
    }
}
