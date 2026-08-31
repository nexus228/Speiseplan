using Speiseplan.Services;
using Speiseplan.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.ViewModels
{
    public class CurrentDayPageViewModel : BaseViewModel
    {
        private readonly IMenuService _menuService;
        private bool _isLoading;

       

        public CurrentDayPageViewModel(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
