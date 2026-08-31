using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.Services
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ConfirmAsync(string title, string message,
                                       string accept,
                                       string cancel)
        {
            return await Shell.Current.DisplayAlertAsync(title, message, accept, cancel);

        }
    }
}
