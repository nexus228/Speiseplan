using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Speiseplan.Model
{
    public class Meal : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int DayId { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description { get; set; } = string.Empty;

        public string? ReceiptURL { get; set; }

        public bool HasReceipt
        {
            get 
            {
                return !String.IsNullOrEmpty(ReceiptURL);
            }
        }

        public string? ImageURL { get; set; }

        public MealIdentifier? Identifier { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
