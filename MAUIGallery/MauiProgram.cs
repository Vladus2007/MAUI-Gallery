using Gallery.Services.Interfaces; 
using Gallery.Services;
using Gallery.ViewModels;
using Gallery.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Gallery
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
            // Регистрируем Сервисы
            builder.Services.AddSingleton<IPhotoService, ApiPhotoService>();
            builder.Services.AddSingleton<IFavoriteService, FavoriteService>();

            // Регистрируем ViewModels
            builder.Services.AddTransient<GalleryViewModel>();
            builder.Services.AddTransient<DetailsViewModel>();

            // Регистрируем Страницы (Views)
            builder.Services.AddTransient<GalleryPage>();
            builder.Services.AddTransient<DetailsPage>();

            // Регистрируем AppShell как главную страницу
            builder.Services.AddSingleton<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
            AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
            {
                Debug.WriteLine($"FirstChanceException: {e.Exception}");
            };
#endif

            return builder.Build();
        }
    }
}