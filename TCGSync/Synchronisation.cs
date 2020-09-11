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
using TCGSync.UI;

namespace TCGSync
{
    /// <summary>
    /// Class that synchronize both account
    /// </summary>
    public static class Synchronization
    {

        /// <summary>
        /// Timer for automatic run Sync Method
        /// </summary>
        private static System.Timers.Timer SyncTimer;

        /// <summary>
        /// Automatic synchronisation
        /// </summary>
        public static void RunAutoSync()
        {
            if (SyncTimer != null)
            {
                SyncTimer.Stop();
                SyncTimer.Dispose();
            }
            lock (DataDatabase.IntervalInMinutesLocker)
                SyncTimer = new System.Timers.Timer((double)(DataDatabase.IntervalInMinutes * 60 * 1000));
            SyncTimer.Elapsed += Sync;
            SyncTimer.AutoReset = true;
            SyncTimer.Enabled = true;
            SyncInfoGiver.RunTimerForUser();
        }

        /// <summary>
        /// Stop automatic synchronisation
        /// </summary>
        public static void StopAutoSync()
        {
            SyncTimer.Stop();
            SyncInfoGiver.StopTimerForUser();
        }

        /// <summary>
        /// Start automatic synchronisation after its stop
        /// </summary>
        public static void ContinueAutoSync()
        {
            SyncTimer.Enabled = true;
            SyncInfoGiver.ContinueTimerForUser();
        }

        /// <summary>
        /// immediate synchronisation in other thread
        /// </summary>
        public static void SyncNow()
        {
            Thread t1 = new Thread(() => Sync());
            t1.Start();
        }

        #region Privat Methods
        /// <summary>
        /// thread save synchronisation
        /// </summary>
        private static void Sync()
        {
            lock (SyncInfoGiver.SyncInfoGiverLocker)
                SyncInfoGiver.SyncInfo = "Synchronisation has started";
            lock (SyncInfoGiver.SyncInfoGiverLocker)
                SyncInfoGiver.IsProccessingSync = true;
            try
            {
                lock (DataDatabase.userDatabase)
                {
                    foreach (var user in DataDatabase.userDatabase)
                    {
                        DateTime start = DateTime.Now - TimeSpan.FromDays(user.PastSyncInterval);
                        DateTime end;
                        if (user.IsFutureSpecified) end = DateTime.Now + TimeSpan.FromDays(user.FutureSyncInterval.Value);
                        else end = DateTime.MaxValue;
                        SyncTC(user, start, end);
                        SyncGoogle(user, start, end);
                    }
                    DataDatabase.SaveChanges();

                    lock (SyncInfoGiver.SyncInfoGiverLocker)
                        SyncInfoGiver.WasLastSyncSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                lock (SyncInfoGiver.SyncInfoGiverLocker)
                    SyncInfoGiver.ErrorMessage = ex.Message;
            }
            finally
            {
                lock (SyncInfoGiver.SyncInfoGiverLocker)
                    SyncInfoGiver.IsProccessingSync = false;
            }
        }

        /// <summary>
        /// sync method for timer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void Sync(Object source, ElapsedEventArgs e)
        {
            Sync();
        }

        /// <summary>
        /// Synchronisation Time Cockpit Timesheets with Google calendat
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void SyncTC(User user, DateTime start, DateTime end)
        {
            lock (SyncInfoGiver.SyncInfoGiverLocker)
                SyncInfoGiver.SyncInfo = "Checking Time Cockpit Events with Google calendar";

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
            foreach (var modifiedEvent in modifiedEvents)
            {
                gBrooker.EditEvent(modifiedEvent);
                user.ChangeEvent(modifiedEvent);
            }

        }

        /// <summary>
        /// Synchronisation Google Events with Time Cockpit
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void SyncGoogle(User user, DateTime start, DateTime end)
        {
            lock (SyncInfoGiver.SyncInfoGiverLocker)
                SyncInfoGiver.SyncInfo = "Checking Google events with Time Cockpit calendar";

            var gBrooker = new GBrooker(user);
            var events = gBrooker.GetEvents(start, end);
            var newEvents = GetNewGoogleEvents(user, events);
            //var modifiedEvents = GetModifiedGoogleEvents(user, events);
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

        /// <summary>
        /// Get Time Cokpit Events that was not in database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ActualEvents">Actual events from time Cokpit</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Google Events that was not in database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ActualEvents">actual events from google calendars</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Time Cockpit Events that was in database
        /// but some parameter has changed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ActualEvents"></param>
        /// <returns></returns>
        private static IEnumerable<Event> GetModifiedTCEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToTCId;
            var modifiedEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (oldEvents.ContainsKey(actualEvent.TCId)
                    && !oldEvents[actualEvent.TCId].Equals(actualEvent))
                {
                    modifiedEvents.Add(actualEvent.WithGoogleID(oldEvents[actualEvent.TCId].GoogleId));
                }
            }
            return modifiedEvents;
        }

        /// <summary>
        /// Get Google Events that was in database
        /// but some parameter has changed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ActualEvents"></param>
        /// <returns></returns>
        private static IEnumerable<Event> GetModifiedGoogleEvents(User user, IEnumerable<Event> ActualEvents)
        {
            var oldEvents = user.EventsAccordingToGoogleId;
            var modifiedEvents = new List<Event>();
            foreach (var actualEvent in ActualEvents)
            {
                if (oldEvents.ContainsKey(actualEvent.GoogleId)
                    && !oldEvents[actualEvent.GoogleId].Equals(actualEvent))
                {
                    modifiedEvents.Add(actualEvent.WithTCID(oldEvents[actualEvent.GoogleId].TCId));
                }
            }
            return modifiedEvents;
        }
        #endregion
    }

    internal static class EventExtension
    {
        /// <summary>
        /// create new Event with GoogleID as parameter
        /// </summary>
        /// <param name="event1"></param>
        /// <param name="GoogleID"></param>
        /// <returns></returns>
        internal static Event WithGoogleID(this Event event1, string GoogleID)
            => new Event()
            {
                GoogleId = GoogleID,
                TCId = event1.TCId,
                Description = event1.Description,
                Start = event1.Start,
                End = event1.End,
                Customer = event1.Customer
            };

        /// <summary>
        /// Create new Evet with TCId as parameter
        /// </summary>
        /// <param name="event1"></param>
        /// <param name="TCID"></param>
        /// <returns></returns>
        internal static Event WithTCID(this Event event1, string TCID)
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
