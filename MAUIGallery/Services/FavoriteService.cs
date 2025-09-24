using System.Text.Json;
using Gallery.Services.Interfaces;
using Microsoft.Maui.Storage;

namespace Gallery.Services
{
    public class FavoriteService : IFavoriteService
    {
        
        private const string FavoritesKey = "favorite_photos";
        private List<string> _favoriteIds;

        public FavoriteService()
        {
            _favoriteIds = LoadFavorites();
        }
        public async Task ToggleFavoriteAsync(string photoId)
        {
            if (await IsFavoriteAsync(photoId))
            {
                await RemoveFavoriteAsync(photoId);
            }
            else
            {
                await AddFavoriteAsync(photoId);
            }
        }
        public Task<bool> AddFavoriteAsync(string photoId)
        {
            if (!_favoriteIds.Contains(photoId))
            {
                _favoriteIds.Add(photoId);
                SaveFavorites();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> RemoveFavoriteAsync(string photoId)
        {
            var wasRemoved = _favoriteIds.Remove(photoId);
            if (wasRemoved)
            {
                SaveFavorites();
            }
            return Task.FromResult(wasRemoved);
        }

        public Task<bool> IsFavoriteAsync(string photoId)
        {
            return Task.FromResult(_favoriteIds.Contains(photoId));
        }

        public Task<List<string>> GetFavoriteIdsAsync()
        {
            return Task.FromResult(_favoriteIds);
        }

        
        private List<string> LoadFavorites()
        {
            var json = Preferences.Get(FavoritesKey, null);
            if (string.IsNullOrEmpty(json))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                
                return new List<string>();
            }
        }

        
        private void SaveFavorites()
        {
            var json = JsonSerializer.Serialize(_favoriteIds);
            Preferences.Set(FavoritesKey, json);
        }
    }
}