﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    public class User : ICloneable
    {
        public string TCUsername;
        public string TCPassword;
        public string googleCalendarId = null;
        public string Fullname { private get; set; }
        public string GoogleEmail { private get; set; } = "";

        // What events was last synchronizated
        public List<Event> Events = new List<Event>();
        public Dictionary<string, Event> EventsAccordingToGoogleId = new Dictionary<string, Event>();
        public Dictionary<string, Event> EventsAccordingToTCId = new Dictionary<string, Event>();


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

        public User() { }
        public User(string data)
        {
            char[] separator = new char[1] { ParameterSeparator };
            var dataArray = data.Split(separator);
            TCUsername = dataArray[0];
            TCPassword = BasicEncription.Decode(dataArray[1]);
            googleCalendarId = dataArray[2];
            PastSyncInterval = Int32.Parse(dataArray[3]);
            if (dataArray[4] == "false")
            {
                IsFutureSpecified = false;
                _futureSyncInterval = null;
            }
            else
            {
                FutureSyncInterval = Int32.Parse(dataArray[4]);
            }
            Fullname = dataArray[5];
            GoogleEmail = dataArray[6];
            separator[0] = EventSeparator;
            var eventArray = dataArray[7].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var strEvent in eventArray)
            {
                var event1 = new Event(strEvent);

                // Adding only "Actual" Events
                if (event1.Start > DateTime.Now - TimeSpan.FromDays(User.MaxPastSyncInterval))
                    AddEvent(event1);
            }
        }
        public object Clone()
        {
            User user = new User();
            user.TCUsername = TCUsername;
            user.TCPassword = TCPassword;
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
        public static readonly char ParameterSeparator = '{';
        public static readonly char EventSeparator = '|';
        /// <summary>
        /// Method stores data about user
        /// </summary>
        /// <returns></returns>
        public string ToStore()
        {
            StringBuilder data = new StringBuilder();
            data.Append(TCUsername);
            data.Append(ParameterSeparator);
            data.Append(BasicEncription.Encode(TCPassword));
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

        public void AddEvent(Event event1)
        {
            Events.Add(event1);
            EventsAccordingToGoogleId[event1.GoogleId] = event1;
            EventsAccordingToTCId[event1.TCId] = event1;
        }

        public void ChangeEvent(Event event1)
        {
            var oldEvent = EventsAccordingToGoogleId[event1.GoogleId];
            EventsAccordingToGoogleId[event1.GoogleId] = event1;
            EventsAccordingToTCId[event1.TCId] = event1;
            Events.Remove(oldEvent);
            Events.Add(event1);
        }


        public override string ToString()
        {
            if (GoogleEmail != "") return string.Format("{0}  ({1})", Fullname, GoogleEmail);
            else return string.Format("{0}", Fullname);
        }

        private static class BasicEncription
        {
            private static readonly int Shift = 5;
            private static Random random = new Random();
            public static string Encode(string text)
            {
                string encodedText = "";
                foreach (var character in text)
                {
                    encodedText += EncodeChar(character);
                }
                return encodedText;
            }
            private static string EncodeChar(char character)
            {
                char encodeChar = (char)(character + Shift);
                if (encodeChar == ParameterSeparator) encodeChar = (char)33;
                if (encodeChar == EventSeparator) encodeChar = (char)34;
                if (encodeChar == Event.ParameterSeparator) encodeChar = (char)35;
                
                return new string(new char[3] { encodeChar, GetRandomCharacter(), GetRandomCharacter() });

            }
            private static char GetRandomCharacter()
            {
                lock (random)
                {
                    int randomChar = random.Next(36, 126);
                    if (randomChar == ParameterSeparator || randomChar == EventSeparator || randomChar == Event.ParameterSeparator) randomChar++;
                    return (char)randomChar;
                }
            }
            public static string Decode(string encodedText)
            {
                if (encodedText.Length % 3 != 0) throw new ArgumentException("It cannot be decoded");
                string decodedText = "";
                for (int i = 0; i < encodedText.Length / 3; i++)
                {
                    decodedText += DecodeChar(encodedText.Substring(i * 3, 3));
                }
                return decodedText;
            }
            private static char DecodeChar(string character)
            {
                char decodedChar = character[0];
                if (decodedChar == 33) decodedChar = ParameterSeparator;
                if (decodedChar == 34) decodedChar = EventSeparator;
                if (decodedChar == 35) decodedChar = Event.ParameterSeparator;
                return (char)(decodedChar - Shift);

            }
        }
    }
}
