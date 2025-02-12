﻿using AutoMapper;
using Net12.Maze;
using Net12.Maze.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMaze.EfStuff.DbModel;
using WebMaze.EfStuff.DbModel.Maze;
using WebMaze.Models;
using WebMaze.Services;

namespace WebMaze.EfStuff.Repositories
{
    public class MazeLevelRepository : BaseRepository<MazeLevelWeb>
    {

        public MazeLevelRepository(WebContext webContext) : base(webContext)
        {
        }
        public void ChangeModel(MazeLevelWeb model, MazeLevel maze, IMapper mapper)
        {
            var dict = new Dictionary<Type, MazeCellInfo>()
            {
                { typeof(Wall), MazeCellInfo.Wall},
                { typeof(WeakWall), MazeCellInfo.WeakWall},
                { typeof(Ground), MazeCellInfo.Ground},
                { typeof(GoldMine), MazeCellInfo.Goldmine},
                { typeof(Coin), MazeCellInfo.Coin},
                { typeof(Bed),MazeCellInfo.Bed},
                { typeof(Puddle), MazeCellInfo.Puddle},
                { typeof(VitalityPotion), MazeCellInfo.VitalityPotion},
                { typeof(Bless), MazeCellInfo.Bless},
                { typeof(TeleportIn), MazeCellInfo.TeleportIn},
                { typeof(TeleportOut), MazeCellInfo.TeleportOut},
                { typeof(Fountain), MazeCellInfo.Fountain},
                { typeof(Trap), MazeCellInfo.Trap},
                { typeof(HealPotion), MazeCellInfo.HealPotion},
                { typeof(WolfPit), MazeCellInfo.WolfPit},
                { typeof(Tavern), MazeCellInfo.Tavern},
                { typeof(Healer), MazeCellInfo.Healer},
                { typeof(Exit), MazeCellInfo.Exit}
            };

            model.Height = maze.Height;
            model.Width = maze.Width;
            model.HeroMaxFatigure = maze.Hero.MaxFatigue;
            model.HeroMaxHp = maze.Hero.Max_hp;
            model.HeroNowFatigure = maze.Hero.CurrentFatigue;
            model.HeroNowHp = maze.Hero.Hp;
            model.HeroX = maze.Hero.X;
            model.HeroY = maze.Hero.Y;
            model.Message = maze.Message;

            foreach (var cell in maze.Cells)
            {
                var cellModel = model.Cells.Single(c => c.X == cell.X && c.Y == cell.Y);

                var mod = mapper?.Map<MazeCellWeb>(cell);
                if (mod != null)
                {
                    cellModel.TypeCell = mod.TypeCell;
                    cellModel.Obj1 = mod.Obj1;
                    cellModel.Obj2 = mod.Obj2;
                }
            }
            var listActiveEnemies = new List<MazeEnemyInfo>();
            model.Enemies.ForEach(e => e.IsActive = false);
            foreach (var enemy in maze.Enemies)
            {
                var enemyModel = model.Enemies.Single(e => e.Id == enemy.Id);
                listActiveEnemies.Add(enemyModel.TypeEnemy);
                var mod = mapper.Map<MazeEnemyWeb>(enemy);
                if (mod != null)
                {
                    enemyModel.TypeEnemy = mod.TypeEnemy;
                    enemyModel.Obj1 = mod.Obj1;
                    enemyModel.Obj2 = mod.Obj2;
                    enemyModel.X = mod.X;
                    enemyModel.Y = mod.Y;
                    enemyModel.Hp = mod.Hp;
                    enemyModel.IsActive = true;
                }
            }

            model.Enemies = model.Enemies.Where(e =>
            {
                return e.IsActive;
            }).ToList();



        }



    }
}
