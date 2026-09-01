using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class EditMealPage : ContentPage
{
	public EditMealPage(EditMealViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}