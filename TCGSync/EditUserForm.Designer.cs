namespace TCGSync
{
    partial class EditUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserForm));
            this.label6 = new System.Windows.Forms.Label();
            this.NewCalendarBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CalendarsBox = new System.Windows.Forms.ComboBox();
            this.EndDomain = new System.Windows.Forms.NumericUpDown();
            this.StartDomain = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.TCUserNameTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ChangeUserButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GoogleButton = new System.Windows.Forms.Button();
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.StartLabel = new System.Windows.Forms.Label();
            this.EndSpecifiedCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.EmailLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EndDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(319, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Name:";
            // 
            // NewCalendarBox
            // 
            this.NewCalendarBox.Location = new System.Drawing.Point(363, 197);
            this.NewCalendarBox.Name = "NewCalendarBox";
            this.NewCalendarBox.Size = new System.Drawing.Size(132, 20);
            this.NewCalendarBox.TabIndex = 61;
            this.NewCalendarBox.TextChanged += new System.EventHandler(this.NewCalendarBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "or create new one";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Select calendar to synchronisation";
            // 
            // CalendarsBox
            // 
            this.CalendarsBox.FormattingEnabled = true;
            this.CalendarsBox.Location = new System.Drawing.Point(322, 151);
            this.CalendarsBox.MaxDropDownItems = 50;
            this.CalendarsBox.Name = "CalendarsBox";
            this.CalendarsBox.Size = new System.Drawing.Size(174, 21);
            this.CalendarsBox.TabIndex = 58;
            this.CalendarsBox.Text = "(select)";
            // 
            // EndDomain
            // 
            this.EndDomain.Location = new System.Drawing.Point(160, 252);
            this.EndDomain.Name = "EndDomain";
            this.EndDomain.Size = new System.Drawing.Size(61, 20);
            this.EndDomain.TabIndex = 56;
            this.EndDomain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // StartDomain
            // 
            this.StartDomain.Location = new System.Drawing.Point(160, 225);
            this.StartDomain.Name = "StartDomain";
            this.StartDomain.Size = new System.Drawing.Size(61, 20);
            this.StartDomain.TabIndex = 55;
            this.StartDomain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Time Cockpit";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(25, 47);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.UsernameLabel.TabIndex = 49;
            this.UsernameLabel.Text = "Username:";
            // 
            // TCUserNameTextBox
            // 
            this.TCUserNameTextBox.Enabled = false;
            this.TCUserNameTextBox.Location = new System.Drawing.Point(92, 44);
            this.TCUserNameTextBox.Name = "TCUserNameTextBox";
            this.TCUserNameTextBox.Size = new System.Drawing.Size(123, 20);
            this.TCUserNameTextBox.TabIndex = 48;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(360, 311);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 47;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ChangeUserButton
            // 
            this.ChangeUserButton.Location = new System.Drawing.Point(451, 311);
            this.ChangeUserButton.Name = "ChangeUserButton";
            this.ChangeUserButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeUserButton.TabIndex = 46;
            this.ChangeUserButton.Text = "Change User";
            this.ChangeUserButton.UseVisualStyleBackColor = true;
            this.ChangeUserButton.Click += new System.EventHandler(this.CreateNewUserButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Other setting:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Change your Google account";
            // 
            // GoogleButton
            // 
            this.GoogleButton.Location = new System.Drawing.Point(360, 47);
            this.GoogleButton.Name = "GoogleButton";
            this.GoogleButton.Size = new System.Drawing.Size(84, 37);
            this.GoogleButton.TabIndex = 43;
            this.GoogleButton.Text = "Change";
            this.GoogleButton.UseVisualStyleBackColor = true;
            this.GoogleButton.Click += new System.EventHandler(this.GoogleButton_Click);
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(22, 259);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(133, 13);
            this.EndTimeLabel.TabIndex = 42;
            this.EndTimeLabel.Text = "Synchronize Days in future";
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Location = new System.Drawing.Point(22, 226);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(126, 13);
            this.StartLabel.TabIndex = 41;
            this.StartLabel.Text = "Synchronize Days in past";
            // 
            // EndSpecifiedCheckBox
            // 
            this.EndSpecifiedCheckBox.AutoSize = true;
            this.EndSpecifiedCheckBox.Location = new System.Drawing.Point(241, 254);
            this.EndSpecifiedCheckBox.Name = "EndSpecifiedCheckBox";
            this.EndSpecifiedCheckBox.Size = new System.Drawing.Size(88, 17);
            this.EndSpecifiedCheckBox.TabIndex = 40;
            this.EndSpecifiedCheckBox.Text = "Not specified";
            this.EndSpecifiedCheckBox.UseVisualStyleBackColor = true;
            this.EndSpecifiedCheckBox.CheckedChanged += new System.EventHandler(this.EndSpecifiedCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(232, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "It cannot be change. You must create new user";
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(326, 29);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(31, 13);
            this.EmailLabel.TabIndex = 64;
            this.EmailLabel.Text = "email";
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 360);
            this.Controls.Add(this.EmailLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.NewCalendarBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CalendarsBox);
            this.Controls.Add(this.EndDomain);
            this.Controls.Add(this.StartDomain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.TCUserNameTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ChangeUserButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GoogleButton);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.EndSpecifiedCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditUserForm";
            this.Text = "TCGSync - Edit User";
            ((System.ComponentModel.ISupportInitialize)(this.EndDomain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDomain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox NewCalendarBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox CalendarsBox;
        private System.Windows.Forms.NumericUpDown EndDomain;
        private System.Windows.Forms.NumericUpDown StartDomain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.TextBox TCUserNameTextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ChangeUserButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button GoogleButton;
        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.CheckBox EndSpecifiedCheckBox;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label EmailLabel;
    }
}