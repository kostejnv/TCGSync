using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meziantou.Framework.Win32;
using System.Net;

namespace TimeCockpitCommunication
{
    public static class TCCredentialsManager
    {
        private static readonly string AppName = "TCGSync - ";

        public static NetworkCredential Get(string username)
        {
            var cred = CredentialManager.ReadCredential(AppName + username);
            return new NetworkCredential(cred.UserName, cred.Password);
        }

        public static void Save(string username, string password)
        {
            CredentialManager.WriteCredential(
                applicationName: AppName + username,
                userName: username,
                secret: password,
                persistence: CredentialPersistence.LocalMachine);
        }
        public static void Delete(string username)
        {
            CredentialManager.DeleteCredential(AppName + username);
        }

        public static bool Exists(string username) => Get(username) == null;
    }
}
