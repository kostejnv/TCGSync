using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGSync.Entities
{
    /// <summary>
    /// Interface for access to a calendar database
    /// </summary>
    public interface IBrooker
    {
        /// <summary>
        /// Get events in a time interval
        /// </summary>
        /// <param name="start">Date when the interval starts</param>
        /// <param name="end">Date when the interval ends</param>
        /// <returns></returns>
        IEnumerable<Event> GetEvents(DateTime start, DateTime end);
        /// <summary>
        /// Add event to a calendar
        /// </summary>
        /// <param name="event1">Added event</param>
        /// <returns></returns>
        string CreateEvent(Event event1);
        /// <summary>
        /// Edit event in a calendar with the same ID
        /// </summary>
        /// <param name="event1">Edited event</param>
        void EditEvent(Event event1);
    }
}
