﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff.DbModel;
using WebMaze.EfStuff.DbModel.GuessTheNumber;
using WebMaze.EfStuff.DbModel.SeaBattle;

namespace WebMaze.EfStuff
{
    public class WebContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Gallery { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<NewCellSuggestion> NewCellSuggestions { get; set; }
        public DbSet<StuffForHero> StuffsForHero { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Game> FavGames { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<MazeDifficultProfile> MazeDifficultProfiles { get; set; }
        public DbSet<Perrmission> Perrmissions { get; set; }
        public DbSet<SuggestedEnemys> SuggestedEnemys { get; set; }

        public DbSet<MazeLevelWeb> MazeLevelsUser { get; set; }
        public DbSet<MazeCellWeb> CellsModels { get; set; }
        public DbSet<GameDevices> GameDevices { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<ZumaGameCell> ZumaGameCells { get; set; }
        public DbSet<ZumaGameColor> ZumaGameColors { get; set; }
        public DbSet<ZumaGameField> ZumaGameFields { get; set; }
        public DbSet<ZumaGameDifficult> ZumaGameDifficults { get; set; }
        public DbSet<GuessTheNumberGame> GuessTheNumberGames { get; set; }
        public DbSet<GuessTheNumberGameAnswer> GuessTheNumberGameAnswers { get; set; }
        public DbSet<GuessTheNumberGameParameters> GuessTheNumberGameParameters { get; set; }
        public DbSet<SeaBattleCell> SeaBattleCells { get; set; }
        public DbSet<SeaBattleField> SeaBattleFields { get; set; }
        public DbSet<SeaBattleGame> SeaBattleGames { get; set; }
        public DbSet<SeaBattleDifficult> SeaBattleDifficults { get; set; }
        public DbSet<RequestForMoney> RequestForMoneys { get; set; }

        public WebContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.MyCellSuggestions)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MyBugReports)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Perrmissions)
                .WithMany(x => x.UsersWhichHasThePermission);

            //modelBuilder.Entity<User>()
            //   .HasMany(x => x.CellSuggestionsWhichIAprove)
            //   .WithOne(x => x.Approver);
            modelBuilder.Entity<NewCellSuggestion>()
               .HasOne(x => x.Approver)
               .WithMany(x => x.CellSuggestionsWhichIAprove);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Author);

            modelBuilder.Entity<Image>()
               .HasOne(x => x.Author)
               .WithMany(x => x.Images);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Creator);

            modelBuilder.Entity<Book>()
               .HasOne(x => x.Creator)
               .WithMany(x => x.Books);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MyEnemySuggested)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<User>()
               .HasMany(x => x.EnemySuggestedWhichIAprove)
               .WithOne(x => x.Approver);

            modelBuilder.Entity<StuffForHero>()
               .HasOne(x => x.Proposer)
               .WithMany(x => x.AddedSStuff);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MyFavGames)
                .WithOne(x => x.Creater);

            modelBuilder.Entity<Game>()
               .HasOne(x => x.Creater)
               .WithMany(x => x.MyFavGames);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MyNews)
                .WithOne(x => x.Author);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MazeDifficultProfiles)
                .WithOne(x => x.Creater);



            modelBuilder.Entity<User>()
               .HasMany(x => x.NewsComments)
               .WithOne(x => x.Author);

            modelBuilder.Entity<News>()
               .HasMany(x => x.NewsComments)
               .WithOne(x => x.News);

            modelBuilder.Entity<ZumaGameField>()
                .HasMany(x => x.Cells)
                .WithOne(x => x.Field)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ZumaGameField>()
                .HasMany(x => x.Palette)
                .WithOne(x => x.Field)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ZumaGameField>()
                .HasOne(x => x.Gamer)
                .WithOne(x => x.ZumaGameField)
                .HasForeignKey<ZumaGameField>(x => x.GamerId);

            modelBuilder.Entity<ZumaGameDifficult>()
                .HasOne(x => x.Author)
                .WithMany(x => x.ZumaGameDifficults);

            modelBuilder.Entity<User>().HasMany(x => x.ListMazeLevels).WithOne(x => x.Creator);
            modelBuilder.Entity<MazeLevelWeb>().HasMany(x => x.Cells).WithOne(x => x.MazeLevel);
            modelBuilder.Entity<MazeLevelWeb>().HasMany(x => x.Enemies).WithOne(x => x.MazeLevel);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupList>().HasMany(x => x.Users).WithOne(x => x.Group);
            modelBuilder.Entity<GroupList>().HasOne(x => x.Creator).WithMany(x => x.Groups);
            modelBuilder.Entity<UserInGroup>().HasOne(x => x.User).WithMany(x => x.UsersInGroup);

            modelBuilder.Entity<GuessTheNumberGame>()
                .HasOne(x => x.Player)
                .WithMany(x => x.GuessTheNumberGames);

            modelBuilder.Entity<GuessTheNumberGame>()
               .HasOne(x => x.Parameters)
               .WithMany(x => x.Games);

            modelBuilder.Entity<GuessTheNumberGameAnswer>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Answers);

            modelBuilder.Entity<SeaBattleGame>()
                .HasOne(x => x.User)
                .WithOne(x => x.SeaBattleGame)
                .HasForeignKey<SeaBattleGame>(x => x.UserId);

            modelBuilder.Entity<SeaBattleGame>()
                .HasMany(x => x.Fields)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeaBattleField>()
                .HasMany(x => x.Cells)
                .WithOne(x => x.Field)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RequestForMoney>()
                .HasOne(x => x.RequestCreator)
                .WithMany(x => x.RequestCreators);

            modelBuilder.Entity<RequestForMoney>()
                .HasOne(x => x.RequestRecipient)
                .WithMany(x => x.RequestRecipients);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
