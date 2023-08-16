using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Interfaces;
using System.Globalization;

namespace EngineeringCentreDashboard.Business
{
    public class GoogleCalendarHelper : IGoogleCalendarHelper
    {
 

            private readonly string[] Scopes = { CalendarService.Scope.Calendar };
            private readonly string ApplicationName = "Calendar API Sample";

            public CalendarService AuthenticateServiceAccount(string jsonFilePath)
            {
                ServiceAccountCredential credential;
                using (var stream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                    credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(confg.ClientEmail) { Scopes = Scopes }.FromPrivateKey(confg.PrivateKey));
                }

                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Calendar API Sample",
                });

                return service;
            }

        //public IList<Event> GetEvents(CalendarService service, string calendarId)
        //{
        //    var request = service.Events.List(calendarId);
        //    request.TimeMin = DateTime.Now;
        //    request.ShowDeleted = false;
        //    request.SingleEvents = true;
        //    request.MaxResults = 10;
        //    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        //    var events = request.Execute();

        //    return events.Items;
        //}

        public IList<CalendarEvent> GetEvents(CalendarService service, string calendarId)
        {
            var request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var events = request.Execute();

            var calendarEvents = events.Items.Select(e => new CalendarEvent
            {
                Id = e.Id,
                Summary = e.Summary,
                Description = e.Description,
                StartDateTime = e.Start?.DateTime != null ? DateTimeOffset.Parse(e.Start.DateTime.ToString()) : (DateTimeOffset?)null,
                EndDateTime = e.End?.DateTime != null ? DateTimeOffset.Parse(e.End.DateTime.ToString()) : (DateTimeOffset?)null
            }).ToList();

            return calendarEvents;
        }





        // Additional methods for inserting/updating/deleting events...
        //    public Event InsertOrUpdateEvent(CalendarService service, string calendarId, GoogleEvent googleEvent)
        //    {
        //        var @event = new Event
        //        {
        //            Id = googleEvent.Id,
        //            Summary = googleEvent.Summary,
        //            Location = googleEvent.Location,
        //            Description = googleEvent.Description,
        //            Start = new EventDateTime { DateTime = googleEvent.Start },
        //            End = new EventDateTime { DateTime = googleEvent.End },
        //            Recurrence = googleEvent.Recurrence,
        //            Attendees = googleEvent.Attendees
        //        };

        //        try
        //        {
        //            return service.Events.Insert(@event, calendarId).Execute();
        //        }
        //        catch (Exception)
        //        {
        //            try
        //            {
        //                return service.Events.Update(@event, calendarId, @event.Id).Execute();
        //            }
        //            catch (Exception)
        //            {
        //                throw;  // handle exception
        //            }
        //        }
        //    }
    }

}


