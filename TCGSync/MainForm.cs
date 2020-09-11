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
            if (Run.FirstTimeRun)
            {
                IconTray.Visible = false;
            }
            else
            {
                IconTray.Visible = true;
                WindowState = FormWindowState.Minimized;
            }
            SyncInfoGiver.SendMessage();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void SynchronizeButton_Click(object sender, EventArgs e)
        {
            Synchronization.SyncNow();
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
            if (UserListBox.SelectedItem == null) return;
            User user = (User)UserListBox.SelectedItem;
            var deleter = new UserModifications.UserDeleter(user);
            deleter.DeleteUser();
        }

        private void synchronizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Synchronization.SyncNow();
        }

        private void newUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateButton_Click(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                WindowState = FormWindowState.Minimized;
                IconTray.Visible = true;
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                Dispose();
            }
        }

        public void EnableChangeAndDeleteButton()
        {
            ChangeUserButton.Enabled = false;
            DeleteButton.Enabled = false;
        }

        private void ChangeUserButton_Click(object sender, EventArgs e)
        {
            if (UserListBox.SelectedItem == null) return;
            User user = (User)UserListBox.SelectedItem;
            EditUserForm editUserForm = new EditUserForm(user);
            editUserForm.Show();
        }
        public void ShowMessage(string text, bool important = false)
        {
            if (!IsDisposed && Created)
            {
                this.Invoke(new Action(() => MessageLabel.Text = text));

                string iconMessage = string.Format("TCGSync - {0}", text);
                if (iconMessage.Length < 63)
                    this.Invoke(new Action(() => IconTray.Text = string.Format("TCGSync - {0}", text)));
                else
                    this.Invoke(new Action(() => IconTray.Text = string.Format("TCGSync - {0}", text).Substring(0, 60) + "..."));
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void stopSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSync_Click(sender, e);
        }

        private void StopSync_Click(object sender, EventArgs e)
        {
            if (StopSync.Text == "Stop Sync")
            {
                Synchronization.StopAutoSync();
                StopSync.Text = "Continue";
                stopSyncToolStripMenuItem.Text = "Continue";
                stopSyncToolStripMenuItem1.Text = "Continue";

            }
            else
            {
                Synchronization.ContinueAutoSync();
                StopSync.Text = "Stop Sync";
                stopSyncToolStripMenuItem.Text = "Stop Sync";
                stopSyncToolStripMenuItem1.Text = "Stop Sync";
            }
        }

        private void stopSyncToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StopSync_Click(sender, e);
        }

        private void IconTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            IconTray.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "TCGSync", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

        }

        private void IconTray_MouseMove(object sender, MouseEventArgs e)
        {
            SyncInfoGiver.SendMessage();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TCGSync_uzivatelska_prirucka.pdf");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TCGSync_uzivatelska_prirucka.pdf");
        }
    }
}
