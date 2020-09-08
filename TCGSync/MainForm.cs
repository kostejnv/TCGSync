using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCGSync;
using TCGSync.Entities;

namespace TCGSync.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            NewUserForm newUserForm = new NewUserForm();
            newUserForm.Show();
        }

        private void SetTimeButton_Click(object sender, EventArgs e)
        {
            UserDatabase.IntervalInMinutes = SyncIntervalBox.Value;
            Synchronization.RunAutoSync();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserDatabase.SaveChanges();
        }

        private void SynchronizeButton_Click(object sender, EventArgs e)
        {
            Synchronization.Sync();
        }

        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserListBox.SelectedItems.Count == 1)
            {
                DeleteButton.Enabled = true;
                ChangeUserButton.Enabled = true;
            }
            else
            {
                DeleteButton.Enabled = false;
                ChangeUserButton.Enabled = false;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            User user = (User)UserListBox.SelectedItem;
            var deleter = new UserModifications.UserDeleter(user);
            deleter.DeleteUser();
        }

        private void synchronizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Synchronization.Sync();
        }

        private void newUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateButton_Click(sender, e);
        }
    }
}
