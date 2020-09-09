using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCGSync;
using TCGSync.UserModifications;

namespace TCGSync.UI
{
    public partial class NewUserForm : Form
    {
        UserCreator userCreator;
        public NewUserForm()
        {
            InitializeComponent();
            userCreator = new UserCreator(this);
        }

        private void VerifyButton_Click(object sender, EventArgs e)
        {
            WaitLabel.Visible = true;
            if (userCreator.TCVerify(TCUserNameTextBox.Text, TCPasswordTextBox.Text))
            {
                TCUserNameTextBox.BackColor = Color.LightGreen;
                TCPasswordTextBox.BackColor = Color.LightGreen;
                GoogleButton.Enabled = true;
            }
            else
            {
                TCUserNameTextBox.BackColor = Color.OrangeRed;
                TCPasswordTextBox.BackColor = Color.OrangeRed;
            }
            WaitLabel.Visible = false;
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
            if (userCreator.GoogleLogin())
            {
                GoogleButton.Invoke(new Action(() => GoogleButton.BackColor = Color.LightGreen));
                CreateNewUserButton.Invoke(new Action(() => CreateNewUserButton.Enabled = true));
                CalendarsBox.Invoke(new Action(() => CalendarsBox.Enabled = true));
                NewCalendarBox.Invoke(new Action(() => NewCalendarBox.Enabled = true));
            }
            else
            {
                GoogleButton.Invoke(new Action(() => GoogleButton.BackColor = Color.OrangeRed));
            }
        }

        private void CreateNewUserButton_Click(object sender, EventArgs e)
        {
            userCreator.SetSetting((int)StartDomain.Value, !EndSpecifiedCheckBox.Checked, (int)EndDomain.Value);
            try
            {
                UserDatabase.AddUserToUserDatabase(userCreator.GetUser());
                KillGLoginThreads();
                this.Dispose();
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            KillGLoginThreads();
            Dispose();
        }

        private void NewCalendarBox_TextChanged(object sender, EventArgs e)
        {
            if (NewCalendarBox.Text != "") CalendarsBox.Enabled = false;
            else CalendarsBox.Enabled = true;
        }
    }
}
