using Speiseplan.Model;
using Speiseplan.Services;
using Speiseplan.Views;
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

        public ICommand EditImageCommand { get; private set; }

        

        public EditMealViewModel(IMenuService menuService, INavigationService navigationService)
        {
            Meal = new Meal();

            _menuService = menuService;
            _navigationService = navigationService;

            SaveCommand = new Command(SaveMeal);

            EditImageCommand = new Command(EditImage);
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
            Meal mealToSave = Meal;
            mealToSave.Name = Name;

            await _menuService.UpdateMealAsync(mealToSave);

            _navigationService.GoBack();
        }

        private async void EditImage(object obj)
        {

            var viewModel = new ImageGalleryViewModel();
            var popupView = new ImageGalleryPopup(viewModel);

            string? selectedUrl = await _navigationService.ShowModalAsync(popupView, true);

        }



    }
}
