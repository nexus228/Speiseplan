using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
            
        }

        public void GoTo(string route)
        {
            Shell.Current.GoToAsync(route);
        }

        public void GoBack()
        {
            Shell.Current.GoToAsync("..");
        }

        public void GoTo(string route, object parameter)
        {
            var navParams = new ShellNavigationQueryParameters { { "PARAMETER", parameter } };
            Shell.Current.GoToAsync(route, navParams);
        }
        
        public void ShowRootPage()
        {
            Application.Current!.Windows[0].Page = new AppShell();
        }
    }
}
