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

namespace GoogleCalendarCommunication
{
    public static class GUtil
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API.NET Quickstart";

        public static bool GLogin(string username)
        {
            try
            {
                UserCredential credential;
                using (var stream =
                    new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "google_token";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        username,
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
