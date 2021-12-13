﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using WebMaze.Services;

namespace WebMaze.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebContext _webContext;
        private UserService _userService;
        private UserRepository _userRepository;
        private ReviewRepository _reviewRepository;
        private NewCellSuggRepository _newCellSuggRepository;
        private MovieRepository _movieRepository;

        private SuggestedEnemysRepository _suggestedEnemysRepository;
        private FavGamesRepository _favGamesRepository;
        private IMapper _mapper;

        public HomeController(WebContext webContext,
         UserRepository userRepository, ReviewRepository reviewRepository,
         IMapper mapper, FavGamesRepository favGamesRepository, UserService userService, MovieRepository movieRepository)
        {
            _webContext = webContext;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _favGamesRepository = favGamesRepository;
            _userService = userService;
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

        public IActionResult Book()
        {
            var bookViewModels = new List<BookViewModel>();
            foreach (var dbBook in _webContext.Books)
            {
                var bookViewModel = new BookViewModel();
                bookViewModel.Name = dbBook.Name;
                bookViewModel.Link = dbBook.Link;
                bookViewModel.ImageLink = dbBook.ImageLink;
                bookViewModel.Author = dbBook.Author;
                bookViewModel.Desc = dbBook.Desc;
                bookViewModel.ReleaseDate = dbBook.ReleaseDate;
                bookViewModel.PublicationDate = dbBook.PublicationDate;
                bookViewModels.Add(bookViewModel);
            }

            return View(bookViewModels);
        }

        

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookViewModel bookViewModel)
        {
            var dbBook = new Book()
            {
                Name = bookViewModel.Name,
                Link = bookViewModel.Link,
                ImageLink = bookViewModel.ImageLink,
                Author = bookViewModel.Author,
                Desc = bookViewModel.Desc,
                ReleaseDate = bookViewModel.ReleaseDate,
                PublicationDate = bookViewModel.PublicationDate
            };
            _webContext.Books.Add(dbBook);

            _webContext.SaveChanges();

            return RedirectToAction("Index", "Home");
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
        public IActionResult FavoriteGames()
        {
            //var GamesViewModels = new List<GameViewModel>();
            var GamesViewModels = _favGamesRepository
               .GetAll()
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

            return RedirectToAction("FavoriteGames", "Home");
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
            review.Creator = _userService.GetCurrentUser();

            review.IsActive = true;
            _reviewRepository.Save(review);

            var FeedBackUsers = new List<FeedBackUserViewModel>();
            if (_reviewRepository.GetAll().Any())
            {
                FeedBackUsers = _reviewRepository.GetAll().Select(rev => _mapper.Map<FeedBackUserViewModel>(rev)).ToList();
            }
            return View(FeedBackUsers);
        }
        public IActionResult RemoveReview(long idReview)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var myUser = _userService.GetCurrentUser();
                if (myUser == _reviewRepository.Get(idReview).Creator)
                {
                    _reviewRepository.Remove(idReview);
                }

            }
            return RedirectToAction("Reviews", "Home");
        }

        public IActionResult Movie()
        {
            var MovieViewModels = new List<MovieViewModel>();
            MovieViewModels = _movieRepository
                .GetAll()
                .Select(dbModel => _mapper.Map<MovieViewModel>(dbModel))
                .ToList();


            return View(MovieViewModels);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddMovie(long id)
        {
            var model = _mapper.Map<MovieViewModel>(_movieRepository.Get(id)) ?? new MovieViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movieViewModel)
        {

            var dbMovie = new Movie();
            dbMovie = _mapper.Map<Movie>(movieViewModel);
            dbMovie.IsActive = true;
            _movieRepository.Save(dbMovie);
            return RedirectToAction($"{nameof(HomeController.Movie)}");
        }

        public IActionResult RemoveMovie(long id)
        {
            _movieRepository.Remove(id);
            return RedirectToAction($"{nameof(HomeController.Movie)}");
        }


    }


}


