using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGSync.Entities;
using TimeCockpitCommunication;
using GoogleCalendarCommunication;

namespace TCGSync.UserModifications
{
    public class UserCreator
    {
        private User NewUser;
        private bool WasTCVerify;
        private bool WasGLogin;
        private bool WaSSetSetting;

        public UserCreator()
        {
            NewUser = new User();
            WasTCVerify = false;
            WasGLogin = false;
            WaSSetSetting = false;
        }

        public bool TCVerify(string username, string password)
        {
            if (TCUtils.VerifyAccount(username, password))
            {
                NewUser.TCUsername = username;
                NewUser.TCPassword = password;
                WasTCVerify = true;
                return true;
            }
            return false;
        }
        public bool GoogleLogin()
        {
            if (NewUser.TCUsername == null) throw new InvalidOperationException("This operation is without UserName not supported");
            GUtil.GLogin(NewUser);
            WasGLogin = true;
            return true;
        }
        public void SetSetting(int pastSyncInterval, bool isFutureSpecified, int futureSyncInterval)
        {
            NewUser.PastSyncInterval = pastSyncInterval;
            NewUser.IsFutureSpecified = isFutureSpecified;
            NewUser.FutureSyncInterval = futureSyncInterval;
            //
            WaSSetSetting = true;
        }
        public User GetUser()
        {
            if (!WasTCVerify) throw new InvalidOperationException("Time Cockpit verifying was not successful");
            if (!WasGLogin) throw new InvalidOperationException("Google Login was not successful");
            if (!WaSSetSetting) throw new InvalidOperationException("Setting was not filled");
            return NewUser;
        }
    }
}
