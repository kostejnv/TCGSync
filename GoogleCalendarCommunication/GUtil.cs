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
using System.Windows.Forms;
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

        /// <summary>
        /// Directory name with google token
        /// </summary>
        public static readonly string TokenDirectory = "google_token";

        /// <summary>
        /// Application name for Google Uses
        /// </summary>
        private static readonly string ApplicationName = "TCGSync";

        /// <summary>
        /// Log in Google Calendar
        /// </summary>
        /// <param name="user"></param>
        /// <param name="path">directory where should be store token</param>
        /// <returns></returns>
        public static bool GLogin(User user, string path)
        {
            try
            {
                GetCredentials(user, path);
                return true;
            } 
            // if user refuse log in
            catch (System.AggregateException)
            {
                return false;
            }
        }

        public static bool GLogin(User user) => GLogin(user, TokenDirectory);

        /// <summary>
        /// Get google credentials
        /// if user does not have token, methods log in user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="path">directory where should be store token</param>
        /// <returns></returns>
        public static UserCredential GetCredentials(User user, string path)
        {
            UserCredential credential;
            using (var stream =
                new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {

                // This method try to find user's token, if failed create new one to path folder after log in Google
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    user.Username,
                    CancellationToken.None,
                    new FileDataStore(path, true)).Result;
            }
            return credential;
        }

        public static UserCredential GetCredentials(User user) => GetCredentials(user, TokenDirectory);

        /// <summary>
        /// Get calendarService for the credentials
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public static CalendarService GetCalendarService(UserCredential credential)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        /// <summary>
        /// remove user google token from TokenDirectory
        /// </summary>
        /// <param name="user"></param>
        public static void RemoveGoogleToken(User user)
        {
            try
            {
                //try to find user token
                string[] files = Directory.GetFiles(TokenDirectory);
                var tokenNames = files.Where(f => f.Contains(user.Username));

                //if token exist delete
                if (tokenNames.ToList().Count != 0)
                {
                    string tokenName = files.Where(f => f.Contains(user.Username)).First();
                    File.Delete(tokenName);
                }
            }
            catch (IOException e)
            {
                var result = MessageBox.Show(string.Format("Token was not deleted, because '{0}'", e.Message), "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    RemoveGoogleToken(user);
                }
            }
        }

        /// <summary>
        /// get all user calendars
        /// </summary>
        /// <param name="user"></param>
        /// <param name="path">path of directory where user is token</param>
        /// <returns></returns>
        public static List<GoogleCalendarInfo> GetCalendars(User user, string path)
        {
            var service = GetCalendarService(GetCredentials(user, path));
            CalendarList calendars = service.CalendarList.List().Execute();
            var calendarsList = new List<GoogleCalendarInfo>();
            foreach (var calendar in calendars.Items)
            {
                calendarsList.Add(new GoogleCalendarInfo(calendar));
            }
            return calendarsList;
        }
        public static List<GoogleCalendarInfo> GetCalendars(User user) => GetCalendars(user, TokenDirectory);

        /// <summary>
        /// create new calendar
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Name">name of calendar</param>
        /// <returns></returns>
        public static string CreateNewCalendar(User user, string Name)
        {
            Calendar calendar = new Calendar();
            calendar.Summary = Name;
            var service = GetCalendarService(GetCredentials(user));
            Calendar createdCalendar = service.Calendars.Insert(calendar).Execute();
            return createdCalendar.Id;
        }

        /// <summary>
        /// Get name of google email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="path">path of directory where user is token</param>
        /// <returns></returns>
        public static string GetEmail(User user, string path)
        {
            var service = GetCalendarService(GetCredentials(user, path));
            var calendar = service.CalendarList.List().Execute().Items.Where(c => c.Primary.GetValueOrDefault()).FirstOrDefault();
            return calendar.Summary;
        }

        public static string GetEmail(User user) => GetEmail(user, TokenDirectory);

    }

    /// <summary>
    /// Main information about Google calendars
    /// </summary>
    public struct GoogleCalendarInfo
    {
        public string Name;

        public string ID;

        public GoogleCalendarInfo(CalendarListEntry calendar)
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
