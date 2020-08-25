using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGSyncIterfacesAndAbstract;
using TimeCockpitCommunication.TCService;
using System.Net;

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

        public Guid CreateEvent(EventAbstract event1)
        {
            var tCEvent = event1.ToAPP_Timesheet();
            TCService.AddToAPP_Timesheet(tCEvent);
            TCService.SaveChanges();
            return tCEvent.APP_TimesheetUuid;
        }

        public HashSet<EventAbstract> GetEvents(DateTime start, DateTime end)
        {
            var timesheets = TCService.APP_Timesheet
                .Where(t => t.APP_BeginTime >= start && t.APP_EndTime <= end && Username == t.APP_UserDetail.APP_Username)
                .AsEnumerable();

            var eventHashSet = new HashSet<EventAbstract>();
            foreach (var timesheet in timesheets)
            {
                eventHashSet.Add(new TimeCockpitEvent(timesheet));
            }
            return eventHashSet;
        }

        public void SetEvent(EventAbstract event1)
        {
            APP_Timesheet tCEvent = TCService.APP_Timesheet.Where(t => t.APP_TimesheetUuid == event1.ID).First();
            tCEvent.APP_BeginTime = event1.Start;
            tCEvent.APP_EndTime = event1.End;
            tCEvent.APP_Description = event1.Description;
        }
    }

    public sealed class TimeCockpitEvent : EventAbstract
    {
        internal TimeCockpitEvent(APP_Timesheet timesheet)
        {
            ID = timesheet.APP_TimesheetUuid;
            Start = timesheet.APP_BeginTime;
            End = timesheet.APP_EndTime;
            Description = timesheet.APP_Description;
        }
    }

    static class EventAbstractExtension
    {
        internal static APP_Timesheet ToAPP_Timesheet(this EventAbstract event1)
            => new APP_Timesheet
            { APP_BeginTime = event1.Start, APP_EndTime = event1.End, APP_Description = event1.Description };
    }
}
