using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGSync.Entities;
using TimeCockpitCommunication;
using GoogleCalendarCommunication;
using System.Windows.Forms;
using TCGSync.UI;
using System.IO;

namespace TCGSync.UserModifications
{ 
    /// <summary>
    /// class that contains method for create user
    /// </summary>
    public class UserCreator
    {
        private User NewUser;

        private bool WasTCVerify;

        private bool WasGLogin;

        private bool WaSSetSetting;

        /// <summary>
        /// Form to fill information about user
        /// </summary>
        private NewUserForm Form;

        public UserCreator(NewUserForm form)
        {
            NewUser = new User();
            WasTCVerify = false;
            WasGLogin = false;
            WaSSetSetting = false;
            Form = form;
        }

        /// <summary>
        /// return true if Time Cockpit credentials are correct
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TCVerify(string username, string password)
        {
            // Check if there is a user with same username in database
            if (DataDatabase.ExistsUser(username))
            {
                MessageBox.Show
                    ("There is the same username in database! Time Cockpit username is unique parameter and therefore it cannot be use more than once.",
                    "Login failed",
                    MessageBoxButtons.OK);
                return false;
            }
            if (TCUtils.VerifyAccount(username, password))
            {
                NewUser.TCUsername = username;
                NewUser.TCPassword = password;
                NewUser.Fullname = TCUtils.GetFullname(NewUser);
                WasTCVerify = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return true if Google login was successful
        /// </summary>
        /// <returns></returns>
        public bool GoogleLogin()
        {
            if (NewUser.TCUsername == null) throw new InvalidOperationException("This operation is without UserName not supported");
            GUtil.GLogin(NewUser);
            WasGLogin = true;

            // multithreaded add calendars to CalendarsBox
            var calendars = GUtil.GetCalendars(NewUser);
            Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.Items.Clear()));
            foreach (var calendar in calendars)
            {
                Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.Items.Add(calendar)));
            }

            return true;
        }

        /// <summary>
        /// store user setting
        /// </summary>
        /// <param name="pastSyncInterval"></param>
        /// <param name="isFutureSpecified"></param>
        /// <param name="futureSyncInterval"></param>
        public void SetSetting(int pastSyncInterval, bool isFutureSpecified, int futureSyncInterval)
        {
            NewUser.PastSyncInterval = pastSyncInterval;
            NewUser.IsFutureSpecified = isFutureSpecified;
            NewUser.FutureSyncInterval = futureSyncInterval;

            // if user want to create new calendar
            if (Form.NewCalendarBox.Text != "")
            {
                NewUser.googleCalendarId = GUtil.CreateNewCalendar(NewUser, Form.NewCalendarBox.Text);
                WaSSetSetting = true;
            }
            else
            {
                // if user select his calendar
                if (Form.CalendarsBox.SelectedItem != null)
                {
                    NewUser.googleCalendarId = ((GoogleCalendarInfo)(Form.CalendarsBox.SelectedItem)).ID;
                    WaSSetSetting = true;
                }
                else
                    WaSSetSetting = false;
            }
        }

        /// <summary>
        /// check if all data are correct and get user
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            if (!WasTCVerify) throw new InvalidOperationException("Time Cockpit verifying was not successful");
            if (!WasGLogin) throw new InvalidOperationException("Google Login was not successful");
            if (!WaSSetSetting) throw new InvalidOperationException("Setting was not filled");
            
            //Check if new user with same Timecockpit credentials was added meanwhile set setting
            if (DataDatabase.ExistsUser(NewUser.TCUsername))
                throw new InvalidOperationException("There is the same username in database! Time Cockpit username is unique parameter and therefore it cannot be use more than once.");

            NewUser.GoogleEmail = GUtil.GetEmail(NewUser);
            Synchronization.SyncNow();

            return NewUser;
        }
    }

}
