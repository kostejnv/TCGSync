namespace TCGSync.UI
{
    partial class NewUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUserForm));
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.StartLabel = new System.Windows.Forms.Label();
            this.EndSpecifiedCheckBox = new System.Windows.Forms.CheckBox();
            this.GoogleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CreateNewUserButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.VerifyButton = new System.Windows.Forms.Button();
            this.TCPasswordTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.TCUserNameTextBox = new System.Windows.Forms.TextBox();
            this.EnableLoginLabel = new System.Windows.Forms.Label();
            this.StartDomain = new System.Windows.Forms.NumericUpDown();
            this.EndDomain = new System.Windows.Forms.NumericUpDown();
            this.WaitLabel = new System.Windows.Forms.Label();
            this.CalendarsBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NewCalendarBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StartDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(36, 321);
            this.EndTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(164, 16);
            this.EndTimeLabel.TabIndex = 19;
            this.EndTimeLabel.Text = "Synchronize Days in future";
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Location = new System.Drawing.Point(36, 281);
            this.StartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(158, 16);
            this.StartLabel.TabIndex = 18;
            this.StartLabel.Text = "Synchronize Days in past";
            // 
            // EndSpecifiedCheckBox
            // 
            this.EndSpecifiedCheckBox.AutoSize = true;
            this.EndSpecifiedCheckBox.Location = new System.Drawing.Point(328, 315);
            this.EndSpecifiedCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndSpecifiedCheckBox.Name = "EndSpecifiedCheckBox";
            this.EndSpecifiedCheckBox.Size = new System.Drawing.Size(106, 20);
            this.EndSpecifiedCheckBox.TabIndex = 16;
            this.EndSpecifiedCheckBox.Text = "Not specified";
            this.EndSpecifiedCheckBox.UseVisualStyleBackColor = true;
            this.EndSpecifiedCheckBox.CheckedChanged += new System.EventHandler(this.EndSpecifiedCheckBox_CheckedChanged);
            // 
            // GoogleButton
            // 
            this.GoogleButton.Enabled = false;
            this.GoogleButton.Location = new System.Drawing.Point(457, 97);
            this.GoogleButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GoogleButton.Name = "GoogleButton";
            this.GoogleButton.Size = new System.Drawing.Size(112, 46);
            this.GoogleButton.TabIndex = 20;
            this.GoogleButton.Text = "Log in";
            this.GoogleButton.UseVisualStyleBackColor = true;
            this.GoogleButton.Click += new System.EventHandler(this.GoogleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Log in your Google Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Other setting:";
            // 
            // CreateNewUserButton
            // 
            this.CreateNewUserButton.Enabled = false;
            this.CreateNewUserButton.Location = new System.Drawing.Point(608, 385);
            this.CreateNewUserButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CreateNewUserButton.Name = "CreateNewUserButton";
            this.CreateNewUserButton.Size = new System.Drawing.Size(100, 28);
            this.CreateNewUserButton.TabIndex = 23;
            this.CreateNewUserButton.Text = "Create User";
            this.CreateNewUserButton.UseVisualStyleBackColor = true;
            this.CreateNewUserButton.Click += new System.EventHandler(this.CreateNewUserButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(487, 385);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 28);
            this.CancelButton.TabIndex = 24;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // VerifyButton
            // 
            this.VerifyButton.Location = new System.Drawing.Point(193, 140);
            this.VerifyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.Size = new System.Drawing.Size(100, 28);
            this.VerifyButton.TabIndex = 30;
            this.VerifyButton.Text = "Verify";
            this.VerifyButton.UseVisualStyleBackColor = true;
            this.VerifyButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // TCPasswordTextBox
            // 
            this.TCPasswordTextBox.Location = new System.Drawing.Point(129, 108);
            this.TCPasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TCPasswordTextBox.Name = "TCPasswordTextBox";
            this.TCPasswordTextBox.PasswordChar = '•';
            this.TCPasswordTextBox.Size = new System.Drawing.Size(163, 22);
            this.TCPasswordTextBox.TabIndex = 29;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(40, 112);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(71, 16);
            this.PasswordLabel.TabIndex = 28;
            this.PasswordLabel.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Fill your Time Cockpit credentials";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(40, 80);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(74, 16);
            this.UsernameLabel.TabIndex = 26;
            this.UsernameLabel.Text = "Username:";
            // 
            // TCUserNameTextBox
            // 
            this.TCUserNameTextBox.Location = new System.Drawing.Point(129, 76);
            this.TCUserNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TCUserNameTextBox.Name = "TCUserNameTextBox";
            this.TCUserNameTextBox.Size = new System.Drawing.Size(163, 22);
            this.TCUserNameTextBox.TabIndex = 25;
            // 
            // EnableLoginLabel
            // 
            this.EnableLoginLabel.AutoSize = true;
            this.EnableLoginLabel.Location = new System.Drawing.Point(364, 57);
            this.EnableLoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnableLoginLabel.Name = "EnableLoginLabel";
            this.EnableLoginLabel.Size = new System.Drawing.Size(292, 16);
            this.EnableLoginLabel.TabIndex = 31;
            this.EnableLoginLabel.Text = "(Enable after verifying Time Cockpit credentials)";
            // 
            // StartDomain
            // 
            this.StartDomain.Location = new System.Drawing.Point(220, 279);
            this.StartDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartDomain.Name = "StartDomain";
            this.StartDomain.Size = new System.Drawing.Size(81, 22);
            this.StartDomain.TabIndex = 32;
            this.StartDomain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // EndDomain
            // 
            this.EndDomain.Location = new System.Drawing.Point(220, 313);
            this.EndDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndDomain.Name = "EndDomain";
            this.EndDomain.Size = new System.Drawing.Size(81, 22);
            this.EndDomain.TabIndex = 33;
            this.EndDomain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // WaitLabel
            // 
            this.WaitLabel.AutoSize = true;
            this.WaitLabel.Location = new System.Drawing.Point(89, 146);
            this.WaitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WaitLabel.Name = "WaitLabel";
            this.WaitLabel.Size = new System.Drawing.Size(86, 16);
            this.WaitLabel.TabIndex = 34;
            this.WaitLabel.Text = "Please wait...";
            this.WaitLabel.Visible = false;
            // 
            // CalendarsBox
            // 
            this.CalendarsBox.Enabled = false;
            this.CalendarsBox.FormattingEnabled = true;
            this.CalendarsBox.Location = new System.Drawing.Point(413, 187);
            this.CalendarsBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CalendarsBox.MaxDropDownItems = 50;
            this.CalendarsBox.Name = "CalendarsBox";
            this.CalendarsBox.Size = new System.Drawing.Size(231, 24);
            this.CalendarsBox.TabIndex = 35;
            this.CalendarsBox.Text = "(select)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 161);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Select calendar to synchronisation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(464, 219);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "or create new one";
            // 
            // NewCalendarBox
            // 
            this.NewCalendarBox.Enabled = false;
            this.NewCalendarBox.Location = new System.Drawing.Point(468, 244);
            this.NewCalendarBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NewCalendarBox.Name = "NewCalendarBox";
            this.NewCalendarBox.Size = new System.Drawing.Size(175, 22);
            this.NewCalendarBox.TabIndex = 38;
            this.NewCalendarBox.TextChanged += new System.EventHandler(this.NewCalendarBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(409, 247);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "Name:";
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.NewCalendarBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CalendarsBox);
            this.Controls.Add(this.WaitLabel);
            this.Controls.Add(this.EndDomain);
            this.Controls.Add(this.StartDomain);
            this.Controls.Add(this.EnableLoginLabel);
            this.Controls.Add(this.VerifyButton);
            this.Controls.Add(this.TCPasswordTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.TCUserNameTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CreateNewUserButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GoogleButton);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.EndSpecifiedCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NewUserForm";
            this.Text = "TCGSync - New User";
            ((System.ComponentModel.ISupportInitialize)(this.StartDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDomain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.CheckBox EndSpecifiedCheckBox;
        private System.Windows.Forms.Button GoogleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CreateNewUserButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button VerifyButton;
        private System.Windows.Forms.TextBox TCPasswordTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox TCUserNameTextBox;
        private System.Windows.Forms.Label EnableLoginLabel;
        private System.Windows.Forms.NumericUpDown StartDomain;
        private System.Windows.Forms.NumericUpDown EndDomain;
        private System.Windows.Forms.Label WaitLabel;
        public System.Windows.Forms.ComboBox CalendarsBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox NewCalendarBox;
        private System.Windows.Forms.Label label6;
    }
}