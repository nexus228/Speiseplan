using Speiseplan.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.ViewModels
{
    public class MealPageViewModel : BaseViewModel, IQueryAttributable
    {
        private IList<Meal> _mealItems;


        public IList<Meal>? MealItems
        {
            get => _mealItems;
            private set
            {
                if (value != null && value != _mealItems)
                {
                    _mealItems = value;
                    OnPropertyChanged(nameof(MealItems));
                }
            }
        }

        public Day? Day { get; private set; }

        public MealPageViewModel()
        {
            _mealItems = new List<Meal>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                Day = (Day)query["PARAMETER"];
                OnPropertyChanged("Day");

                if (Day != null)
                {
                    MealItems = Day.Meal;
                }

            }

        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
