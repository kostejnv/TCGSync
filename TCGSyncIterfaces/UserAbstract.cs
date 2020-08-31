using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSyncIterfacesAndAbstract
{
    public abstract class UserAbstract
    {
        private string _tcUsername;
        public string TCUsername
        {
            get { return _tcUsername; }
            set { _tcUsername = value; }
        }
        private string _tcPassword;
        public string TCPassword
        {
            protected get { return _tcPassword; }
            set { _tcPassword = value; }
        }
        protected string TCpassword;

        // What events was last synchronizated
        public HashSet<string> LastGoggleEventsGuid = null;
        public HashSet<string> LastTCEventsGuid = null;

        /// <summary>
        /// Method to store data about user
        /// </summary>
        /// <returns></returns>
        public abstract string ToStore();
        public override string ToString() => TCUsername;
    }

    public class User : UserAbstract
    {
        public int PastSyncInterval;
        public bool IsFutureSpecified = true;
        private int? _futureSyncInterval;
        public int? FutureSyncInterval
        {
            get { return _futureSyncInterval; }
            set
            {
                if (IsFutureSpecified) _futureSyncInterval = value;
                else _futureSyncInterval = null;
            }
        }
        public User(string tCUsername, string password)
        {
            TCUsername = tCUsername;
            TCpassword = password;
            LastGoggleEventsGuid = new HashSet<string>();
            LastTCEventsGuid = new HashSet<string>();       
        }

        public User()
        { }
        /// <summary>
        /// Construct User from stored date (data created from method .ToStore())
        /// </summary>
        /// <param name="data"></param>
        public User(string data)
        {
            char[] separator = new char[1] { ';' };
            var dataArray = data.Split(separator);
            TCUsername = dataArray[0];
            TCpassword = dataArray[1];
            separator[0] = ',';
            LastGoggleEventsGuid = new HashSet<string>();
            var GEventsGuidsArr = dataArray[2].Split(separator);
            foreach (var guid in GEventsGuidsArr)
            {
                LastGoggleEventsGuid.Add(guid);
            }
            LastTCEventsGuid = new HashSet<string>();
            var TCEventsGuidsArr = dataArray[3].Split(separator);
            foreach (var guid in TCEventsGuidsArr)
            {
                LastTCEventsGuid.Add(guid);
            }
        }

        /// <summary>
        /// Method stores data about user
        /// </summary>
        /// <returns></returns>
        public override string ToStore()
        {
            StringBuilder data = new StringBuilder();
            data.Append(TCUsername);
            data.Append(";");
            data.Append(TCpassword);
            data.Append(";");
            if (LastGoggleEventsGuid == null || LastTCEventsGuid == null)
                throw new NullReferenceException();
            foreach (var guid in LastGoggleEventsGuid)
            {
                data.Append(guid.ToString());
                data.Append(",");
            }
            data.Remove(data.Length - 1, 1);
            data.Append(";");
            foreach (var guid in LastTCEventsGuid)
            {
                data.Append(guid.ToString());
                data.Append(",");
            }
            data.Remove(data.Length - 1, 1);
            data.Append(";");
            return data.ToString();
        }
    }
}
