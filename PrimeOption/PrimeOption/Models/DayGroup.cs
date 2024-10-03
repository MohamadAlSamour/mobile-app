using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PrimeOption.Models
{
    public class DayGroup : ObservableCollection<Events>
    {
        public DateTime Date { get; private set; }

        public DayGroup(DateTime date, IEnumerable<Events> events) : base(events)
        {
            Date = date;
        }
    }
}

