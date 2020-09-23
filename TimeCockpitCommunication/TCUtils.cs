using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCockpitCommunication.TCService;
using System.Net;
using TCGSync.Entities;

namespace TimeCockpitCommunication
{

    /// <summary>
    /// static class with Time Cockpit utilities
    /// </summary>
    public static class TCUtils
    {
        /// <summary>
        /// return true if credentials are correct else false
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyAccount(string username, string password)
        {
            try
            {
                var TCsvc = GetService(new NetworkCredential(username, password));

                // trivial query to check if credentials are correct
                var temp = TCsvc.APP_UserDetail.First();        
                return true;
            }
            catch (System.Data.Services.Client.DataServiceQueryException)
            {
                return false;
            }
        }

        public static DataService GetService(NetworkCredential cred)
        {
            var TCsvc = new DataService(new Uri("https://apipreview.timecockpit.com/odata", UriKind.Absolute));
            TCsvc.Credentials = cred;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return TCsvc;
        }

        /// <summary>
        /// Get Fullname of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetFullname(User user)
        {
            if (!TCCredentialsManager.Exists(user.Username)) throw new InvalidOperationException("User has not TC credentials");
            var service = GetService(TCCredentialsManager.Get(user.Username));
            return service.APP_UserDetail.Where(u => u.APP_Username == user.Username).First().APP_Fullname;
        }
    }
}
