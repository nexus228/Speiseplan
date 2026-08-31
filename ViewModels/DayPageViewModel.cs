using Speiseplan.Model;
using Speiseplan.Services;
using Speiseplan.Utils;
using Speiseplan.Views;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class DayPageViewModel : BaseViewModel, IQueryAttributable
    {

        #region private fields

        private INavigationService _navigationService;

        private IList<Day> _dayItems;

        #endregion

        #region public properties 

        public Menu? Menu { get; private set; }

        public IList<Day>? DayItems
        {
            get => _dayItems;
            private set
            {
                if (value != null && value != _dayItems)
                {
                    _dayItems = value;
                    OnPropertyChanged(nameof(DayItems));
                }             
            }
        }

        public ICommand ItemSelectedCommand { get; }

        #endregion

        public DayPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _dayItems = new List<Day>();

            ItemSelectedCommand = new Command<Day>(ItemSelected);
        }

       
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                Menu = (Menu)query["PARAMETER"];
                OnPropertyChanged("Menu");

                if (Menu != null)
                {
                    DayItems = Menu.Days;
                }
               
            }
            
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        #region private functions
        private void ItemSelected(Day day)
        {
            if (day == null)
                return;

            Logger.Info($"Ausgewählt: {day.Name}");
           
            _navigationService.GoTo(nameof(MealPage), day);
        }

        #endregion
    }
}
