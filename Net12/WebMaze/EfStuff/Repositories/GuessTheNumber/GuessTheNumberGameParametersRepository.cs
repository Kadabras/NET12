﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff.DbModel.GuessTheNumber;
using WebMaze.Services.GuessTheNumber;

namespace WebMaze.EfStuff.Repositories.GuessTheNumber
{
    public class GuessTheNumberGameParametersRepository : BaseRepository<GuessTheNumberGameParameters>
    {
        public GuessTheNumberGameParametersRepository(WebContext webContext) : base(webContext)
        {
        }
        public virtual GuessTheNumberGameParameters Get(GuessTheNumberGameDifficulty difficulty)
        {
            return _dbSet.SingleOrDefault(p => p.Difficulty == difficulty);
        }
    }
}
