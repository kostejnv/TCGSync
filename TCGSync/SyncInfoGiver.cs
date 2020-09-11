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
    /// Class that cares for synchronisation message
    /// </summary>
    public static class SyncInfoGiver
    {
        /// <summary>
        /// Form where should message display
        /// </summary>
        private static MainForm MessageCannal;

        /// <summary>
        /// timer that shows in what time will be necx sync
        /// </summary>
        private static System.Timers.Timer SyncMinutesTimer;

        /// <summary>
        /// Locker for all this data field
        /// </summary>
        public static readonly object SyncInfoGiverLocker = new object();

        #region Data Field
        private static string _syncInfo;
        public static string SyncInfo
        {
            private get => _syncInfo;
            set
            {
                _syncInfo = value;
                SendMessage();
            }
        }

        public static int _minutesToNextSync;
        public static int MinutesToNextSync
        {
            private get => _minutesToNextSync;
            set
            {
                _minutesToNextSync = value;
                SendMessage();
            }
        }

        private static bool _isProccessingSync = false;
        public static bool IsProccessingSync
        {
            private get => _isProccessingSync;
            set
            {
                _isProccessingSync = value;
                SendMessage();
            }
        }

        private static bool _wasSyncStop = false;
        public static bool WasSyncStop
        {
            private get => _wasSyncStop;
            set
            {
                _wasSyncStop = value;
                SendMessage();
            }
        }

        private static bool _wasLastSyncSuccessful = true;
        public static bool WasLastSyncSuccessful
        {
            private get => _wasLastSyncSuccessful;
            set
            {
                _wasLastSyncSuccessful = value;
                SendMessage();
            }
        }

        public static string _errorMessage;
        public static string ErrorMessage
        {
            private get => _errorMessage;
            set
            {
                _errorMessage = value;
                WasLastSyncSuccessful = false;
            }
        }
        #endregion

        public static void Initialization(MainForm messageCannal)
        {
            MessageCannal = messageCannal;
        }

        /// <summary>
        /// start TimerForUser
        /// </summary>
        public static void RunTimerForUser()
        {
            if (SyncMinutesTimer != null && SyncMinutesTimer.Enabled)
            {
                SyncMinutesTimer.Stop();
                SyncMinutesTimer.Dispose();
            }
            lock (SyncInfoGiverLocker)
            {


                lock (DataDatabase.IntervalInMinutesLocker)
                {
                    SyncMinutesTimer = new System.Timers.Timer(60 * 1000);
                    SyncMinutesTimer.Elapsed += SyncTimer;
                    SyncMinutesTimer.AutoReset = true;
                    MinutesToNextSync = (int)DataDatabase.IntervalInMinutes;
                    SyncMinutesTimer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Stop TimerForUser
        /// </summary>
        public static void StopTimerForUser()
        {
            SyncMinutesTimer.Stop();
            WasSyncStop = true;
        }

        /// <summary>
        /// start TimerForUser after its stop
        /// </summary>
        public static void ContinueTimerForUser()
        {
            SyncMinutesTimer.Enabled = true;
            WasSyncStop = false;
        }

        /// <summary>
        /// Send message to form about sync information
        /// </summary>
        public static void SendMessage()
        {
            bool importantMessage = false;
            string message = "";
            lock (SyncInfoGiverLocker)
            {


                if (IsProccessingSync)
                {
                    message += SyncInfo;
                }
                else
                {
                    if (!WasSyncStop)
                    {
                        message += string.Format("Next sync in {0} minutes ", MinutesToNextSync);
                        if (WasLastSyncSuccessful)
                        {
                            message += string.Format("(last sync was successful)");
                        }
                        else
                        {
                            message += string.Format("(last sync failed because '{0}')", ErrorMessage);
                            importantMessage = true;
                        }
                    }
                    else
                    {
                        message += "Synchronisation was stopped";
                    }
                }
            }
            MessageCannal.ShowMessage(message, importantMessage);
        }

        #region Private Methods

        /// <summary>
        /// Method For TimerForUser
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void SyncTimer(Object source, ElapsedEventArgs e)
        {
            if (MinutesToNextSync > 0)
            {
                MinutesToNextSync--;
            }
            else
            {
                lock (DataDatabase.IntervalInMinutesLocker)
                    MinutesToNextSync = (int)DataDatabase.IntervalInMinutes;
            }
        }
        #endregion
    }
}
