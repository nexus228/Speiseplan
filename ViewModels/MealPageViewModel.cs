using Speiseplan.Model;
using Speiseplan.Services;
using Speiseplan.Services.CustomEventArgs;
using Speiseplan.Services.Enum;
using Speiseplan.Utils;
using Speiseplan.Views;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class MealPageViewModel : BaseViewModel, IQueryAttributable
    {
        private IList<Meal> _mealItems;

        private readonly IMenuService _menuService;

        private INavigationService _navigationService;

        public IList<Meal> MealItems
        {
            get => _mealItems;
            private set
            {
                _mealItems = value;
                OnPropertyChanged(nameof(MealItems));
            }
        }

        public Day? Day { get; private set; }

        public ICommand EditItemCommand { get; }

        public MealPageViewModel(IMenuService menuService, INavigationService navigationService)
        {
            _menuService = menuService;
            _navigationService = navigationService;

            _mealItems = new List<Meal>();


            _menuService.MenusChanged += OnMenusChanged;

            EditItemCommand = new Command<Meal>(EditItemDesired);
        }

        private void OnMenusChanged(object? sender, MenusChangedEventArgs args)
        {
            if (args.ChangeType == MenuChangeType.Updated)
            {
                var updatedMenu = args.AffectedMenu;
                if (updatedMenu != null && Day != null && updatedMenu.ID == Day.MenuId)
                {
                    Day = updatedMenu.Days.FirstOrDefault(d => d.Id == Day.Id);
                    MealItems = Day.Meal;
                }
            }
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
            _menuService.MenusChanged -= OnMenusChanged;
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
