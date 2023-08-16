using EngineeringCentreDashboard.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace EngineeringCentreDashboard.Interfaces
{
    public interface IGoogleCalendarHelper
    {
        public CalendarService AuthenticateServiceAccount(string jsonFilePath);
        public IList<CalendarEvent> GetEvents(CalendarService service, string calendarId);

    }
}
