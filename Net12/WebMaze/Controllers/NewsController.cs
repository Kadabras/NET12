﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff;
using WebMaze.EfStuff.DbModel;
using WebMaze.EfStuff.Repositories;
using WebMaze.Models;

namespace WebMaze.Controllers
{
    public class NewsController : Controller
    {
        private WebContext _webContext;

        private NewsRepository _newsRepository;
        public NewsController(WebContext webContext, NewsRepository newsRepository)
        {
            _webContext = webContext;
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            var newsViewModels = new List<NewsViewModel>();
            newsViewModels = _newsRepository.GetAll()
                .Select(x => new NewsViewModel
                {
                    CreationDate = x.CreationDate,
                    EventDate = x.EventDate,
                    Location = x.Location,
                    NameOfAuthor = x.Author.Name,
                    Text = x.Text,
                    Title = x.Title,
                    Id=x.Id
                }).ToList();

            return View(newsViewModels);
        }

        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddNews(NewsViewModel newsViewModel)
        {
            var author = _webContext
                   .Users
                   .OrderBy(x => x.Name)
                   .FirstOrDefault();

            var dbNews = new News()
            {
                EventDate = newsViewModel.EventDate,
                CreationDate = DateTime.Now.Date,
                Location = newsViewModel.Location,
                Author = author,
                Text = newsViewModel.Text,
                Title = newsViewModel.Title
            };
            _newsRepository.Save(dbNews);

            return RedirectToAction("Index", "News");
        }

        public IActionResult RemoveNews(long newsId)
        {
            _newsRepository.Remove(newsId);
            return RedirectToAction("Index", "News");
        }
    }
}
