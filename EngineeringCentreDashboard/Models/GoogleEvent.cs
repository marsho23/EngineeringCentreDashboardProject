using Google.Apis.Calendar.v3.Data;

namespace EngineeringCentreDashboard.Models
{
    public class GoogleEvent
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<string> Recurrence { get; set; }
        public IList<EventAttendee> Attendees { get; set; }
    }
}
