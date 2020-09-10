using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{

    public class Event
    {
        public string GoogleId;
        public string TCId;
        public DateTime? Start;
        public DateTime? End;
        public string Description;
        public string Customer;
        public Event(string data)
        {
            char[] separator = new char[1] { ParameterSeparator };
            var splited = data.Split(separator);
            GoogleId = splited[0];
            TCId = splited[1];
            Start = new DateTime(Int64.Parse(splited[2]));
            End = new DateTime(Int64.Parse(splited[3]));
            Description = splited[4];
            Customer = splited[5];

        }

        public Event() { }

        public static readonly char ParameterSeparator = '}';
        public string ToStore()
        {
            try
            {
                StringBuilder sB = new StringBuilder();
                sB.Append(GoogleId);
                sB.Append(ParameterSeparator);
                sB.Append(TCId);
                sB.Append(ParameterSeparator);
                sB.Append(Start.Value.Ticks);
                sB.Append(ParameterSeparator);
                sB.Append(End.Value.Ticks);
                sB.Append(ParameterSeparator);
                string replacedDescription = Description.Replace(ParameterSeparator, ')');
                replacedDescription = Description.Replace(User.ParameterSeparator, '(');
                replacedDescription = Description.Replace(User.EventSeparator, '/');
                sB.Append(replacedDescription);
                sB.Append(ParameterSeparator);
                sB.Append(Customer);
                return sB.ToString();
            }
            catch (NullReferenceException)
            {
                throw new ArgumentException("Event has not all parameters");
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Event))
                return false;
            else
                return GoogleId == ((Event)obj).GoogleId
                    && TCId == ((Event)obj).TCId
                    && Start == ((Event)obj).Start
                    && End == ((Event)obj).End
                    && Description == ((Event)obj).Description
                    && Customer == ((Event)obj).Customer;
        }
    }
}
