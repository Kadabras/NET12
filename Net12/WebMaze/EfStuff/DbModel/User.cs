﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff.DbModel.GuessTheNumber;
using WebMaze.EfStuff.DbModel.ThreeInRow;
using WebMaze.EfStuff.DbModel.SeaBattle;

namespace WebMaze.EfStuff.DbModel
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int Coins { get; set; }
        public int GlobalUserRating { get; set; }

        public Language DefaultLocale { get; set; }

        public virtual List<Perrmission> Perrmissions { get; set; }
        public virtual List<NewCellSuggestion> MyCellSuggestions { get; set; }
        public virtual List<NewCellSuggestion> CellSuggestionsWhichIAprove { get; set; }
        public virtual List<StuffForHero> AddedSStuff { get; set; }
        public virtual List<SuggestedEnemys> MyEnemySuggested { get; set; }
        public virtual List<SuggestedEnemys> EnemySuggestedWhichIAprove { get; set; }
        public virtual List<Review> MyReviews { get; set; }
        public virtual List<Game> MyFavGames { get; set; }
        public virtual List<News> MyNews { get; set; }

        public virtual List<Image> Images { get; set; }
        public virtual List<Book> Books { get; set; }
        public virtual List<BugReport> MyBugReports { get; set; }
        public virtual List<GameDevices> MyGameDevices { get; set; }
        public virtual List<MazeDifficultProfile> MazeDifficultProfiles { get; set; }
        public virtual List<MazeLevelWeb> ListMazeLevels { get; set; }
        public virtual List<NewsComment> NewsComments { get; set; }
        public virtual List<MinerField> MinerFields { get; set; }
        public virtual ZumaGameField ZumaGameField { get; set; }
        public virtual List<ZumaGameDifficult> ZumaGameDifficults { get; set; }
        public virtual List<GroupList> Groups { get; set; }
        public virtual List<UserInGroup> UsersInGroup { get; set; }
        public virtual List<GuessTheNumberGame> GuessTheNumberGames { get; set; }
        public virtual List<ThreeInRowGameField> ThreeInRowGameFields { get; set; }
        public virtual SeaBattleGame SeaBattleGame { get; set; }
        public virtual List<RequestForMoney> RequestRecipients { get; set; }
        public virtual List<RequestForMoney> RequestCreators { get; set; }
    }
}
