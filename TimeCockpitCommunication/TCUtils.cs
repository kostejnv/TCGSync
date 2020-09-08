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
                var TCsvc = new DataService(new Uri("https://apipreview.timecockpit.com/odata", UriKind.Absolute));
                TCsvc.Credentials = new NetworkCredential(username, password);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var temp = TCsvc.APP_UserDetail.First();        // trivial query to check if credentials are correct
                return true;
            }
            catch (System.Data.Services.Client.DataServiceQueryException)
            {
                return false;
            }
        }

        public static string GetFullname(User user)
        {
            var service = new DataService(new Uri("https://apipreview.timecockpit.com/odata", UriKind.Absolute));
            service.Credentials = new NetworkCredential(user.TCUsername, user.TCPassword);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return service.APP_UserDetail.Where(u => u.APP_Username == user.TCUsername).First().APP_Fullname;
        }
    }
}
