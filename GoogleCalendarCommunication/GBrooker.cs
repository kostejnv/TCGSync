using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TCGSync.Entities;


namespace GoogleCalendarCommunication
{
    /// <summary>
    /// Brooker that can communicate with Google Calendar
    /// </summary>
    public sealed class GBrooker : IBrooker
    {
        User user;
        /// <summary>
        /// Data servis for access to Google Calendar database
        /// </summary>
        private readonly CalendarService GService;
        /// <summary>
        /// Google Calendar permissions for this application
        /// </summary>
        public static readonly string[] Scopes = { CalendarService.Scope.Calendar };

        /// <summary>
        /// Constructor that create access to user's Google Calendar
        /// </summary>
        /// <param name="user">Google Calendar owner</param>
        public GBrooker(User user)
        {
            this.user = user;
            UserCredential credential = GUtil.GetCredentials(user);
            // Create Google Calendar API service.
            GService = GUtil.GetCalendarService(credential);
        }
        /// <summary>
        /// Get Hashset of events from user's Google Calendar in the interval
        /// </summary>
        /// <param name="start">Start of time interval</param>
        /// <param name="end">End of time interval</param>
        /// <returns></returns>
        public IEnumerable<TCGSync.Entities.Event> GetEvents(DateTime start, DateTime end)
        {
            // Define parameters of request.
            EventsResource.ListRequest request = GService.Events.List(user.googleCalendarId);
            request.TimeMin = start;
            request.TimeMax = end;
            request.ShowDeleted = false;
            request.SingleEvents = true;

            // List events.
            Events events = request.Execute();
            var glist = new List<TCGSync.Entities.Event>();
            foreach (var eventItem in events.Items)
            {
                glist.Add(new GoogleEvent(eventItem));
            }
            return glist;


        }
        /// <summary>
        /// Add new event in google calendar
        /// </summary>
        /// <param name="event1">added event</param>
        /// <returns>ID of event in google calendar</returns>
        public string CreateEvent(TCGSync.Entities.Event event1)
        {
            var googleEvent = event1.ToGoogleEvent();
            var request = GService.Events.Insert(googleEvent, user.googleCalendarId);
            Google.Apis.Calendar.v3.Data.Event response = request.Execute();
            return response.Id;

        }
        /// <summary>
        /// Edit Event with the same ID
        /// </summary>
        /// <param name="event1">Event with changes</param>
        public void SetEvent(TCGSync.Entities.Event event1)
        {
            EventsResource.GetRequest getRequest = new EventsResource.GetRequest(GService, user.googleCalendarId, event1.GoogleId);
            Google.Apis.Calendar.v3.Data.Event googleEvent = getRequest.Execute();
            googleEvent.Start = new EventDateTime() { DateTime = event1.Start };
            googleEvent.End = new EventDateTime() { DateTime = event1.End };
            googleEvent.Description = event1.Description;
            EventsResource.UpdateRequest updateRequest = new EventsResource.UpdateRequest(GService, googleEvent, user.googleCalendarId, event1.GoogleId);
            updateRequest.Execute();
        }
    }

    /// <summary>
    /// Event with specific parameter for Google Calendar
    /// </summary>
    sealed class GoogleEvent : TCGSync.Entities.Event
    {
        /// <summary>
        /// Constructor that creates GoogleEvent from Google Calendar Event
        /// </summary>
        /// <param name="event1">Google Calendar Event</param>
        internal GoogleEvent(Google.Apis.Calendar.v3.Data.Event event1)
        {
            TimeZone localTimeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = localTimeZone.GetUtcOffset(DateTime.Now);
            GoogleId = event1.Id;
            Start = event1.Start.DateTime + currentOffset;
            End = event1.End.DateTime + currentOffset;
            Description = event1.Summary;
        }
    }

    static class EventAbstractExtension
    {
        /// <summary>
        /// Extension method that converse EventAbstract to GoogleEvent
        /// </summary>
        /// <param name="event1"></param>
        /// <returns></returns>
        internal static Google.Apis.Calendar.v3.Data.Event ToGoogleEvent(this TCGSync.Entities.Event event1)
            => new Google.Apis.Calendar.v3.Data.Event
            {       
                Start = new EventDateTime() { DateTime = event1.Start },
                End = new EventDateTime() { DateTime = event1.End },
                Summary = event1.Description 
            };
    }
}
