using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{

    public abstract class EventAbstract
    {
        public string ID;
        public DateTime? Start;
        public DateTime? End;
        public string Description;
    }
}
