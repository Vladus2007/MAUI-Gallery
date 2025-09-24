using Gallery.ViewModels;

namespace Gallery.Views
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage(GalleryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

           
            if (BindingContext is GalleryViewModel viewModel && viewModel.Photos.Count == 0)
            {
                await viewModel.LoadFirstPageAsync();
            }
        }
    }
}