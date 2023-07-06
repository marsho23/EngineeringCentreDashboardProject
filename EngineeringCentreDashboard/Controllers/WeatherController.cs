using EngineeringCentreDashboard.Business;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace EngineeringCentreDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        //private WeatherHelper _weatherHelper;
        private readonly WeatherHelper _weatherHelper;

        public WeatherController(IConfiguration configuration)
        {
            _weatherHelper = new WeatherHelper(configuration, new RestClient());
        }
        //public WeatherController()
        //{
        //    _weatherHelper = new WeatherHelper("aad4d03ca4620f103c34e6578bdd3500");
        //}

        [HttpGet]
        public IActionResult Get()
        {
            String city = "Manchester";
            var weather = _weatherHelper.GetForecastForToday(city);
            return Ok(weather);
        }
    }
}
