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

namespace TCGSync.UserModifications
{
    public class UserCreator
    {
        private User NewUser;
        private bool WasTCVerify;
        private bool WasGLogin;
        private bool WaSSetSetting;
        private NewUserForm Form;

        public UserCreator(NewUserForm form)
        {
            NewUser = new User();
            WasTCVerify = false;
            WasGLogin = false;
            WaSSetSetting = false;
            Form = form;
        }

        public bool TCVerify(string username, string password)
        {
            if (UserDatabase.ExistsUser(username))
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
            var calendars = GUtil.GetCalendars(NewUser);
            foreach (var calendar in calendars)
            {
                Form.CalendarsBox.Items.Add(calendar);
            }
            return true;
        }
        public void SetSetting(int pastSyncInterval, bool isFutureSpecified, int futureSyncInterval)
        {
            NewUser.PastSyncInterval = pastSyncInterval;
            NewUser.IsFutureSpecified = isFutureSpecified;
            NewUser.FutureSyncInterval = futureSyncInterval;
            if (Form.NewCalendarBox.Text != "")
                NewUser.googleCalendarId = GUtil.CreateNewCalendar(NewUser, Form.NewCalendarBox.Text);
            else
            {
                if (Form.CalendarsBox.SelectedItem != null)
                    NewUser.googleCalendarId = ((CalendarInfo)(Form.CalendarsBox.SelectedItem)).ID;
                else
                    WaSSetSetting = false;
            }
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

    public class UserDeleter
    {
        User user;
        public UserDeleter(User user)
        {
            this.user = user;
        }

        public void DeleteUser()
        {
            DialogResult result = MessageBox.Show(
                string.Format("Do you want to really delete user {0}?", user.TCUsername),
                "Delete User",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                UserDatabase.userDatabase.Remove(user);
                GUtil.RemoveGoogleToken(user);
                UserDatabase.SaveChanges();
            }
        }
    }
}
