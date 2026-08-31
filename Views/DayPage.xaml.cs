using Speiseplan.Model;
using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class DayPage : ContentPage
{

    public DayPage(DayPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}