using Speiseplan.ViewModels;
using Speiseplan.Views;

namespace Speiseplan
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateMenuPage), typeof(CreateMenuPage));
            Routing.RegisterRoute(nameof(DayPage), typeof(DayPage));
            Routing.RegisterRoute(nameof(MealPage), typeof(MealPage));
            Routing.RegisterRoute(nameof(EditMealPage), typeof(EditMealPage));
        }
    }
}
