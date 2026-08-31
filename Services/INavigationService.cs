using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.Services
{
    public interface INavigationService
    {
        void GoTo(string route);

        void GoTo(string route, object parameter);

        void GoBack();

        void ShowRootPage();
    }
}
