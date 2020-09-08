﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    public class User
    {
        public string TCUsername;
        public string TCPassword;
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
            data.Append(BasicEncription.Encode(TCPassword));
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
                data.Append(event1.ToStore());
                data.Append(",");
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


        public override string ToString() => TCUsername;

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
                if (encodeChar == ';' || encodeChar == ',' || encodeChar == '|') encodeChar++;
                return new string(new char[3] { encodeChar, GetRandomCharacter(), GetRandomCharacter() });

            }
            private static char GetRandomCharacter()
            {
                lock (random)
                {
                    int randomChar = random.Next(33, 126);
                    if (randomChar == ';' || randomChar == ',' || randomChar == '|') randomChar++;
                    return (char)randomChar;
                }
            }
            public static string Decode(string encodedText)
            {
                if (encodedText.Length % 3 != 0) throw new ArgumentException("It do not decode");
                string decodedText = "";
                for (int i = 0; i < encodedText.Length/3; i++)
                {
                    decodedText += DecodeChar(encodedText.Substring(i * 3, 3));
                }
                return decodedText;
            }
            private static char DecodeChar(string character)
            {
                char decodedChar = character[0];
                if (decodedChar - 1 == ';' || decodedChar - 1 == ',' || decodedChar - 1 == '|') decodedChar--; 
                return (char)(decodedChar - Shift);

            } 
        }
    }
}
