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

        /// <summary>
        /// Log in Google Calendar
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GLogin(User user)
        {
            try
            {
                UserCredential credential;
                using (var stream =
                    new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
                {
                    // Name of folder that contains authentification tokens
                    string credPath = "google_token";

                    // This method try to find user's token, if failed create new one to credPath folder after log in Google
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        user.TCUsername,
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }
                return true;
            }
            catch (System.AggregateException)
            {
                return false;
            }
        }

    }
}
