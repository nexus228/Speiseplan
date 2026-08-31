using Speiseplan.Services;
using Speiseplan.Utils;
using Speiseplan.Views;

namespace Speiseplan
{
    public partial class App : Application
    {
        private readonly StartPage _startPage;

        public App(StartPage startPage)
        {
            InitializeComponent();
            Current?.UserAppTheme = AppTheme.Light;
            _startPage = startPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_startPage);
            //return new Window(new AppShell());
        }
    }
}