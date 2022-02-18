﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMaze.Models.CurrencyDto;

namespace WebMaze.Services
{
    public class CurrenceService
    {
        public async Task<List<CurrencyRateDto>> GetRates()
        {
            var client = new HttpClient();
            var dateStr = DateTime.Now.ToString("yyyy-M-d");//2016-7-6
            var uri = new Uri($"https://www.nbrb.by/api/exrates/rates?ondate={dateStr}&periodicity=0");
            
            
            var response = await client.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            var rates = JsonConvert.DeserializeObject<List<CurrencyRateDto>>(json);

            return rates;
        }
    }
}
