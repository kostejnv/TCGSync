using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeCockpitCommunication;
using GoogleCalendarCommunication;
using TCGSync.Entities;
using TCGSync.UI;
using TCGSync;

namespace TCGSync
{
    static class Run
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            UserDatabase.InitializeDatabase(mainForm, mainForm.SyncIntervalBox);
            SyncInfoGiver.Initialization(mainForm);
            Synchronization.RunAutoSync();
            Synchronization.SyncNow();
            Application.Run(mainForm);
            UserDatabase.SaveChanges();
        }

         
    }
}
