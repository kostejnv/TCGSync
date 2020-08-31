using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    public interface IBrooker
    {
        HashSet<EventAbstract> GetEvents(DateTime start, DateTime end);

        string CreateEvent(EventAbstract event1);

        void SetEvent(EventAbstract event1);
    }
}
