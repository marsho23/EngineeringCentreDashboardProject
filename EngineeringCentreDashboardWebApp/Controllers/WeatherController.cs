using EngineeringCentreDashboardWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Interfaces;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Google;
using System.Web;
using Google.Apis.Calendar.v3;
using Newtonsoft.Json;
using EngineeringCentreDashboardWebApp.Models;

namespace EngineeringCentreDashboardWebApp.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IUserLoginHelper _userLoginHelper;
        private readonly IGoogleCalendarHelper _googleCalendarHelper;
        private readonly HttpClient _httpClient;


        public WeatherController(IWeatherService weatherService, IUserLoginHelper userLoginHelper, IGoogleCalendarHelper googleCalendarHelper, HttpClient httpClient)
        {
            _weatherService = weatherService;
            this._userLoginHelper = userLoginHelper;
            this._googleCalendarHelper = googleCalendarHelper;
            _httpClient = httpClient;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {

            //var name = User.FindFirstValue(ClaimTypes.GivenName);
            var email = User.FindFirstValue(ClaimTypes.Name);
            ViewData["email"] = email;

            string name;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isValidEmail = Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);

            if (isValidEmail)
            {
                if (User.Identity.IsAuthenticated)
                {
                    name = User.FindFirstValue(ClaimTypes.GivenName);
                    ViewData["email"] = email;
                    //ViewData["name"] = name;
                    //var weatherResponse = await _weatherService.GetForecastForToday("Manchester");
                    //return View(weatherResponse);
                }
                else
                {
                    email = User.FindFirstValue(ClaimTypes.Email);
                    ViewData["email"] = email;
                }
            }
            var weatherResponse = await _weatherService.GetForecastForToday("Manchester");



            name = User.FindFirstValue(ClaimTypes.GivenName);
            ViewData["name"] = name;


            var userLogin = await _userLoginHelper.GetOrCreateUser(email);
            //ViewData["id"]=userLogin.Id;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { RedirectUri = "https://localhost:7187/Weather" };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


            string webApiBaseUrl = "https://localhost:7181/api/";

            HttpResponseMessage response = await _httpClient.GetAsync(webApiBaseUrl + "UserLogin/getByEmail/" + email);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Deserialize the content into a UserResponseModel object
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(content);
                // Now you can access the ID from the deserialized object
                int userId = userResponse.Id;
                ViewData["userId"] = userId;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
            }


            return View(weatherResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string googleLogoutUrl = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7187";

            return Redirect(googleLogoutUrl);

        }

        //[HttpGet]
        //public IActionResult GetCalendarEvents()
        //{
        //    string jsonFile = "keys/engineering-centre-dashboard-e7295ccf8f0a.json";
        //    string calendarId = "maryumshouket@gmail.com";

        //    var calendarService = _googleCalendarHelper.AuthenticateServiceAccount(jsonFile);
        //    var events = _googleCalendarHelper.GetEvents(calendarService, calendarId);

        //    return Ok(events);
        //}
    }
}
