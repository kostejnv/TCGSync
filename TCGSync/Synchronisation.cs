using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGSync.Entities;
using TimeCockpitCommunication;
using GoogleCalendarCommunication;
using System.Timers;
using System.Threading;

namespace TCGSync
{
    public static class Synchronization
    {
        
        private static System.Timers.Timer SyncTimer;

        public static void RunAutoSync()
        {
            if (SyncTimer != null)
            {
                SyncTimer.Stop();
                SyncTimer.Dispose();
            }
            SyncTimer = new System.Timers.Timer((double)UserDatabase.IntervalInMinutes * 60000);
            SyncTimer.Elapsed += Sync;
            SyncTimer.AutoReset = true;
            SyncTimer.Enabled = true;
        } 
        public static void SyncNow()
        {
            Thread t1 = new Thread(() => Sync());
            t1.Start();
        }

        private static void Sync()
        {
            lock (UserDatabase.userDatabase)
            {
                foreach (var user in UserDatabase.userDatabase)
                {
                    DateTime start = DateTime.Now - TimeSpan.FromDays(user.PastSyncInterval);
                    DateTime end;
                    if (user.IsFutureSpecified) end = DateTime.Now + TimeSpan.FromDays(user.FutureSyncInterval.Value);
                    else end = DateTime.MaxValue;
                    SyncTC(user, start, end);
                    SyncGoogle(user, start, end);
                }
                UserDatabase.SaveChanges();
            }
        }
        private static void Sync(Object source, ElapsedEventArgs e)
        {
            Sync();
        }


        private static void SyncTC(User user, DateTime start, DateTime end)
        {
            var tCBrooker = new TCBrooker(user);
            var events = tCBrooker.GetEvents(start, end);
            var newEvents = GetNewTCEvents(user, events);
            var modifiedEvents = GetModifiedTCEvents(user, events);
            GBrooker gBrooker = new GBrooker(user);
            foreach (var newEvent in newEvents)
            {
                var gID = gBrooker.CreateEvent(newEvent);
                user.AddEvent(newEvent.WithGoogleID(gID));
            }
            //foreach (var modifiedEvent in modifiedEvents)
            //{
            //    gBrooker.SetEvent(modifiedEvent);
            //    user.ChangeEvent(modifiedEvent);
            //}

        }

        private static void SyncGoogle(User user, DateTime start, DateTime end)
        {
            var gBrooker = new GBrooker(user);
            var events = gBrooker.GetEvents(start, end);
            var newEvents = GetNewGoogleEvents(user, events);
            var modifiedEvents = GetModifiedGoogleEvents(user, events);
            TCBrooker tCBrooker = new TCBrooker(user);
            foreach (var newEvent in newEvents)
            {
                var tcID = tCBrooker.CreateEvent(newEvent);
                user.AddEvent(newEvent.WithTCID(tcID));
            }
            //foreach (var modifiedEvent in modifiedEvents)
            //{
            //    tCBrooker.SetEvent(modifiedEvent);
            //    user.ChangeEvent(modifiedEvent);
            //}
        }

        private static IEnumerable<Event> GetNewTCEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToTCId;
            var newEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (!oldEvents.ContainsKey(actualEvent.TCId))
                {
                    newEvents.Add(actualEvent);
                }
            }
            return newEvents;
        }

        private static IEnumerable<Event> GetNewGoogleEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToGoogleId;
            var newEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (!oldEvents.ContainsKey(actualEvent.GoogleId))
                {
                    newEvents.Add(actualEvent);
                }
            }
            return newEvents;
        }

        private static IEnumerable<Event> GetModifiedTCEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToTCId;
            var modifiedEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (oldEvents.ContainsKey(actualEvent.TCId)
                    && !oldEvents[actualEvent.TCId].Equals(actualEvent))
                {
                    modifiedEvents.Add(actualEvent);
                }
            }
            return modifiedEvents;
        }
        private static IEnumerable<Event> GetModifiedGoogleEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToGoogleId;
            var modifiedEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (oldEvents.ContainsKey(actualEvent.GoogleId)
                    && !oldEvents[actualEvent.GoogleId].Equals(actualEvent))
                {
                    modifiedEvents.Add(actualEvent);
                }
            }
            return modifiedEvents;
        }
    }

    internal static class EventExtension
    {
        public static Event WithGoogleID(this Event event1, string GoogleID)
            => new Event()
            {
                GoogleId = GoogleID,
                TCId = event1.TCId,
                Description = event1.Description,
                Start = event1.Start,
                End = event1.End,
                Customer = event1.Customer
            };
        public static Event WithTCID(this Event event1, string TCID)
            => new Event()
            {
                TCId = TCID,
                GoogleId = event1.GoogleId,
                Description = event1.Description,
                Start = event1.Start,
                End = event1.End,
                Customer = event1.Customer
            };
    }
}
