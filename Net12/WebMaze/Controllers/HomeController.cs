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
        private WebContext _webContext;
        private NewCellSuggRepository _newCellSuggRepository;
        private UserRepository _userRepository;
        private ReviewRepository _reviewRepository;
        private SuggestedEnemysRepository _suggestedEnemysRepository;
        private IMapper _mapper;
        public HomeController(WebContext webContext,
            UserRepository userRepository, ReviewRepository reviewRepository,
            SuggestedEnemysRepository suggestedEnemysRepository,
            IMapper mapper, NewCellSuggRepository newCellSuggRepository)
        {
            _webContext = webContext;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _suggestedEnemysRepository = suggestedEnemysRepository;
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

        public IActionResult SuggestedEnemys()
        {
            var suggestedEnemysViewModels = new List<SuggestedEnemysViewModel>();
            var suggestedEnemys = _webContext.SuggestedEnemys.ToList();

            suggestedEnemysViewModels = _suggestedEnemysRepository
               .GetAll()
               .Select(dbModel => _mapper.Map<SuggestedEnemysViewModel>(dbModel))
               .ToList();

            return View(suggestedEnemysViewModels);
        }

        public IActionResult RemoveSuggestedEnemy(long suggestedEnemysId)
        {
            _suggestedEnemysRepository.Remove(suggestedEnemysId);
            return RedirectToAction($"{nameof(HomeController.SuggestedEnemys)}");
        }
 
        [HttpGet]
        public IActionResult AddSuggestedEnemy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSuggestedEnemy(SuggestedEnemysViewModel suggestedEnemysViewModel)
        {
            var creater = _userRepository.GetRandomUser();


            var dbSuggestedEnemys = new SuggestedEnemys();

            dbSuggestedEnemys = _mapper.Map<SuggestedEnemys>(suggestedEnemysViewModel);
            dbSuggestedEnemys.Creater = creater;
            dbSuggestedEnemys.IsActive = true;

            _suggestedEnemysRepository.Save(dbSuggestedEnemys);

            return RedirectToAction($"{nameof(HomeController.SuggestedEnemys)}");
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
                FeedBackUsers = _reviewRepository.GetAll().Select(rev => new FeedBackUserViewModel { UserName = rev.Creator.Name, TextInfo = rev.Text, Rate = rev.Rate }).ToList();
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
                FeedBackUsers = _reviewRepository.GetAll().Select(rev => new FeedBackUserViewModel { UserName = rev.Creator.Name, TextInfo = rev.Text, Rate = rev.Rate }).ToList();
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


    }

}
