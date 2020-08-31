using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGSync.Entities;
using TCGSync.UI;
using System.Windows.Forms;

namespace TCGSync
{
    public static class UserDatabase
    {
        private static List<User> userDatabase = new List<User>();
        public static ListBox listBox { private get; set; }
        public static void InitializeDatabase(ListBox viewToDatabase)
        {
            listBox = viewToDatabase;
        }

        private static void RefreshListBox()
        {
            listBox.Items.Clear();
            foreach (var user in userDatabase)
            {
                listBox.Items.Add(user);
            }
        }
        public static void AddUserToUserDatabase(User user)
        {
            userDatabase.Add(user);
            if (listBox != null) RefreshListBox();
        }
    }
}
