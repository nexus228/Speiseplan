using Speiseplan.Model;
using Speiseplan.Services;


namespace Speiseplan.ViewModels
{
    public class CurrentDayPageViewModel : BaseViewModel
    {
        private IList<Meal> _mealItems;

        private readonly IMenuService _menuService;
        private bool _isLoading;


        public IList<Meal> MealItems
        {
            get => _mealItems;
            private set
            {
                _mealItems = value;
                OnPropertyChanged(nameof(MealItems));
            }

        }

        public CurrentDayPageViewModel(IMenuService menuService)
        {
            _menuService = menuService;
            _mealItems = new List<Meal>();

            MealItems = _findMealListForCurrentDate(DateOnly.FromDateTime(DateTime.Now));
        }

        public override void Dispose()
        {
        }

        private IList<Meal> _findMealListForCurrentDate(DateOnly currentDate)
        {
            IList<Meal> returnValue = new List<Meal>();
            IList<Menu> menuList = _menuService.MenuList.ToList();
            for (int i = 0; i < menuList.Count; i++)
            {
                Menu menu = menuList[i];
                if (menu != null)
                {
                    ;
                    DateOnly startDate = DateOnly.FromDateTime(menu.StartDate);
                    DateOnly endDate = DateOnly.FromDateTime(menu.EndDate);

                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        IList<Day> days = menu.Days;

                        for (int j = 0; j < days.Count; j++)
                        {
                            Day day = days[j];
                            DateOnly dayDate = DateOnly.FromDateTime(day.Date);
                            if (dayDate == currentDate)
                            {
                                returnValue = day.Meal;
                                break;
                            }
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
