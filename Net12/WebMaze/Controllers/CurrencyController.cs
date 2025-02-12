﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMaze.Models.CurrencyDto;
using WebMaze.Services;

namespace WebMaze.Controllers
{
    public class CurrencyController : Controller
    {
        private CurrenceService _currenceService;

        public CurrencyController(CurrenceService currenceService)
        {
            _currenceService = currenceService;
        }

        public async Task<ActionResult> Index()
        {
            var currencies = await _currenceService.GetAllCurrencies();

            return View(currencies);
        }

        public async Task<IActionResult> GetRateById(int currencyId)
        {           
            var rate = await _currenceService.GetRateById(currencyId);

            return View(rate);
        }
        public async Task<IActionResult> GetRateByIdOnDate(int currencyId, DateTime date)
        {            
            var rate = await _currenceService.GetRateByIdOnDate(currencyId, date);

            return View(rate);
        }
        public IActionResult GetRateByIdOnPeriod(int currencyId, DateTime onStartDate, DateTime onEndDate)
        {
            return View();
        }
        public async Task<IActionResult> GetRateByIdOnPeriodJson(int currencyId, DateTime onStartDate, DateTime onEndDate)
        {
            var rateList = await _currenceService.GetRateByIdOnPeriod(currencyId, onStartDate, onEndDate);
            foreach (var rate in rateList)
            {
                var date = rate.Date.Split("T");
                rate.Date = date[0];
            }

            return Json(rateList);
        }
    }
}
