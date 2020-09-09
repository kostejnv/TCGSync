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
                NewUser.Fullname = TCUtils.GetFullname(NewUser);
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
            {
                NewUser.googleCalendarId = GUtil.CreateNewCalendar(NewUser, Form.NewCalendarBox.Text);
                WaSSetSetting = true;
            }
            else
            {
                if (Form.CalendarsBox.SelectedItem != null)
                {
                    NewUser.googleCalendarId = ((CalendarInfo)(Form.CalendarsBox.SelectedItem)).ID;
                    WaSSetSetting = true;
                }
                else
                    WaSSetSetting = false;
            }
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
                string.Format("Do you want to really delete user {0}?", user.ToString()),
                "Delete User",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                UserDatabase.userDatabase.Remove(user);
                GUtil.RemoveGoogleToken(user);
                UserDatabase.RefreshListBox();
                UserDatabase.SaveChanges();
            }
        }
    }

    public class UserChanger : IDisposable
    {
        private User OldUser;
        public User ChangingUser;
        private EditUserForm Form;
        bool WaSSetSetting = false;
        bool WasGLogin = false;
        private readonly string tempDir = "temp";
        public UserChanger(User user, EditUserForm form)
        {
            ChangingUser = (User)user.Clone();
            OldUser = user;
            Form = form;
        }
        public void LoadCalendars()
        {
            var calendars = GUtil.GetCalendars(ChangingUser);
            foreach (var calendar in calendars)
            {
                Form.CalendarsBox.Items.Add(calendar);
            }
            var actualCalendar = calendars.Where(c => c.ID == ChangingUser.googleCalendarId);
            if ((actualCalendar.ToList().Count) == 1)
            {
                Form.CalendarsBox.SelectedItem = actualCalendar.First();
            }           
        }
        public string GetGoogleEmail()
        {
            string[] files = Directory.GetFiles(tempDir);
            var tokenNames = files.Where(f => f.Contains(ChangingUser.TCUsername));
            if (tokenNames.ToList().Count != 0)
            {
                return GUtil.GetEmail(ChangingUser, tempDir);
            }
            else
            {
                return GUtil.GetEmail(ChangingUser);
            }
        }

        public bool GoogleLogin()
        {
            RemoveTemp();
            if (ChangingUser.TCUsername == null) throw new InvalidOperationException("This operation is without UserName not supported");
            GUtil.GLogin(ChangingUser, "temp");
            WasGLogin = true;
            var calendars = GUtil.GetCalendars(ChangingUser, tempDir);
            Form.CalendarsBox.Items.Clear();
            foreach (var calendar in calendars)
            {
                Form.CalendarsBox.Items.Add(calendar);
            }
            Form.CalendarsBox.SelectedIndex = -1;
            Form.CalendarsBox.Text = "(select)";
            return true;
        }
        public void SetSetting(int pastSyncInterval, bool isFutureSpecified, int futureSyncInterval)
        {
            ChangingUser.PastSyncInterval = pastSyncInterval;
            ChangingUser.IsFutureSpecified = isFutureSpecified;
            ChangingUser.FutureSyncInterval = futureSyncInterval;
            if (Form.NewCalendarBox.Text != "")
            {
                ChangingUser.googleCalendarId = GUtil.CreateNewCalendar(ChangingUser, Form.NewCalendarBox.Text);
                WaSSetSetting = true;
            }
            else
            {
                if (Form.CalendarsBox.SelectedItem != null)
                {
                    ChangingUser.googleCalendarId = ((CalendarInfo)(Form.CalendarsBox.SelectedItem)).ID;
                    WaSSetSetting = true;
                }
                else
                    WaSSetSetting = false;
            }
        }
        public void ChangeUserInDatabse()
        {
            if (!WaSSetSetting) throw new InvalidOperationException("Setting was not filled");
            lock (UserDatabase.userDatabase)
            {
                ChangingUser.EventsAccordingToGoogleId = OldUser.EventsAccordingToGoogleId;
                ChangingUser.EventsAccordingToTCId = OldUser.EventsAccordingToTCId;
                ChangingUser.Events = OldUser.Events;
                ChangeGoogleToken();
                UserDatabase.userDatabase.Remove(OldUser);
                UserDatabase.AddUserToUserDatabase(ChangingUser);
            }
            
        }
        public void ChangeGoogleToken()
        {
            if (!WasGLogin) return;
            string tokenDir = GUtil.TokenDirectory;
            GUtil.RemoveGoogleToken(OldUser);
            try
            {
                string[] files = Directory.GetFiles(tempDir);
                var tokenNames = files.Where(f => f.Contains(ChangingUser.TCUsername));
                if (tokenNames.ToList().Count != 0)
                {
                    string tokenNameFullPath = files.Where(f => f.Contains(ChangingUser.TCUsername)).First();
                    string tokenName = tokenNameFullPath.Substring(tempDir.Length + 1);
                    File.Copy(tokenNameFullPath, Path.Combine(tokenDir, tokenName));
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(string.Format("Token does not add, because '{0}', please try login to Google again", e.Message), "Error", MessageBoxButtons.OK);
                Form.GoogleButton.BackColor = System.Drawing.Color.OrangeRed;
                throw new InvalidOperationException();
            }
        }
        private void RemoveTemp()
        {
            try
            {
                string[] files = Directory.GetFiles(tempDir);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(string.Format("Temporaly files was not deleted, because '{0}'", e.Message), "Error", MessageBoxButtons.OK);
            }
        }

        public void Dispose()
        {
            OldUser = null;
            ChangingUser = null;
            RemoveTemp();
        }
    }
}
