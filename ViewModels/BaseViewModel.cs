using Speiseplan.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Speiseplan.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public string TAG => this.GetType().ToString();

        public event PropertyChangedEventHandler? PropertyChanged;



        public BaseViewModel() 
        {
            Logger.Info(TAG + " Contructor called");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Logger.Info("PropertyChanged: " + propertyName);
        }

        public abstract void Dispose();  
    }
}
