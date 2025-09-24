using Gallery.ViewModels;
using Gallery.Models;

namespace Gallery.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(DetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}