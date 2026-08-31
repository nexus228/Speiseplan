using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class CurrentDayPage : ContentPage
{
	public CurrentDayPage(CurrentDayPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}