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
        public static decimal IntervalInMinutes = 15;
        public static ListBox listBox { private get; set; }
        private static object FileLocker = new object();
        public static void InitializeDatabase(ListBox viewToDatabase, NumericUpDown intervalSyncDomain)
        {
            listBox = viewToDatabase;
            LoadDatabase();
            RefreshListBox();
            intervalSyncDomain.Value = IntervalInMinutes;
        }

        private static void RefreshListBox()
        {
            if (listBox == null) return;
            listBox.Items.Clear();
            foreach (var user in userDatabase)
            {
                listBox.Items.Add(user);
            }

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

        public static void SaveChanges()
        {
            lock (FileLocker)
            {
                using (StreamWriter sw = new StreamWriter("data"))
                {
                    sw.WriteLine(IntervalInMinutes);
                    foreach (var user in userDatabase)
                    {
                        sw.WriteLine(user.ToStore());
                    }
                }
            }

        }

        public static void LoadDatabase()
        {
            lock (FileLocker)
            {
                if (File.Exists("data"))
                {
                    using (var sr = new StreamReader("data"))
                    {
                        string line = null;
                        if ((line = sr.ReadLine()) != null) IntervalInMinutes = Decimal.Parse(line);
                        while ((line = sr.ReadLine()) != null)
                        {
                            userDatabase.Add(new User(line));
                        }
                    }
                    RefreshListBox();
                }
            }
        }
    }
}
