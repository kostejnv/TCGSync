﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCGSync.UserModifications;
using TCGSync.Entities;
using System.IO;

namespace TCGSync
{
    public partial class EditUserForm : Form
    {
        UserChanger userChanger;
        public EditUserForm(User user)
        {
            InitializeComponent();
            userChanger = new UserChanger(user, this);
            FillBoxes();


        }
        private void FillBoxes()
        {
            var user = userChanger.ChangingUser;
            TCUserNameTextBox.Text = user.TCUsername;
            TCPasswordTextBox.Text = user.TCPassword;
            StartDomain.Value = user.PastSyncInterval;
            if (user.IsFutureSpecified)
            {
                EndDomain.Value = user.PastSyncInterval;
            }
            else
            {
                EndDomain.Enabled = false;
                EndSpecifiedCheckBox.Checked = true;
            }
            userChanger.LoadCalendars();
            EmailLabel.Text = userChanger.GetGoogleEmail();
        }

        private void CreateNewUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                userChanger.SetSetting((int)StartDomain.Value, !EndSpecifiedCheckBox.Checked, (int)EndDomain.Value);
                userChanger.ChangeUserInDatabse();
                this.Dispose();
                KillGLoginThreads();
                userChanger.Dispose();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void EndSpecifiedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (EndSpecifiedCheckBox.CheckState)
            {
                case CheckState.Checked:
                    EndDomain.Enabled = false;
                    break;
                case CheckState.Unchecked:
                    EndDomain.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void NewCalendarBox_TextChanged(object sender, EventArgs e)
        {
            if (NewCalendarBox.Text != "") CalendarsBox.Enabled = false;
            else CalendarsBox.Enabled = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
            KillGLoginThreads();
            userChanger.Dispose();
        }

        private List<Thread> threads = new List<Thread>();
        private void KillGLoginThreads()
        {
            foreach (var t in threads)
            {
                if (t.IsAlive) t.Abort();
            }
        }
        private void GoogleButton_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(() => GoogleButton_Click());
            t1.Start();
            threads.Add(t1);
        }
        private void GoogleButton_Click()
        {
            GoogleButton.Invoke(new Action(() => GoogleButton.BackColor = Color.Transparent));
            if (userChanger.GoogleLogin())
            {
                GoogleButton.Invoke(new Action(() => GoogleButton.BackColor = Color.LightGreen));
                CalendarsBox.Invoke(new Action(() => CalendarsBox.Enabled = true));
                NewCalendarBox.Invoke(new Action(() => NewCalendarBox.Enabled = true));
            }
            else
            {
                GoogleButton.Invoke(new Action(() => GoogleButton.BackColor = Color.OrangeRed));
            }
        }
    }
}
