using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    /// <summary>
    /// Class with all data about user
    /// </summary>
    public class User : ICloneable
    {
        #region Data Field
        public string Username;

        /// <summary>
        /// Id of Calendar which is synchronized
        /// </summary>
        public string googleCalendarId = null;
        public string Fullname { private get; set; }
        public string GoogleEmail { private get; set; } = "";

        // All synchronized event
        public List<Event> Events = new List<Event>();
        public Dictionary<string, Event> EventsAccordingToGoogleId = new Dictionary<string, Event>();
        public Dictionary<string, Event> EventsAccordingToTCId = new Dictionary<string, Event>();

        #endregion

        #region Setting
        public static readonly int MaxPastSyncInterval = 100;

        public int PastSyncInterval;

        public bool IsFutureSpecified = true;

        private int? _futureSyncInterval;
        public int? FutureSyncInterval
        {
            get { return _futureSyncInterval; }
            set
            {
                if (IsFutureSpecified) _futureSyncInterval = value;
                else _futureSyncInterval = null;
            }
        }
        #endregion

        public User() { }

        /// <summary>
        /// Get the same user without reference
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            User user = new User();
            user.Username = Username;
            user.googleCalendarId = googleCalendarId;
            user.Fullname = Fullname;
            user.GoogleEmail = GoogleEmail;
            user.PastSyncInterval = PastSyncInterval;
            user.IsFutureSpecified = IsFutureSpecified;
            user.FutureSyncInterval = FutureSyncInterval;
            user.Events = Events;
            user.EventsAccordingToGoogleId = EventsAccordingToGoogleId;
            user.EventsAccordingToTCId = EventsAccordingToTCId;
            return user;
        }

        #region Storing data
        /// <summary>
        /// Separator for storing user
        /// </summary>
        public static readonly char ParameterSeparator = '{';

        /// <summary>
        /// Separator for storing between Events
        /// </summary>
        public static readonly char EventSeparator = '|';

        /// <summary>
        /// Constructor for storing data
        /// </summary>
        /// <param name="data">Line</param>
        public User(string data)
        {
            char[] separator = new char[1] { ParameterSeparator };
            var dataArray = data.Split(separator);
            Username = dataArray[0];
            googleCalendarId = dataArray[1];
            PastSyncInterval = Int32.Parse(dataArray[2]);
            if (dataArray[3] == "false")
            {
                IsFutureSpecified = false;
                _futureSyncInterval = null;
            }
            else
            {
                FutureSyncInterval = Int32.Parse(dataArray[3]);
            }
            Fullname = dataArray[4];
            GoogleEmail = dataArray[5];
            separator[0] = EventSeparator;
            var eventArray = dataArray[6].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var strEvent in eventArray)
            {
                var event1 = new Event(strEvent);

                // Adding only event that are newer than MaxPastSyncInterval
                if (event1.Start > DateTime.Now - TimeSpan.FromDays(User.MaxPastSyncInterval))
                    AddEvent(event1);
            }
        }

        /// <summary>
        /// Method stores data about user
        /// </summary>
        /// <returns></returns>
        public string ToStore()
        {
            StringBuilder data = new StringBuilder();
            data.Append(Username);
            data.Append(ParameterSeparator);
            data.Append(googleCalendarId);
            data.Append(ParameterSeparator);
            data.Append(PastSyncInterval);
            data.Append(ParameterSeparator);
            if (IsFutureSpecified) data.Append(_futureSyncInterval);
            else data.Append("false");
            data.Append(ParameterSeparator);
            data.Append(Fullname);
            data.Append(ParameterSeparator);
            data.Append(GoogleEmail);
            data.Append(ParameterSeparator);
            foreach (var event1 in Events)
            {
                data.Append(event1.ToStore());
                data.Append(EventSeparator);
            }
            return data.ToString();
        }
        #endregion

        /// <summary>
        /// Add Event to all data field
        /// </summary>
        /// <param name="event1"></param>
        public void AddEvent(Event event1)
        {
            Events.Add(event1);
            EventsAccordingToGoogleId[event1.GoogleId] = event1;
            EventsAccordingToTCId[event1.TCId] = event1;
        }

        /// <summary>
        /// Change event in all data field
        /// </summary>
        /// <param name="event1"></param>
        public void ChangeEvent(Event event1)
        {
            var oldEvent = EventsAccordingToGoogleId[event1.GoogleId];
            EventsAccordingToGoogleId[event1.GoogleId] = event1;
            EventsAccordingToTCId[event1.TCId] = event1;
            Events.Remove(oldEvent);
            Events.Add(event1);
        }

        /// <summary>
        /// override with Fullname and google email address
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (GoogleEmail != "") return string.Format("{0}  ({1})", Fullname, GoogleEmail);
            else return string.Format("{0}", Fullname);
        }
    }
}
