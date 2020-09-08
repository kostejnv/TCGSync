using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCGSync.Entities;

namespace GoogleCalendarCommunication
{
    /// <summary>
    /// static class with google calendar utilities 
    /// </summary>
    public static class GUtil
    {
        /// <summary>
        /// Google Calendar permissions for this application
        /// </summary>
        static readonly string[] Scopes = GBrooker.Scopes;
        public static readonly string TokenDirectory = "google_token";
        /// <summary>
        /// Application name for Google Uses
        /// </summary>
        private static readonly string ApplicationName = "TCGSync";
        /// <summary>
        /// Log in Google Calendar
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GLogin(User user)
        {
            try
            {
                GetCredentials(user);
                return true;
            }
            catch (System.AggregateException)
            {
                return false;
            }

            
        }

        public static UserCredential GetCredentials(User user)
        {
            UserCredential credential;
            using (var stream =
                new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {

                // This method try to find user's token, if failed create new one to credPath folder after log in Google
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    user.TCUsername,
                    CancellationToken.None,
                    new FileDataStore(TokenDirectory, true)).Result;
            }
            return credential;
        }
        public static CalendarService GetCalendarService(UserCredential credential)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        public static void RemoveGoogleToken(User user)
        {
            string[] files = Directory.GetFiles(TokenDirectory);
            var tokenNames = files.Where(f => f.Contains(user.TCUsername));
            if (tokenNames.ToList().Count != 0)
            {
                string tokenName = files.Where(f => f.Contains(user.TCUsername)).First();
                File.Delete(tokenName);
            }            
        }
        public static List<CalendarInfo> GetCalendars(User user)
        {
            var service = GetCalendarService(GetCredentials(user));
            CalendarList calendars = service.CalendarList.List().Execute();
            var calendarsList = new List<CalendarInfo>();
            foreach (var calendar in calendars.Items)
            {
                calendarsList.Add(new CalendarInfo(calendar));
            }
            return calendarsList;
        }
        public static string CreateNewCalendar(User user, string Name)
        {
            Calendar calendar = new Calendar();
            calendar.Summary = Name;
            var service = GetCalendarService(GetCredentials(user));
            Calendar createdCalendar = service.Calendars.Insert(calendar).Execute();
            return createdCalendar.Id;
        }

    }

    public struct CalendarInfo
    {
        public string Name;
        public string ID;
        public CalendarInfo(CalendarListEntry calendar)
        {
            Name = calendar.Summary;
            ID = calendar.Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
