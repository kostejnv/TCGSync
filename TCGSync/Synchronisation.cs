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
            lock (DataDatabase.IntervalInMinutesLocker)
                SyncTimer = new System.Timers.Timer((double)(DataDatabase.IntervalInMinutes * 60 * 1000));
            SyncTimer.Elapsed += Sync;
            SyncTimer.AutoReset = true;
            SyncTimer.Enabled = true;
            SyncInfoGiver.RunTimerForUser();
        }
        public static void StopAutoSync()
        {
            SyncTimer.Stop();
            SyncInfoGiver.StopTimerForUser();
        }
        public static void ContinueAutoSync()
        {
            SyncTimer.Enabled = true;
            SyncInfoGiver.ContinueTimerForUser();
        }
        public static void SyncNow()
        {
            Thread t1 = new Thread(() => Sync());
            t1.Start();
        }

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
        private static void Sync(Object source, ElapsedEventArgs e)
        {
            Sync();
        }


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

        private static void SyncGoogle(User user, DateTime start, DateTime end)
        {
            lock (SyncInfoGiver.SyncInfoGiverLocker)
                SyncInfoGiver.SyncInfo = "Checking Google events with Time Cockpit calendar";
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
                    modifiedEvents.Add(actualEvent.WithGoogleID(oldEvents[actualEvent.TCId].GoogleId));
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
                    modifiedEvents.Add(actualEvent.WithTCID(oldEvents[actualEvent.GoogleId].TCId));
                }
            }
            return modifiedEvents;
        }
    }

}
