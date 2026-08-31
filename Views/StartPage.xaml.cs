using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class StartPage : ContentPage
{
	public StartPage(StartPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        _ = viewModel.InitStartPageAction();

    }
}