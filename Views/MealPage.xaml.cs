using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class MealPage : ContentPage
{
	public MealPage(MealPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}