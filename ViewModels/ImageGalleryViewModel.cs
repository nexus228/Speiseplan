
using Speiseplan.Services.CustomEventArgs;
using Supabase;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Speiseplan.ViewModels
{
    public class ImageGalleryViewModel : BaseViewModel
    {

        private readonly Client _supabase;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _selectedImageUrl = string.Empty;
        public string SelectedImageUrl
        {
            get => _selectedImageUrl;
            set
            {
                _selectedImageUrl = value; 
                OnPropertyChanged(nameof(SelectedImageUrl)); 
            }
        }

        public ObservableCollection<string> ImageUrls { get; } = new();

        public event EventHandler<string>? ImageSelectionFinished;

        public ICommand ImageSelectedCommand => new Command<string>(url =>
        {
            ImageSelectionFinished?.Invoke(this, url);
        });

        public ImageGalleryViewModel()
        {
           
            _supabase = new Client(
                "https://flmibwdoetmpnywaqtvs.supabase.co",
                "sb_publishable_MBbmyPQMPFG1K-zwV6b26Q_iKJf21Nu"
            );
        }

        public void SetInitialSelection(string? currentUrl)
        {
            if (currentUrl == null)
            {
                SelectedImageUrl = string.Empty;
            }
            else 
            {
                SelectedImageUrl = currentUrl;
            }     
        }

        public async Task LoadImagesAsync()
        {
            IsLoading = true;

            var bucket = _supabase.Storage.From("meal-images");
            var files = await bucket.List("public");

            if(files == null)
            {
                IsLoading = false;
                return;
            }

            ImageUrls.Clear();
            ImageUrls.Add(string.Empty);
            foreach (var file in files.Where(f => f.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)))
            {
                string publicUrl = bucket.GetPublicUrl($"public/{file.Name}");
                ImageUrls.Add(publicUrl);

                
            }

            foreach (var stringUrl in ImageUrls)
            {
                if (SelectedImageUrl.Equals(stringUrl))
                {
                    SelectedImageUrl = stringUrl;
                    break;
                }
            }

            IsLoading = false;
        }


        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
