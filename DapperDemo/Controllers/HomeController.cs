using System.Diagnostics;
using System.Net.Http;
using DapperDemo.DataAccess.Models;
using DapperDemo.DataAccess.Repository;
using DapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IDataRepository _dataRepository;
        private readonly string _apiKey = "183680ba-eab3-11ef-9159-0242ac130003-1836813c-eab3-11ef-9159-0242ac130003";

        public HomeController(HttpClient httpClient, ILogger<HomeController> logger, IDataRepository dataRepository)
        {
            _httpClient = httpClient;
            _dataRepository = dataRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Weather()
        {
            var httpClient = new HttpClient();
            var weatherApiUrl = $"https://www.7timer.info/bin/astro.php?lon=113.2&lat=23.1&ac=0&unit=metric&output=json&tzshift=0";
            var response = await _httpClient.GetAsync(weatherApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<Root>(jsonData);

                // Save to DB
                await _dataRepository.SaveRootData(weatherData);

                return Ok(weatherData);
            }

            return StatusCode((int)response.StatusCode, "Failed to fetch weather data");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
