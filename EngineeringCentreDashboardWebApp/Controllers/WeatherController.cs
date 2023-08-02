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

namespace EngineeringCentreDashboardWebApp.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IUserLoginHelper _userLoginHelper;

        public WeatherController(IWeatherService weatherService, IUserLoginHelper userLoginHelper)
        {
            _weatherService = weatherService;
            this._userLoginHelper = userLoginHelper;


        }


        [Authorize]
        public async Task<IActionResult> Index()
        {

            //var email = User.FindFirstValue(ClaimTypes.Email);
            var email = User.FindFirstValue(ClaimTypes.Name);
            //string name;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isValidEmail = Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);

            if (isValidEmail)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //name = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ViewData["email"] = email;
                    //ViewData["name"] = name;
                    var weatherResponse = await _weatherService.GetForecastForToday("Manchester");
                    return View(weatherResponse);
                }
            }
            email = User.FindFirstValue(ClaimTypes.Email);
            ViewData["email"] = email;

            //name = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewData["name"] = name;


            var userLogin = await _userLoginHelper.GetOrCreateUser(email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { RedirectUri = "https://localhost:7187/Weather" };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect(authProperties.RedirectUri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string googleLogoutUrl = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7187";
           
            return Redirect(googleLogoutUrl);

        }

    }
}
