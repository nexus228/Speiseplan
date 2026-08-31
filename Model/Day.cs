using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Speiseplan.Model
{
    public class Day
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string DateText 
        {
            get
            { 
                return Date.ToString("dd. MMMM yyyy");
            }
            set; 
        }
        

        public IList<Meal>? Meal { get; set; }
    }
}
