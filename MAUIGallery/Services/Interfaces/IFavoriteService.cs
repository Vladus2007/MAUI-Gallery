namespace Gallery.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddFavoriteAsync(string photoId);
        Task<bool> RemoveFavoriteAsync(string photoId);
        Task<bool> IsFavoriteAsync(string photoId);
        Task<List<string>> GetFavoriteIdsAsync();
        Task ToggleFavoriteAsync(string photoId);
    }
}