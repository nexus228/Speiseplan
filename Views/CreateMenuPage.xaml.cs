using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class CreateMenuPage : ContentPage
{
	public CreateMenuPage(CreateMenuPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}