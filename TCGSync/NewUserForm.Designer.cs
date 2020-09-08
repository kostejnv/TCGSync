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
            ((System.ComponentModel.ISupportInitialize)(this.StartDomain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(27, 261);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(133, 13);
            this.EndTimeLabel.TabIndex = 19;
            this.EndTimeLabel.Text = "Synchronize Days in future";
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Location = new System.Drawing.Point(27, 228);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(126, 13);
            this.StartLabel.TabIndex = 18;
            this.StartLabel.Text = "Synchronize Days in past";
            // 
            // EndSpecifiedCheckBox
            // 
            this.EndSpecifiedCheckBox.AutoSize = true;
            this.EndSpecifiedCheckBox.Location = new System.Drawing.Point(246, 256);
            this.EndSpecifiedCheckBox.Name = "EndSpecifiedCheckBox";
            this.EndSpecifiedCheckBox.Size = new System.Drawing.Size(88, 17);
            this.EndSpecifiedCheckBox.TabIndex = 16;
            this.EndSpecifiedCheckBox.Text = "Not specified";
            this.EndSpecifiedCheckBox.UseVisualStyleBackColor = true;
            this.EndSpecifiedCheckBox.CheckedChanged += new System.EventHandler(this.EndSpecifiedCheckBox_CheckedChanged);
            // 
            // GoogleButton
            // 
            this.GoogleButton.Enabled = false;
            this.GoogleButton.Location = new System.Drawing.Point(343, 79);
            this.GoogleButton.Name = "GoogleButton";
            this.GoogleButton.Size = new System.Drawing.Size(84, 37);
            this.GoogleButton.TabIndex = 20;
            this.GoogleButton.Text = "Log in";
            this.GoogleButton.UseVisualStyleBackColor = true;
            this.GoogleButton.Click += new System.EventHandler(this.GoogleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Log in your Google Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Other setting:";
            // 
            // CreateNewUserButton
            // 
            this.CreateNewUserButton.Enabled = false;
            this.CreateNewUserButton.Location = new System.Drawing.Point(456, 313);
            this.CreateNewUserButton.Name = "CreateNewUserButton";
            this.CreateNewUserButton.Size = new System.Drawing.Size(75, 23);
            this.CreateNewUserButton.TabIndex = 23;
            this.CreateNewUserButton.Text = "Create User";
            this.CreateNewUserButton.UseVisualStyleBackColor = true;
            this.CreateNewUserButton.Click += new System.EventHandler(this.CreateNewUserButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(365, 313);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 24;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // VerifyButton
            // 
            this.VerifyButton.Location = new System.Drawing.Point(145, 114);
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.Size = new System.Drawing.Size(75, 23);
            this.VerifyButton.TabIndex = 30;
            this.VerifyButton.Text = "Verify";
            this.VerifyButton.UseVisualStyleBackColor = true;
            this.VerifyButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // TCPasswordTextBox
            // 
            this.TCPasswordTextBox.Location = new System.Drawing.Point(97, 88);
            this.TCPasswordTextBox.Name = "TCPasswordTextBox";
            this.TCPasswordTextBox.PasswordChar = '•';
            this.TCPasswordTextBox.Size = new System.Drawing.Size(123, 20);
            this.TCPasswordTextBox.TabIndex = 29;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(30, 91);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 28;
            this.PasswordLabel.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Fill your Time Cockpit credentials";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(30, 65);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.UsernameLabel.TabIndex = 26;
            this.UsernameLabel.Text = "Username:";
            // 
            // TCUserNameTextBox
            // 
            this.TCUserNameTextBox.Location = new System.Drawing.Point(97, 62);
            this.TCUserNameTextBox.Name = "TCUserNameTextBox";
            this.TCUserNameTextBox.Size = new System.Drawing.Size(123, 20);
            this.TCUserNameTextBox.TabIndex = 25;
            // 
            // EnableLoginLabel
            // 
            this.EnableLoginLabel.AutoSize = true;
            this.EnableLoginLabel.Location = new System.Drawing.Point(273, 46);
            this.EnableLoginLabel.Name = "EnableLoginLabel";
            this.EnableLoginLabel.Size = new System.Drawing.Size(231, 13);
            this.EnableLoginLabel.TabIndex = 31;
            this.EnableLoginLabel.Text = "(Enable after verifying Time Cockpit credentials)";
            // 
            // StartDomain
            // 
            this.StartDomain.Location = new System.Drawing.Point(165, 227);
            this.StartDomain.Name = "StartDomain";
            this.StartDomain.Size = new System.Drawing.Size(61, 20);
            this.StartDomain.TabIndex = 32;
            this.StartDomain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // EndDomain
            // 
            this.EndDomain.Location = new System.Drawing.Point(165, 254);
            this.EndDomain.Name = "EndDomain";
            this.EndDomain.Size = new System.Drawing.Size(61, 20);
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
            this.WaitLabel.Location = new System.Drawing.Point(67, 119);
            this.WaitLabel.Name = "WaitLabel";
            this.WaitLabel.Size = new System.Drawing.Size(70, 13);
            this.WaitLabel.TabIndex = 34;
            this.WaitLabel.Text = "Please wait...";
            this.WaitLabel.Visible = false;
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 348);
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
            this.Name = "NewUserForm";
            this.Text = "NewUserForm";
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
    }
}