//using EngineeringCentreDashboard.Interfaces;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace EngineeringCentreDashboardWebApp.Controllers
//{
//    [AllowAnonymous, Route("account")]
//    public class AccountController : Controller
//    {
//        private readonly IUserLoginHelper _userLoginHelper;
//        public AccountController(IUserLoginHelper userLoginHelper)
//        {
//            this._userLoginHelper = userLoginHelper;
//        }
//        [Route("google-login")]
//        public IActionResult GoogleLogin()
//        {
//            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
//            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
//        }

//        [Route("google-response")]
//        public async Task<IActionResult> GoogleResponse()
//        {
//            var googleId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            var email = User.FindFirstValue(ClaimTypes.Email);

//            if (googleId == null)
//            {
//                throw new ArgumentNullException("GoogleId is null");
//            }
//            else
//            {
//                Console.WriteLine("google id is "+ googleId);
//            }

//            var user = await _userLoginHelper.GetOrCreateUser(googleId, email, );
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, email)
//            };

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var authProperties = new AuthenticationProperties { RedirectUri = "https://localhost:7187/Weather" };

//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//            return Redirect(authProperties.RedirectUri);
//        }

//    }
//}
