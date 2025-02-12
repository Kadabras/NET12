﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMaze.EfStuff.Repositories;
using AutoMapper;
using WebMaze.Services;
using WebMaze.Models;
using Microsoft.AspNetCore.Authorization;
using WebMaze.EfStuff.DbModel;
using WebMaze.Controllers.AuthAttribute;
using WebMaze.Models.Enums;

namespace WebMaze.Controllers
{
    public class GameController : Controller
    {
        private readonly FavGamesRepository _favGamesRepository;
       /* private readonly UserRepository _userRepository;*/
        private readonly IMapper _mapper;
        private readonly PayForActionService _payForActionService;
        private UserService _userService;
        private UserRepository _userRepository;

        public GameController(FavGamesRepository repository, 
            UserRepository userRepository, 
            IMapper mapper, 
            PayForActionService payForActionService, 
            UserService userService)
        {
            _favGamesRepository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _payForActionService = payForActionService;
            _userService = userService;
        }

        public IActionResult FavoriteGames(string gameFilter, bool ascDirection)
        {
            var GamesViewModels = _favGamesRepository
               .GetFavGamesSortedByProperty(gameFilter, ascDirection)
               .Select(dbModel => _mapper.Map<GameViewModel>(dbModel))
               .ToList();

            return View(GamesViewModels);
        }



        [Authorize]
        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [Authorize]
        [PayForAddActionFilter(TypesOfPayment.Small)]
        [HttpPost]
        public IActionResult AddGame(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(gameViewModel);
            }            

            var creater = _userService.GetCurrentUser();

            var dbGame = _mapper.Map<Game>(gameViewModel);
            dbGame.Creater = creater;
            dbGame.IsActive = true;

            _favGamesRepository.Save(dbGame);

            return RedirectToAction("FavoriteGames", "Game");
        }

        public IActionResult Wonderful(long gameId)
        {
            var game = _favGamesRepository.Get(gameId);
            _payForActionService.CreatorEarnMoney(game.Creater.Id, 10);

            return RedirectToAction("FavoriteGames", "Game");
        }
        
        public IActionResult Awful(long gameId)
        {
            var game = _favGamesRepository.Get(gameId);

            _payForActionService.CreatorDislikeFine(game.Creater.Id, TypesOfPayment.Fine);

            return RedirectToAction("FavoriteGames", "Game");
        }
    }
}
