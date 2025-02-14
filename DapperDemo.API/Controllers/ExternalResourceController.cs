using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DapperDemo.API.Models;
using DapperDemo.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DapperDemo.API.Controllers
{
    [Route("api/externalresource")]

    [ApiController]
    public class ExternalResourceController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly DataRepository _dataRepository;
        private readonly string _apiKey = "183680ba-eab3-11ef-9159-0242ac130003-1836813c-eab3-11ef-9159-0242ac130003"; 

        public ExternalResourceController(HttpClient httpClient, DataRepository dataRepository)
        {
            _httpClient = httpClient;
            _dataRepository = dataRepository;
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weatherApiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(weatherApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonData);

                // Save to DB
                await _dataRepository.SaveWeatherData(weatherData);

                return Ok(weatherData);
            }

            return StatusCode((int)response.StatusCode, "Failed to fetch weather data");
        }
    }
}
