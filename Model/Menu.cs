using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.Model
{
    public class Menu
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? CombinedDateText 
        { 
            get 
            {
                return StartDate.ToString("dd.MM.yyyy") + " – " + EndDate.ToString("dd.MM.yyyy");
            }
        }

        public IList<Day> Days { get; set; } = [];
    }
}
