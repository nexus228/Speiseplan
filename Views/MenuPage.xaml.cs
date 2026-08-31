using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class MenuPage : ContentPage
{

    public MenuPage(MenuPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        NavigatedTo += OnNavigatedTo;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void OnNavigatedTo(object sender, NavigatedToEventArgs args)
    {
        MenuPageViewModel viewModel = (MenuPageViewModel)BindingContext;
        
    }

}