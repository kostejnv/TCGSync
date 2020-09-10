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
    public class TCBrooker : IBrooker
    {
        DataService TCService;
        User user;

        public TCBrooker(User user)
        {
            var service = new DataService(new Uri("https://apipreview.timecockpit.com/odata", UriKind.Absolute));
            service.Credentials = new NetworkCredential(user.TCUsername, user.TCPassword);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            TCService = service;
            this.user = user;
        }

        public string CreateEvent(Event event1)
        {
            var tCEvent = event1.ToAPP_Timesheet(user, TCService);
            TCService.AddToAPP_Timesheet(tCEvent);
            TCService.SaveChanges();
            return tCEvent.APP_TimesheetUuid.ToString();
        }

        public IEnumerable<Event> GetEvents(DateTime start, DateTime end)
        {
            var timesheets = TCService.APP_Timesheet
                .Where(t => t.APP_BeginTime >= start && t.APP_EndTime <= end && user.TCUsername == t.APP_UserDetail.APP_Username)
                .AsEnumerable();

            var eventlist = new List<Event>();
            foreach (var timesheet in timesheets)
            {
                eventlist.Add(new TimeCockpitEvent(timesheet));
            }
            return eventlist;
        }

        public void SetEvent(Event event1) //TODO
        {
            APP_Timesheet tCEvent = TCService.APP_Timesheet.Where(t => t.APP_TimesheetUuid == new Guid(event1.TCId)).First();
            tCEvent.APP_BeginTime = event1.Start;
            tCEvent.APP_EndTime = event1.End;
            tCEvent.APP_Description = event1.Description;
            TCService.UpdateObject(tCEvent);
            TCService.SaveChanges();
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
        internal static APP_Timesheet ToAPP_Timesheet(this Event event1, User user, DataService services)
            => new APP_Timesheet
            {
                APP_BeginTime = event1.Start,
                APP_EndTime = event1.End,
                APP_Description = event1.Description,
                APP_NoBilling = false,
                APP_UserDetail = services.APP_UserDetail.Where(t => t.APP_Username == user.TCUsername).First(),
                APP_UserDetailUuid = services.APP_UserDetail.Where(t => t.APP_Username == user.TCUsername).First().APP_UserDetailUuid
            };
    }
}
