using Speiseplan.Model;
using Speiseplan.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class EditMealViewModel : BaseViewModel, IQueryAttributable
    {
        #region private fields

        private readonly IMenuService _menuService;

        private readonly INavigationService _navigationService;

        private string _name;

        #endregion


        public Meal Meal { get; private set; }

        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ICommand SaveCommand { get; private set; }

        public EditMealViewModel(IMenuService menuService, INavigationService navigationService)
        {
            Meal = new Meal();

            _menuService = menuService;
            _navigationService = navigationService;

            SaveCommand = new Command(SaveMeal);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                Meal = (Meal)query["PARAMETER"];
                OnPropertyChanged("Meal");

                Name = Meal.Name;
                OnPropertyChanged("Name");
            }

        }


        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        private async void SaveMeal(object obj)
        {
            Meal menuToSave = Meal;
            menuToSave.Name = Name;

            await _menuService.UpdateMealAsync(menuToSave);

            await _menuService.GetAllMenusAsync();

            _navigationService.GoBack();
        }

        
    }
}
