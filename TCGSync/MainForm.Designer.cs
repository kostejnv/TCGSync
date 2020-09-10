namespace TCGSync.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.synchronizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.synchronizeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UserListBox = new System.Windows.Forms.ListBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SynchronizeButton = new System.Windows.Forms.Button();
            this.SyncIntervalBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.SetTimeButton = new System.Windows.Forms.Button();
            this.ChangeUserButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SyncIntervalBox)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.synchronizeToolStripMenuItem,
            this.newUserToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // synchronizeToolStripMenuItem
            // 
            this.synchronizeToolStripMenuItem.Name = "synchronizeToolStripMenuItem";
            this.synchronizeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.synchronizeToolStripMenuItem.Text = "Synchronize";
            // 
            // newUserToolStripMenuItem
            // 
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newUserToolStripMenuItem.Text = "New User";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(636, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.synchronizeToolStripMenuItem1,
            this.newUserToolStripMenuItem1,
            this.helpToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // synchronizeToolStripMenuItem1
            // 
            this.synchronizeToolStripMenuItem1.Name = "synchronizeToolStripMenuItem1";
            this.synchronizeToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.synchronizeToolStripMenuItem1.Text = "Sync Now";
            this.synchronizeToolStripMenuItem1.Click += new System.EventHandler(this.synchronizeToolStripMenuItem1_Click);
            // 
            // newUserToolStripMenuItem1
            // 
            this.newUserToolStripMenuItem1.Name = "newUserToolStripMenuItem1";
            this.newUserToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.newUserToolStripMenuItem1.Text = "New User";
            this.newUserToolStripMenuItem1.Click += new System.EventHandler(this.newUserToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessageLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(636, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // UserListBox
            // 
            this.UserListBox.FormattingEnabled = true;
            this.UserListBox.ItemHeight = 16;
            this.UserListBox.Location = new System.Drawing.Point(16, 54);
            this.UserListBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.Size = new System.Drawing.Size(460, 180);
            this.UserListBox.TabIndex = 3;
            this.UserListBox.SelectedIndexChanged += new System.EventHandler(this.UserListBox_SelectedIndexChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(503, 202);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(117, 33);
            this.DeleteButton.TabIndex = 5;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(503, 117);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(117, 33);
            this.CreateButton.TabIndex = 6;
            this.CreateButton.Text = "Create New";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // SynchronizeButton
            // 
            this.SynchronizeButton.Location = new System.Drawing.Point(515, 265);
            this.SynchronizeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SynchronizeButton.Name = "SynchronizeButton";
            this.SynchronizeButton.Size = new System.Drawing.Size(105, 65);
            this.SynchronizeButton.TabIndex = 7;
            this.SynchronizeButton.Text = "Sync Now";
            this.SynchronizeButton.UseVisualStyleBackColor = true;
            this.SynchronizeButton.Click += new System.EventHandler(this.SynchronizeButton_Click);
            // 
            // SyncIntervalBox
            // 
            this.SyncIntervalBox.Location = new System.Drawing.Point(257, 262);
            this.SyncIntervalBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SyncIntervalBox.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.SyncIntervalBox.Name = "SyncIntervalBox";
            this.SyncIntervalBox.Size = new System.Drawing.Size(60, 22);
            this.SyncIntervalBox.TabIndex = 8;
            this.SyncIntervalBox.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 265);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Synchronization interval (in minutes)";
            // 
            // SetTimeButton
            // 
            this.SetTimeButton.Location = new System.Drawing.Point(337, 261);
            this.SetTimeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetTimeButton.Name = "SetTimeButton";
            this.SetTimeButton.Size = new System.Drawing.Size(100, 28);
            this.SetTimeButton.TabIndex = 18;
            this.SetTimeButton.Text = "Set";
            this.SetTimeButton.UseVisualStyleBackColor = true;
            this.SetTimeButton.Click += new System.EventHandler(this.SetTimeButton_Click);
            // 
            // ChangeUserButton
            // 
            this.ChangeUserButton.Enabled = false;
            this.ChangeUserButton.Location = new System.Drawing.Point(503, 161);
            this.ChangeUserButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChangeUserButton.Name = "ChangeUserButton";
            this.ChangeUserButton.Size = new System.Drawing.Size(117, 33);
            this.ChangeUserButton.TabIndex = 19;
            this.ChangeUserButton.Text = "Change";
            this.ChangeUserButton.UseVisualStyleBackColor = true;
            this.ChangeUserButton.Click += new System.EventHandler(this.ChangeUserButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 372);
            this.Controls.Add(this.ChangeUserButton);
            this.Controls.Add(this.SetTimeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SyncIntervalBox);
            this.Controls.Add(this.SynchronizeButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.UserListBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SyncIntervalBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem synchronizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem synchronizeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newUserToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        public System.Windows.Forms.ListBox UserListBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button SynchronizeButton;
        public System.Windows.Forms.NumericUpDown SyncIntervalBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetTimeButton;
        private System.Windows.Forms.Button ChangeUserButton;
    }
}

