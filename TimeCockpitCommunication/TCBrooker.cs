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

    /// <summary>
    /// Brooker that can communicate with Time Cockpit
    /// </summary>
    public sealed class TCBrooker : IBrooker
    {

        /// <summary>
        /// Data service for accass to Time Cockpit
        /// </summary>
        private readonly DataService TCService;

        /// <summary>
        /// user that is associeted with brooker
        /// </summary>
        private readonly User user;

        /// <summary>
        /// Constructor that create access to user's Time Cockpit
        /// </summary>
        /// <param name="user"></param>
        public TCBrooker(User user)
        {
            if (!TCCredentialsManager.Exists(user.Username)) throw new InvalidOperationException("User has not TC credentials");
            var service = TCUtils.GetService(TCCredentialsManager.Get(user.Username));
            TCService = service;
            this.user = user;
        }

        /// <summary>
        /// Get IEnumerable of events from user's Time Cockpit in the intervall
        /// </summary>
        /// <param name="start">Start of time intervall</param>
        /// <param name="end">End of time intervall</param>
        /// <returns></returns>
        public IEnumerable<Event> GetEvents(DateTime start, DateTime end)
        {
            // get Time Cockpit timesheets
            var timesheets = TCService.APP_Timesheet
                .Where(t => t.APP_BeginTime >= start && t.APP_EndTime <= end && user.Username == t.APP_UserDetail.APP_Username)
                .AsEnumerable();

            // convert Timsheets to List<Event>
            var eventlist = new List<Event>();
            foreach (var timesheet in timesheets)
            {
                eventlist.Add(new TimeCockpitEvent(timesheet));
            }
            return eventlist;
        }

        /// <summary>
        /// Add new event to Time Cockpit
        /// </summary>
        /// <param name="event1"></param>
        /// <returns></returns>
        public string CreateEvent(Event event1)
        {
            var tCEvent = event1.ToAPP_Timesheet(user, TCService);
            TCService.AddToAPP_Timesheet(tCEvent);
            TCService.SaveChanges();
            return tCEvent.APP_TimesheetUuid.ToString();
        }

        /// <summary>
        /// Edit Event with the same ID,
        /// IT DOES NOT WORK
        /// </summary>
        /// <param name="event1"></param>
        public void EditEvent(Event event1)
        {
            APP_Timesheet tCEvent = TCService.APP_Timesheet.Where(t => t.APP_TimesheetUuid == new Guid(event1.TCId)).First();
            tCEvent.APP_BeginTime = event1.Start;
            tCEvent.APP_EndTime = event1.End;
            tCEvent.APP_Description = event1.Description;
            TCService.UpdateObject(tCEvent);
            TCService.SaveChanges();
        }
    }

    /// <summary>
    /// Event with specific parameter for Time Cockpit
    /// </summary>
    internal sealed class TimeCockpitEvent : Event
    {

        /// <summary>
        /// Constructor that creates TimeCockpitEvent from APP_Timesheet
        /// </summary>
        /// <param name="timesheet">Time Cockpit Timesheet</param>
        internal TimeCockpitEvent(APP_Timesheet timesheet)
        {
            TCId = timesheet.APP_TimesheetUuid.ToString();
            Start = timesheet.APP_BeginTime;
            End = timesheet.APP_EndTime;
            Description = timesheet.APP_Description;
        }
    }

    internal static class EventExtension
    {
        /// <summary>
        /// Extension method that converse Event to TimeCockpitEvent
        /// </summary>
        /// <param name="event1"></param>
        /// <param name="user"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static APP_Timesheet ToAPP_Timesheet(this Event event1, User user, DataService services)
            => new APP_Timesheet
            {
                APP_BeginTime = event1.Start,
                APP_EndTime = event1.End,
                APP_Description = event1.Description,
                APP_NoBilling = false,
                APP_UserDetail = services.APP_UserDetail.Where(t => t.APP_Username == user.Username).First(),
                APP_UserDetailUuid = services.APP_UserDetail.Where(t => t.APP_Username == user.Username).First().APP_UserDetailUuid
            };
    }
}
