using CommunityToolkit.Maui.Views;
using Speiseplan.ViewModels;

namespace Speiseplan.Views;

public partial class ImageGalleryPopup : Popup<string>
{
    private readonly ImageGalleryViewModel _viewModel;
    public ImageGalleryPopup(ImageGalleryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
       
        this.Opened += async (s, e) => await _viewModel.LoadImagesAsync();
    }

 
}