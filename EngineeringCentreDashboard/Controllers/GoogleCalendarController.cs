using EngineeringCentreDashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static EngineeringCentreDashboard.Business.GoogleCalendarHelper;

namespace EngineeringCentreDashboard.Controllers
{
    [Route("api/[controller]")]
    public class GoogleCalendarController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public IActionResult GetCalendarEvents()
        {
            string jsonFile = "keys/engineering-centre-dashboard-e7295ccf8f0a.json";
            string calendarId = "maryumshouket@gmail.com";

            var calendarService = _googleCalendarHelper.AuthenticateServiceAccount(jsonFile);
            var events = _googleCalendarHelper.GetEvents(calendarService, calendarId);

            return Ok(events);
        }

            private readonly IGoogleCalendarHelper _googleCalendarHelper;

        public GoogleCalendarController(IGoogleCalendarHelper googleCalendarHelper)
        {
            _googleCalendarHelper = googleCalendarHelper;
        }

          
    }
}
