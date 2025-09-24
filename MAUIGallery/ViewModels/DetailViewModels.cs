using System.Windows.Input;
using Gallery.Models;
using Gallery.Services.Interfaces;
using Microsoft.Maui.Controls;

namespace Gallery.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IPhotoService _photoService;
        private Photo _selectedPhoto;
        private string _photoId;

        public DetailsViewModel(IPhotoService photoService, IFavoriteService favoriteService)
        {
            _photoService = photoService;
            _favoriteService = favoriteService;
            ToggleFavoriteCommand = new Command(async () => await ToggleFavoriteAsync());
        }

        
        public Photo SelectedPhoto
        {
            get => _selectedPhoto;
            set => SetProperty(ref _selectedPhoto, value);
        }

        
        public string PhotoId
        {
            get => _photoId;
            set
            {
                if (SetProperty(ref _photoId, value))
                {
                    
                    Task.Run(LoadPhotoDataAsync);
                }
            }
        }

        public ICommand ToggleFavoriteCommand { get; }

        
        public void SetPhotoId(string photoId)
        {
            PhotoId = photoId;
        }

        private async Task LoadPhotoDataAsync()
        {
            if (string.IsNullOrEmpty(PhotoId)) return;

            try
            {
                IsBusy = true;
                
                var photos = await _photoService.GetPhotosAsync(1, 30);
                var photo = photos?.FirstOrDefault(p => p.Id == PhotoId);

                if (photo != null)
                {
                    
                    photo.IsFavorite = await _favoriteService.IsFavoriteAsync(photo.Id);
                    SelectedPhoto = photo;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Photo not found.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading photo details: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to load photo details.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ToggleFavoriteAsync()
        {
            if (SelectedPhoto == null) return;

            try
            {
                await _favoriteService.ToggleFavoriteAsync(SelectedPhoto.Id);
                
                SelectedPhoto.IsFavorite = !SelectedPhoto.IsFavorite;
                
                OnPropertyChanged(nameof(SelectedPhoto));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling favorite: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to update favorite.", "OK");
            }
        }
    }
}