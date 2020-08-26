using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSyncIterfacesAndAbstract
{
    public abstract class UserAbstract
    {
        public string Usernname;

        protected string TCUsername;
        protected string TCpassword;

        // What events was last synchronizated
        public HashSet<string> LastGoggleEventsGuid = null;
        public HashSet<string> LastTCEventsGuid = null;

        /// <summary>
        /// Method to store data about user
        /// </summary>
        /// <returns></returns>
        public abstract string ToStore();
        public override string ToString() => Usernname;
    }

    public class User : UserAbstract
    {
        public User(string username, string tCUsername, string password)
        {
            Usernname = username;
            TCUsername = tCUsername;
            TCpassword = password;
            LastGoggleEventsGuid = new HashSet<string>();
            LastTCEventsGuid = new HashSet<string>();       
        }
        /// <summary>
        /// Construct User from stored date (data created from method .ToStore())
        /// </summary>
        /// <param name="data"></param>
        public User(string data)
        {
            char[] separator = new char[1] { ';' };
            var dataArray = data.Split(separator);
            Usernname = dataArray[0];
            TCUsername = dataArray[1];
            TCpassword = dataArray[2];
            separator[0] = ',';
            LastGoggleEventsGuid = new HashSet<string>();
            var GEventsGuidsArr = dataArray[3].Split(separator);
            foreach (var guid in GEventsGuidsArr)
            {
                LastGoggleEventsGuid.Add(guid);
            }
            LastTCEventsGuid = new HashSet<string>();
            var TCEventsGuidsArr = dataArray[4].Split(separator);
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
            data.Append(Usernname);
            data.Append(";");
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
