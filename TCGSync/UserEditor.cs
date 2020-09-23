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
    /// Class with method to edit user
    /// </summary>
    public class UserEditor : IDisposable
    {
        #region Data Field
        /// <summary>
        /// User before change
        /// </summary>
        private User OldUser;

        /// <summary>
        /// Editing user
        /// </summary>
        public User ChangingUser;

        /// <summary>
        /// EditUserForm for editted user
        /// </summary>
        private EditUserForm Form;

        bool WaSSetSetting = false;

        bool WasGLogin = false;

        /// <summary>
        /// Directory name with temporaly google_token
        /// </summary>
        private static readonly string tempDir = "temp";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="form"></param>
        public UserEditor(User user, EditUserForm form)
        {
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);
            lock (DataDatabase.userDatabase)
            {
                ChangingUser = (User)user.Clone();
                OldUser = user;
            }
            Form = form;
        }

        public void Dispose()
        {
            OldUser = null;
            ChangingUser = null;
            RemoveTemp();
        }

        /// <summary>
        /// Load calendars to CalendarsBox
        /// </summary>
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

        /// <summary>
        /// Get google mail for the google mail
        /// </summary>
        /// <returns></returns>
        public string GetGoogleEmail()
        {
            string[] files = Directory.GetFiles(tempDir);
            var tokenNames = files.Where(f => f.Contains(ChangingUser.TCUsername));

            // if google acount was editted
            if (tokenNames.ToList().Count != 0)
            {
                return GUtil.GetEmail(ChangingUser, tempDir);
            }
            //if not
            else
            {
                return GUtil.GetEmail(ChangingUser);
            }
        }

        /// <summary>
        /// multithreading method to login google account
        /// </summary>
        /// <returns>true if successful</returns>
        public bool GoogleLogin()
        {
            RemoveTemp();
            if (ChangingUser.TCUsername == null) throw new InvalidOperationException("This operation is without UserName not supported");

            //login
            GUtil.GLogin(ChangingUser, "temp");
            WasGLogin = true;

            //multithreading change of CalendarsBox
            var calendars = GUtil.GetCalendars(ChangingUser, tempDir);
            Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.Items.Clear()));
            foreach (var calendar in calendars)
            {
                Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.Items.Add(calendar)));
            }
            Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.SelectedIndex = -1));
            Form.CalendarsBox.Invoke(new Action(() => Form.CalendarsBox.Text = "(select)"));
            Form.EmailLabel.Invoke(new Action(() => Form.EmailLabel.Text = GetGoogleEmail()));

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
            ChangingUser.PastSyncInterval = pastSyncInterval;
            ChangingUser.IsFutureSpecified = isFutureSpecified;
            ChangingUser.FutureSyncInterval = futureSyncInterval;

            // if user want to create new calendar
            if (Form.NewCalendarBox.Text != "")
            {
                ChangingUser.googleCalendarId = GUtil.CreateNewCalendar(ChangingUser, Form.NewCalendarBox.Text);
                WaSSetSetting = true;
            }
            else
            {
                // if user select his calendar
                if (Form.CalendarsBox.SelectedItem != null)
                {
                    ChangingUser.googleCalendarId = ((GoogleCalendarInfo)(Form.CalendarsBox.SelectedItem)).ID;
                    WaSSetSetting = true;
                }
                else
                    WaSSetSetting = false;
            }
        }

        /// <summary>
        /// Change user in DataDatabase.userDatabase (multithreading)
        /// </summary>
        public void ChangeUserInDatabse()
        {
            if (!WaSSetSetting) throw new InvalidOperationException("Setting was not filled");

            lock (DataDatabase.userDatabase)
            {
                //if new google account do sync again
                if (WasGLogin)
                {
                    ChangingUser.EventsAccordingToGoogleId = new Dictionary<string, Event>();
                    ChangingUser.EventsAccordingToTCId = new Dictionary<string, Event>();
                    ChangingUser.Events = new List<Event>();
                }
                else
                {
                    ChangingUser.EventsAccordingToGoogleId = OldUser.EventsAccordingToGoogleId;
                    ChangingUser.EventsAccordingToTCId = OldUser.EventsAccordingToTCId;
                    ChangingUser.Events = OldUser.Events;
                }
                ChangeGoogleToken();
                DataDatabase.userDatabase.Remove(OldUser);
                ChangingUser.GoogleEmail = GetGoogleEmail();
                DataDatabase.AddUserToUserDatabase(ChangingUser);
            }

            if (WasGLogin) Synchronization.SyncNow();
        }

        #region Private Methods

        /// <summary>
        /// Get user google token and replace it with temporally
        /// </summary>
        private void ChangeGoogleToken()
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

        /// <summary>
        /// Delete date in dictionary with temporally token
        /// </summary>
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

        #endregion
    }
}
