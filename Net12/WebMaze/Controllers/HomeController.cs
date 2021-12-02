﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff;
using WebMaze.EfStuff.DbModel;
using WebMaze.EfStuff.Repositories;
using WebMaze.Models;

namespace WebMaze.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebContext _webContext;

        private UserRepository _userRepository;
        private ReviewRepository _reviewRepository;
        private NewCellSuggRepository _newCellSuggRepository;
        private StuffForHeroRepository _staffForHeroRepository;
        private MovieRepository _movieRepository;

        private IMapper _mapper;
        public HomeController(WebContext webContext,
            UserRepository userRepository, 
            ReviewRepository reviewRepository, 
            NewCellSuggRepository newCellSuggRepository, 
            StuffForHeroRepository staffForHeroRepository, 
            IMapper mapper,
            MovieRepository movieRepository)
        {
            _webContext = webContext;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _staffForHeroRepository = staffForHeroRepository;
            _mapper = mapper;
            _newCellSuggRepository = newCellSuggRepository;
        }

        public IActionResult Index()
        {
            var userViewModels = new List<UserViewModel>();
            foreach (var dbUser in _userRepository.GetAll())
            {
                var userViewModel = new UserViewModel();
                userViewModel.Id = dbUser.Id;
                userViewModel.UserName = dbUser.Name;
                userViewModel.Coins = dbUser.Coins;
                userViewModels.Add(userViewModel);
            }

            //var userViewModels2 = _webContext.Users.Select(
            //    dbModel => new UserViewModel { 
            //        UserName = dbModel.Name, 
            //        Coins = dbModel.Coins 
            //    });

            return View(userViewModels);
        }

        public IActionResult FavoriteGames()
        {
            var GamesViewModels = new List<GameViewModel>();
            var games = _webContext.FavGames.ToList();
            foreach (var dbGame in games)
            {
                var gameViewModel = new GameViewModel();
                gameViewModel.Name = dbGame.Name;
                gameViewModel.Genre = dbGame.Genre;
                gameViewModel.YearOfProd = dbGame.YearOfProd;
                gameViewModel.Desc = dbGame.Desc;
                gameViewModel.Rating = dbGame.Rating;
                if(dbGame.Creater != null)
                {
                    gameViewModel.Username = dbGame.Creater.Name;
                    gameViewModel.Age = dbGame.Creater.Age;
                }
                
                GamesViewModels.Add(gameViewModel);
            }

            return View(GamesViewModels);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(GameViewModel gameViewMode)
        {
            var creater = _webContext.Users
                .Where(x => x.Name == gameViewMode.Username)
                .FirstOrDefault();

            var dbGame = new Game()
            {
                Name = gameViewMode.Name,
                Genre = gameViewMode.Genre,
                YearOfProd = gameViewMode.YearOfProd,
                Desc = gameViewMode.Desc,
                Rating = gameViewMode.Rating,
                Creater = creater,
            };
            _webContext.FavGames.Add(dbGame);

            _webContext.SaveChanges();

            return RedirectToAction("FavoriteGames", "Home");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel userViewMode)
        {
            var dbUser = new User()
            {
                Name = userViewMode.UserName,
                Password = userViewMode.Password,
                Coins = userViewMode.Coins,
                Age = DateTime.Now.Second % 10 + 20,
                IsActive = true

            };

            _userRepository.Save(dbUser);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveUser(long userId)
        {
            _userRepository.Remove(userId);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Stuff()
        {
            var staffsForHero = new List<StuffForHeroViewModel>();
            staffsForHero = _staffForHeroRepository
                    .GetAll().Select(dbModel => _mapper.Map<StuffForHeroViewModel>(dbModel)).ToList();
            return View(staffsForHero);
        }

        [HttpGet]
        public IActionResult AddStuffForHero()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStuffForHero(StuffForHeroViewModel stuffForHeroViewModel)
        {
            //TODO user current user after login
            var proposer = _userRepository.GetAll()
                .OrderByDescending(x => x.Coins)
                .FirstOrDefault();

            var dbStuffForHero = _mapper.Map<StuffForHero>(stuffForHeroViewModel);

            dbStuffForHero.Proposer = proposer;
            dbStuffForHero.IsActive = true;

            _staffForHeroRepository.Save(dbStuffForHero);
            return RedirectToAction("Index", "Home", "AddStuffForHero");
        }

        public IActionResult Time()
        {
            var smile = DateTime.Now.Second;
            return View(smile);
        }

        [HttpGet]
        public IActionResult Sum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sum(int x, int y)
        {
            var model = x + y;
            return View(model);
        }


        [HttpGet]
        public IActionResult Reviews()
        {
            var FeedBackUsers = new List<FeedBackUserViewModel>();
            if (_userRepository.GetAll().Any())
            {
                FeedBackUsers = _reviewRepository.GetAll().Select(rev => _mapper.Map<FeedBackUserViewModel>(rev)).ToList();
            }

            return View(FeedBackUsers);
        }

        [HttpPost]
        public IActionResult Reviews(FeedBackUserViewModel viewReview)
        {
            // TODO: Selected User
            var review = _mapper.Map<Review>(viewReview);
            review.Creator = _userRepository.GetRandomUser();
            review.IsActive = true;
            _reviewRepository.Save(review);

            var FeedBackUsers = new List<FeedBackUserViewModel>();
            if (_reviewRepository.GetAll().Any())
            {
                FeedBackUsers = _reviewRepository.GetAll().Select(rev => _mapper.Map<FeedBackUserViewModel>(rev)).ToList();
            }
            return View(FeedBackUsers);
        }


        public IActionResult NewCellSugg()
        {
            var newCellSuggestionsViewModel = new List<NewCellSuggestionViewModel>();
            newCellSuggestionsViewModel = _newCellSuggRepository.GetAll()
                .Select(dbModel => _mapper.Map<NewCellSuggestionViewModel>(dbModel))
                .ToList();

            return View(newCellSuggestionsViewModel);
        }
        [HttpGet]
        public IActionResult AddNewCellSugg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewCellSugg(NewCellSuggestionViewModel newCell)
        {
            //TODO user current user after login
            var creater = _userRepository.GetRandomUser();

            var NewCS = new NewCellSuggestion()
            {
                Title = newCell.Title,
                Description = newCell.Description,
                MoneyChange = newCell.MoneyChange,
                HealtsChange = newCell.HealtsChange,
                FatigueChange = newCell.FatigueChange,
                Creater = creater,
                IsActive = true
            };

            _newCellSuggRepository.Save(NewCS);
            return RedirectToAction($"{nameof(HomeController.NewCellSugg)}");
        }
        public IActionResult RemoveNewCellSuggestion(long id)
        {
            _newCellSuggRepository.Remove(id);
            return RedirectToAction($"{nameof(HomeController.NewCellSugg)}");
        }

        public IActionResult Movie()
        {
            var MovieViewModels = new List<MovieViewModel>();
            foreach(var dbMovie in _movieRepository.GetAll())
            {
                var movieViewModel = new MovieViewModel()
                {
                    TitleGame = dbMovie.TitleGame,
                    TitleMovie = dbMovie.TitleMovie,
                    Release = dbMovie.Release,
                    Link = dbMovie.Link,
                    Img = dbMovie.Img
                };
                MovieViewModels.Add(movieViewModel);
            }
            return View(MovieViewModels);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movieViewModel)
        {
            var dbMovie = new Movie()
            {
                TitleGame = movieViewModel.TitleGame,
                TitleMovie = movieViewModel.TitleMovie,
                Release = movieViewModel.Release,
                Link = movieViewModel.Link,
                Img = movieViewModel.Img,
                IsActive = true
            };
            _movieRepository.Save(dbMovie);
            return View();
        }
    }
}
