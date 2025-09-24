using System.Collections.ObjectModel;
using System.Windows.Input;
using Gallery.Models;
using Gallery.Services.Interfaces;
using Gallery.Views;
namespace Gallery.ViewModels
{
    public class GalleryViewModel : BaseViewModel
    {

        // Сервисы внедряются через конструктор
        private readonly IPhotoService _photoService;
        private readonly IFavoriteService _favoriteService;

        // Текущая страница для пагинации
        private int _currentPage = 1;
        // Флаг, предотвращающий одновременную загрузку нескольких страниц
        private bool _isLoadingMore;

        public ObservableCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();
        public ICommand LoadPhotosCommand { get; }
        public ICommand LoadMorePhotosCommand { get; }
        public ICommand PhotoTappedCommand { get; }

        public GalleryViewModel(IPhotoService photoService, IFavoriteService favoriteService)
        {
            _photoService = photoService;
            _favoriteService = favoriteService;
            ToggleFavoriteCommand = new Command<Photo>(async (photo) => await OnToggleFavorite(photo));
            // Инициализация команд
            LoadPhotosCommand = new Command(async () => await LoadFirstPageAsync());
            LoadMorePhotosCommand = new Command(async () => await LoadNextPageAsync());
            // PhotoTappedCommand будет реализован позже, для навигации
            PhotoTappedCommand = new Command<Photo>(async (photo) => await OnPhotoTapped(photo));
        }

        // Загрузка первой страницы (например, при открытии приложения)
        public async Task LoadFirstPageAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                _currentPage = 1;
                Photos.Clear();

                await LoadPhotosData(_currentPage);
            }
            catch (Exception ex)
            {
                // TODO: Показать пользователю ошибку (используй Toast, Snackbar или Alert)
                Console.WriteLine($"Unable to load photos: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Загрузка следующей страницы (при прокрутке)
        private async Task LoadNextPageAsync()
        {
            if (_isLoadingMore) return;

            try
            {
                _isLoadingMore = true;
                _currentPage++;

                await LoadPhotosData(_currentPage);
            }
            catch (Exception ex)
            {
                _currentPage--; // Откатываем страницу при ошибке
                Console.WriteLine($"Unable to load more photos: {ex.Message}");
            }
            finally
            {
                _isLoadingMore = false;
            }
        }

        // Основная логика загрузки и объединения данных
        private async Task LoadPhotosData(int page)
        {
            var newPhotos = await _photoService.GetPhotosAsync(page);
            if (newPhotos?.Any() != true) return; // Если фото нет, выходим

            // Получаем актуальный список избранного
            var favoriteIds = await _favoriteService.GetFavoriteIdsAsync();

            // Обновляем флаги избранного для загруженных фото
            foreach (var photo in newPhotos)
            {
                photo.IsFavorite = favoriteIds.Contains(photo.Id);
            }

            // Добавляем фото в коллекцию (уже на основном потоке UI)
            foreach (var photo in newPhotos)
            {
                Photos.Add(photo);
            }
        }

        // В ViewModels/GalleryViewModel.cs
        private async Task OnPhotoTapped(Photo photo)
        {
            if (photo == null) return;

            // Используем Shell Navigation для перехода
            // Передаем параметр через QueryString
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.PhotoId)}={photo.Id}");
        }
        // В ViewModels/GalleryViewModel.cs
        public ICommand ToggleFavoriteCommand { get; }

        // В конструкторе:
        

// Реализация метода-обработчика
private async Task OnToggleFavorite(Photo photo)
        {
            if (photo == null) return;

            try
            {
                await _favoriteService.ToggleFavoriteAsync(photo.Id);
                // Обновляем состояние именно этого фото в коллекции
                photo.IsFavorite = !photo.IsFavorite;
                // Уведомляем UI об изменении свойства IsFavorite у этого объекта
                // Важно: Чтобы сработало обновление, Photo должен реализововать INotifyPropertyChanged!
                OnPropertyChanged(nameof(photo.IsFavorite));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling favorite: {ex.Message}");
            }
        }
    }
}