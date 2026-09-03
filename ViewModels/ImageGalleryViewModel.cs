
using System.Collections.ObjectModel;
using Supabase;

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

        public ObservableCollection<string> ImageUrls { get; } = new();

        public ImageGalleryViewModel()
        {
            _supabase = new Client(
                "https://flmibwdoetmpnywaqtvs.supabase.co",
                "sb_publishable_MBbmyPQMPFG1K-zwV6b26Q_iKJf21Nu"
            );
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
                ImageUrls.Add(bucket.GetPublicUrl($"public/{file.Name}"));
            }

            IsLoading = false;
        }


        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
