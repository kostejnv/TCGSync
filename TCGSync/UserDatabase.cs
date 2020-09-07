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
        private static List<User> userDatabase = new List<User>();
        public static ListBox listBox { private get; set; }
        private static object FileLocker = new object();
        public static void InitializeDatabase(ListBox viewToDatabase)
        {
            listBox = viewToDatabase;
            LoadDatabase();
            RefreshListBox();
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
            userDatabase.Add(user);
            RefreshListBox();
            SaveChanges();
        }

        public static void SaveChanges()
        {
            lock (FileLocker)
            {
                using (StreamWriter sw = new StreamWriter("data"))
                {
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
