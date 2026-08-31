using Speiseplan.Services;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class CreateMenuPageViewModel : BaseViewModel
    {
        #region private fields

        private readonly IMenuService _menuService;

        private readonly INavigationService _navigationService;

        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddDays(6);
        private string _name;

        #endregion

        public bool IsLoading 
        {
            get;
            private set; 
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        
        public ICommand CreateMenuCommand
        {
            get;
            private set;
        }


        public CreateMenuPageViewModel(IMenuService menuService, INavigationService navigationService)
        {
            IsLoading = false;

            _menuService = menuService;
            _navigationService = navigationService;

            
            CreateMenuCommand = new Command(execute: async () => await CreateNewMenu());
        }

        private async Task CreateNewMenu()
        {
            IsLoading = true;
            
            await _menuService.CreateMenu(Name, StartDate, EndDate);

            IsLoading = false;
            _navigationService.GoBack();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
