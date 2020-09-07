using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    public class User
    {
        public string TCUsername;
        private string _tcPassword;
        public string TCPassword
        {
            protected get { return _tcPassword; }
            set { _tcPassword = value; }
        }
        public string googleCalendarId = null;

        // What events was last synchronizated
        List<Event> Events = new List<Event>();
        public Dictionary<string, Event> EventsAccordingToGoogleId = new Dictionary<string, Event>();
        public Dictionary<string, Event> EventsAccordingToTCId = new Dictionary<string, Event>();


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

        public User() { }
        public User(string data)
        {
            char[] separator = new char[1] { ';' };
            var dataArray = data.Split(separator);
            TCUsername = dataArray[0];
            TCPassword = dataArray[1];
            googleCalendarId = dataArray[2];
            PastSyncInterval = Int32.Parse(dataArray[3]);
            if (dataArray[4]=="false")
            {
                IsFutureSpecified = true;
                _futureSyncInterval = null;
            }
            else
            {
                FutureSyncInterval = Int32.Parse(dataArray[4]);
            }
            separator[0] = ',';
            var eventArray = dataArray[5].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var strEvent in eventArray)
            {
                var event1 = new Event(strEvent);
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
            data.Append(TCUsername);
            data.Append(";");
            data.Append(TCPassword);
            data.Append(";");
            data.Append(googleCalendarId);
            data.Append(";");
            data.Append(PastSyncInterval);
            data.Append(";");
            if (IsFutureSpecified) data.Append(_futureSyncInterval);
            else data.Append("false");
            data.Append(";");
            foreach (var event1 in Events)
            {
                data.Append(event1.ToString());
                data.Append(",");
            }
            return data.ToString();
        }
        public override string ToString() => TCUsername;

        public void AddEvent(Event event1)
        {
            Events.Add(event1);
            EventsAccordingToGoogleId[event1.GoogleId] = event1;
            EventsAccordingToTCId[event1.TCId] = event1;
        }
    }
}
