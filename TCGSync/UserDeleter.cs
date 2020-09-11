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
    /// Class with method to delete user
    /// </summary>
    public class UserDeleter
    {
        User user;
        public UserDeleter(User user)
        {
            this.user = user;
        }

        /// <summary>
        /// Delete user and google token from database
        /// </summary>
        public void DeleteUser()
        {
            DialogResult result = MessageBox.Show(
                string.Format("Do you want to really delete user {0}?", user.ToString()),
                "Delete User",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                lock (DataDatabase.userDatabase)
                {
                    DataDatabase.userDatabase.Remove(user);
                    GUtil.RemoveGoogleToken(user);
                    DataDatabase.RefreshListBox();
                    DataDatabase.SaveChanges();
                }
            }
        }
    }
}
