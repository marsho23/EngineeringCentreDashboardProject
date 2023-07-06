using EngineeringCentreDashboardWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringCentreDashboardWebApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            var weatherResponse = await _weatherService.GetForecastForToday("Manchester");
            return View(weatherResponse);
        }

    }
}
