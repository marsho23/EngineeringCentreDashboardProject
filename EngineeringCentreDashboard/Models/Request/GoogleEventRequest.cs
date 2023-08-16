namespace EngineeringCentreDashboard.Models.Request
{
    public class GoogleEventRequest
    {
        public IList<GoogleEvent> Events { get; set; }
        public GoogleEvent NewEvent { get; set; }
    }
}
