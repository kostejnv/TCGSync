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
    /// <summary>
    /// static class with all application data
    /// </summary>
    public static class DataDatabase
    {
        #region Data Fields
        /// <summary>
        /// List of all user that should synchronize
        /// </summary>
        public static List<User> userDatabase = new List<User>();

        /// <summary>
        /// Locker for multithreading work with IntervalInMinutes
        /// </summary>
        public static readonly object IntervalInMinutesLocker = new object();

        /// <summary>
        /// Intervall of AutoSynchronisation
        /// </summary>
        public static decimal IntervalInMinutes = 15;

        /// <summary>
        /// reference on mainform that contains UserListBox to show userDatabase
        /// </summary>
        public static MainForm Form { private get; set; }

        /// <summary>
        /// Locker for multithreading work with data storing
        /// </summary>
        private static readonly object FileDatabaseLocker = new object();
        #endregion

        /// <summary>
        /// Initialisation method for DataDatabase
        /// </summary>
        /// <param name="form"> Main window of application</param>
        /// <param name="intervalSyncDomain">Box with value of IntervalInMinutes</param>
        public static void InitializeDatabase(MainForm form, NumericUpDown intervalSyncDomain)
        {
            Form = form;
            LoadDatabase();
            RefreshListBox();
            lock (DataDatabase.IntervalInMinutesLocker)
                intervalSyncDomain.Value = IntervalInMinutes;
        }

        /// <summary>
        /// refresh box with user information in main window
        /// </summary>
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

        /// <summary>
        /// Add user to database and store new data
        /// </summary>
        /// <param name="user"></param>
        public static void AddUserToUserDatabase(User user)
        {
            lock (userDatabase)
            {
                userDatabase.Add(user);
                RefreshListBox();
                SaveChanges();
            }
        }

        /// <summary>
        /// Get true if user with the username exists in userDatabase
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool ExistsUser(string username)
        {
            lock (userDatabase)
            {
                return userDatabase.Where(u => u.Username == username).Any();
            }
        }

        /// <summary>
        /// Name of file where database is store
        /// </summary>
        private static readonly string FileName = "data";

        /// <summary>
        /// Save all data from DataDatabase to File
        /// </summary>
        public static void SaveChanges()
        {
            lock (userDatabase)
            {
                lock (FileDatabaseLocker)
                {
                    using (StreamWriter sw = new StreamWriter(FileName))
                    {
                        lock (DataDatabase.IntervalInMinutesLocker)
                            sw.WriteLine(IntervalInMinutes);

                        foreach (var user in userDatabase)
                        {
                            sw.WriteLine(user.ToStore());
                        }

                    }
                }
            }

        }

        /// <summary>
        /// Load data from database, 
        /// if file does not exist sign Run.FirstTimeRun as true
        /// </summary>
        public static void LoadDatabase()
        {
            lock (FileDatabaseLocker)
            {
                try
                {
                    if (File.Exists(FileName))
                    {
                        using (var sr = new StreamReader(FileName))
                        {
                            string line = null;
                            lock (DataDatabase.IntervalInMinutesLocker)
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

                    //if file does not exist sign Run.FirstTimeRun as true
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
