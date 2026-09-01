using Speiseplan.Model;
using Speiseplan.Services;
using Speiseplan.Utils;
using Speiseplan.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class MealPageViewModel : BaseViewModel, IQueryAttributable
    {
        private IList<Meal> _mealItems;

        private INavigationService _navigationService;

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

        public ICommand EditItemCommand { get; }

        public MealPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _mealItems = new List<Meal>();

            EditItemCommand = new Command<Meal>(EditItemDesired);
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


        #region private functions
        private void EditItemDesired(Meal meal)
        {
            if (meal == null)
                return;

            Logger.Info($"Ausgewählt: {meal.Name}");

            _navigationService.GoTo(nameof(EditMealPage), meal);
        }

        #endregion
    }
}
