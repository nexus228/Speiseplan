using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
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

        static Page? GetCurrentPage()
        => Shell.Current?.CurrentPage ?? Application.Current!.Windows[0].Page;

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

        public async Task ShowModalAsync(View view, bool canBeDismissedByTappingOutside)
        {
            var currentPage = GetCurrentPage();
            if (currentPage is null) return;

            await currentPage.ShowPopupAsync(view, new PopupOptions
            {
                CanBeDismissedByTappingOutsideOfPopup = canBeDismissedByTappingOutside
            });
        }   
    }
}
