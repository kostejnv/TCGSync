using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TCGSync.Entities;
using TCGSync.UI;
using System.Windows.Forms;

namespace TCGSync
{
    public static class UserDatabase
    {
        public static List<User> userDatabase = new List<User>();
        public static object IntervalInMinutesLocker = new object();
        public static decimal IntervalInMinutes = 15;
        public static MainForm Form { private get; set; }
        private static object FileDatabaseLocker = new object();
        public static void InitializeDatabase(MainForm form, NumericUpDown intervalSyncDomain)
        {
            Form = form;
            LoadDatabase();
            RefreshListBox();
            lock (UserDatabase.IntervalInMinutesLocker)
                intervalSyncDomain.Value = IntervalInMinutes;
        }

        public static void RefreshListBox()
        {
            if (Form.UserListBox == null) return;
            Form.UserListBox.Items.Clear();
            lock (userDatabase)
            {
                foreach (var user in userDatabase)
                {
                    Form.UserListBox.Items.Add(user);
                }
            }
            if (Form.UserListBox.SelectedItem == null) Form.EnableChangeAndDeleteButton();

        }
        public static void AddUserToUserDatabase(User user)
        {
            lock (userDatabase)
            {
                userDatabase.Add(user);
                RefreshListBox();
                SaveChanges();
            }
        }
        public static bool ExistsUser(string username)
        {
            lock (userDatabase)
            {
                return userDatabase.Where(u => u.TCUsername == username).Any();
            }
        }

        
        public static void SaveChanges()
        {
            lock (userDatabase)
            {
                lock (FileDatabaseLocker)
                {
                    using (StreamWriter sw = new StreamWriter("data"))
                    {
                        lock (UserDatabase.IntervalInMinutesLocker)
                            sw.WriteLine(IntervalInMinutes);

                        foreach (var user in userDatabase)
                        {
                            sw.WriteLine(user.ToStore());
                        }

                    }
                }
            }

        }

        public static void LoadDatabase()
        {
            lock (FileDatabaseLocker)
            {
                try
                {
                    if (File.Exists("data"))
                    {
                        using (var sr = new StreamReader("data"))
                        {
                            string line = null;
                            lock (UserDatabase.IntervalInMinutesLocker)
                                if ((line = sr.ReadLine()) != null) IntervalInMinutes = Decimal.Parse(line);
                            lock (userDatabase)
                            {
                                while ((line = sr.ReadLine()) != null)
                                {
                                    userDatabase.Add(new User(line));
                                }
                            }
                        }
                        RefreshListBox();
                    }
                    else
                    {
                        Run.FirstTimeRun = true;
                    }
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show(string.Format(
                        "The database failed to load because '{0}'. If you press cancel, it is possible that data will be deleted (from database, not from calendars)", ex.Message),
                        "TCGSync Error",
                        MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Retry) LoadDatabase();
                }
            }
        }
    }
}
