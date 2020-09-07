using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCockpitCommunication.TCService;
using System.Net;
using TCGSync.Entities;

namespace TimeCockpitCommunication
{
    class TCBrooker : IBrooker
    {
        DataService TCService;
        string Username;

        public TCBrooker(string username, string password)
        {
            var service = new DataService(new Uri("https://apipreview.timecockpit.com/odata", UriKind.Absolute));
            service.Credentials = new NetworkCredential(username, password);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            TCService = service;
            Username = username;
        }

        public string CreateEvent(Event event1)
        {
            var tCEvent = event1.ToAPP_Timesheet();
            TCService.AddToAPP_Timesheet(tCEvent);
            TCService.SaveChanges();
            return tCEvent.APP_TimesheetUuid.ToString();
        }

        public HashSet<Event> GetEvents(DateTime start, DateTime end)
        {
            var timesheets = TCService.APP_Timesheet
                .Where(t => t.APP_BeginTime >= start && t.APP_EndTime <= end && Username == t.APP_UserDetail.APP_Username)
                .AsEnumerable();

            var eventHashSet = new HashSet<Event>();
            foreach (var timesheet in timesheets)
            {
                eventHashSet.Add(new TimeCockpitEvent(timesheet));
            }
            return eventHashSet;
        }

        public void SetEvent(Event event1) //TODO
        {
            Guid eventGuid = new Guid(event1.TCId); 
            APP_Timesheet tCEvent = TCService.APP_Timesheet.Where(t => t.APP_TimesheetUuid == eventGuid).First();
            tCEvent.APP_BeginTime = event1.Start;
            tCEvent.APP_EndTime = event1.End;
            tCEvent.APP_Description = event1.Description;
        }
    }

    public sealed class TimeCockpitEvent : Event
    {
        internal TimeCockpitEvent(APP_Timesheet timesheet)
        {
            TCId = timesheet.APP_TimesheetUuid.ToString();
            Start = timesheet.APP_BeginTime;
            End = timesheet.APP_EndTime;
            Description = timesheet.APP_Description;
        }
    }

    static class EventAbstractExtension
    {
        internal static APP_Timesheet ToAPP_Timesheet(this Event event1)
            => new APP_Timesheet
            { APP_BeginTime = event1.Start, APP_EndTime = event1.End, APP_Description = event1.Description };
    }
}
