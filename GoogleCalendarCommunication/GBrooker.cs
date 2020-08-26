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
using System.Threading.Tasks;
using TCGSyncIterfacesAndAbstract;


namespace GoogleCalendarCommunication
{
    class GBrooker : IBrooker
    {
        private CalendarService GService;
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "TCGSync";

        public GBrooker(string username)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file googleToken stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "googleToken";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    username,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;                
            }
            // Create Google Calendar API service.
            GService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public HashSet<EventAbstract> GetEvents(DateTime start, DateTime end)
        {
            // Define parameters of request.
            EventsResource.ListRequest request = GService.Events.List("primary");
            request.TimeMin = start;
            request.TimeMax = end;
            request.ShowDeleted = false;
            request.SingleEvents = true;

            // List events.
            Events events = request.Execute();
            var gHashset = new HashSet<EventAbstract>();
            foreach (var eventItem in events.Items)
            {
                gHashset.Add(new GoogleEvent(eventItem));
            }
            return gHashset;


        }

        public string CreateEvent(EventAbstract event1)
        {
            var googleEvent = event1.ToGoogleEvent();
            EventsResource.InsertRequest request = new EventsResource.InsertRequest(GService, googleEvent, "primary");
            Event response = request.Execute();
            return response.Id;

        }

        public void SetEvent(EventAbstract event1)
        {
            EventsResource.GetRequest getRequest = new EventsResource.GetRequest(GService, "primary", event1.ID);
            Event googleEvent = getRequest.Execute();
            googleEvent.Start = new EventDateTime() { DateTime = event1.Start };
            googleEvent.End = new EventDateTime() { DateTime = event1.End };
            googleEvent.Description = event1.Description;
            EventsResource.UpdateRequest updateRequest = new EventsResource.UpdateRequest(GService, googleEvent, "primary", event1.ID);
            updateRequest.Execute();
        }
    }


    class GoogleEvent : EventAbstract
    {

        internal GoogleEvent(Event event1)
        {
            ID = event1.Id;
            Start = event1.Start.DateTime;
            End = event1.End.DateTime;
            Description = event1.Description;
        }
    }
    
    static class EventAbstractExtension
    {
        internal static Event ToGoogleEvent(this EventAbstract event1)
            => new Event
                { Start = new EventDateTime() { DateTime = event1.Start },
                End = new EventDateTime() { DateTime = event1.End },
                Description = event1.Description };
    }
}
