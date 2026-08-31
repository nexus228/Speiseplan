using Speiseplan.Services;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {

        private readonly IMenuService _menuService;
        private readonly INavigationService _navigationService;

        private bool _isOffline;

        private bool _isLoading;

        public bool IsOffline
        {
            get
            {
                return _isOffline;
            }
            private set
            {
                if (_isOffline != value)
                {
                    _isOffline = value;
                    OnPropertyChanged(nameof(IsOffline));
                }
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public ICommand LoadDataCommand { get; private set; }



        public StartPageViewModel(IMenuService menuService, INavigationService navigationService)
        {
            _menuService = menuService;
            _navigationService = navigationService;

            LoadDataCommand = new Command(execute: async () => await InitStartPageAction());
            
        }

        public async Task InitStartPageAction()
        {
            NetworkAccess networkAccess = Connectivity.Current.NetworkAccess;

            if(networkAccess != NetworkAccess.Internet)
            {
                IsOffline = true;
                return;
            }

            IsOffline = false;
            IsLoading = true;

            await _menuService.GetAllMenusAsync();

            IsLoading = false;

            _navigationService.ShowRootPage();
        }
        

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
