using Speiseplan.Services;
using Speiseplan.Model;
using System.Windows.Input;
using Speiseplan.Views;

namespace Speiseplan.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        #region private fields

        private readonly IMenuService _menuService;

        private readonly INavigationService _navigationService;

        private readonly IDialogService _dialogService;

        private List<Menu>? _menuItems;

        private bool _isRefreshing;

        #endregion

        public bool IsRefreshing
        {
            get => _isRefreshing;
            private set
            {
                if (_isRefreshing == value)
                    return;

                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public List<Menu>? MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }

        public ICommand LoadMenusCommand
        {
            get;
            private set;
        }

        public ICommand CreateMenuCommand
        {
            get;
            private set;
        }

        public ICommand EditMenuCommand
        {
            get;
            private set;
        }

        public ICommand DeleteMenuCommand
        {
            get;
            private set;
        }

        public ICommand ItemSelectedCommand { get; }


        public MenuPageViewModel(IMenuService menuService, INavigationService navigationService, IDialogService dialogService)
        {
            _menuService = menuService;
            _dialogService = dialogService;


            _menuItems = menuService.MenuList;
            MenuItems = _menuService.MenuList;

            _menuService.MenuListHasChanged += OnMenuListHasChanged;

            _navigationService = navigationService;
            
            LoadMenusCommand = new Command(execute: async () => await LoadData());
            

            CreateMenuCommand = new Command(CreateNewMenu);
            ItemSelectedCommand = new Command<Menu>(ItemSelected);

            EditMenuCommand = new Command<Menu>(async (menu) =>
            {
                if (menu == null)
                    return;
                //_navigationService.GoTo(nameof(EditMenuPage), menu);

                return;
            });

            DeleteMenuCommand = new Command<Menu>(async (menu) =>
            {
                if (menu == null)
                    return;

                var confirmed = await _dialogService.ConfirmAsync("Löschen bestätigen", $"Sind Sie sicher, dass Sie {menu.Name} löschen möchten?", "Ja", "Nein");
                if (confirmed)
                {
                    bool isDeleted = await _menuService.DeleteMenuAsync(menu.ID);
                    if (isDeleted)
                        await LoadData();
                }
                if (!confirmed)
                    return;
                return;
            });
        }

        private void OnMenuListHasChanged(object? sender, EventArgs e)
        {
            MenuItems = _menuService.MenuList;
        }

        private void ItemSelected(Menu item)
        {
            if (item == null)
                return;

            Console.WriteLine($"Ausgewählt: {item.Name}");
            _navigationService.GoTo(nameof(DayPage), item);
        }

        private async void CreateNewMenu()
        {
            _navigationService.GoTo(nameof(CreateMenuPage));
        }

        private async Task LoadData()
        {
            IsRefreshing = true;

            await _menuService.GetAllMenusAsync();

            IsRefreshing = false;
        }

        public override void Dispose()
        {
            _menuItems = null;
           
        }
    }
}
