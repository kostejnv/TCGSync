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
            if (userCreator.TCVerify(TCUserNameTextBox.Text,TCPasswordTextBox.Text))
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

        private void GoogleButton_Click(object sender, EventArgs e)
        {
            if (userCreator.GoogleLogin())
            {
                GoogleButton.BackColor = Color.LightGreen;
                CreateNewUserButton.Enabled = true;
                CalendarsBox.Enabled = true;
                NewCalendarBox.Enabled = true;
            }
            else
            {
                GoogleButton.BackColor = Color.OrangeRed;
            }
        }

        private void CreateNewUserButton_Click(object sender, EventArgs e)
        {
            userCreator.SetSetting((int)StartDomain.Value, !EndSpecifiedCheckBox.Checked, (int)EndDomain.Value);
            try
            {
                UserDatabase.AddUserToUserDatabase(userCreator.GetUser());
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
            Dispose();
        }

        private void NewCalendarBox_TextChanged(object sender, EventArgs e)
        {
            if (NewCalendarBox.Text != "") CalendarsBox.Enabled = false;
            else CalendarsBox.Enabled = true;
        }
    }
}
