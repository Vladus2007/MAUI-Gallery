# 🖼️ Gallery App - Modern .NET MAUI Image Gallery

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-purple.svg)](https://dotnet.microsoft.com/apps/maui)
[![Platforms](https://img.shields.io/badge/platforms-Android%20|%20iOS%20|%20Windows-blue.svg)](https://dotnet.microsoft.com/apps/maui)
[![Architecture](https://img.shields.io/badge/architecture-MVVM%20|%20SOLID-green.svg)](https://learn.microsoft.com/dotnet/architecture/maui/)

A modern, cross-platform image gallery application built with .NET MAUI that showcases best practices in mobile development, clean architecture, and modern .NET patterns.

## ✨ Features

### 🎯 Core Functionality
- **📷 Image Gallery** - Browse curated photos from Unsplash API
- **💖 Favorites System** - Mark and manage favorite images with local persistence
- **📱 Responsive Design** - Optimized for mobile devices and different screen sizes
- **🔄 Smart Pagination** - Infinite scroll with automatic loading of new content

### 🛠 Technical Excellence
- **🏗 MVVM Architecture** - Clean separation of concerns
- **📡 API Integration** - Robust HTTP client with error handling
- **💾 Local Storage** - SQLite-based favorites management
- **🎨 Custom Controls** - Beautiful UI with smooth animations
- **⚡ Performance Optimized** - Image caching and efficient memory usage

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with .NET MAUI workload
- Android Emulator or physical device
- [Unsplash API Key](https://unsplash.com/developers) (free)

### Installation & Running

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/gallery-app.git
cd gallery-app
```

2. **Configure API Key**
```csharp
// In AppConstants.cs, update with your Unsplash API key
public static class AppConstants
{
    public const string UnsplashApiKey = "YOUR_UNSPLASH_ACCESS_KEY_HERE";
    public const string UnsplashBaseUrl = "https://api.unsplash.com";
}
```

3. **Restore dependencies**
```bash
dotnet restore
```

4. **Run the application**
```bash
# For Android
dotnet build -t:Run -f net9.0-android

# Or run from Visual Studio
# Select GalleryApp.Android as startup project and press F5
```

## 🏗 Architecture Overview

### Project Structure
```
GalleryApp/
├── Models/           # Data models and entities
├── Services/         # Business logic and API integration
│   └── Interfaces/   # Service contracts
├── ViewModels/       # Presentation logic
├── Views/            # UI components and pages
├── Converters/       # XAML value converters
└── Resources/        # Assets, styles, and configurations
```

### Key Design Patterns
- **MVVM (Model-View-ViewModel)** - Clean architecture separation
- **Dependency Injection** - Loose coupling and testability
- **Repository Pattern** - Abstract data access layer
- **Observer Pattern** - Reactive property updates

## 💻 Code Examples

### Modern Async/Await Implementation
```csharp
public async Task<List<Photo>> GetPhotosAsync(int page, int perPage = 30)
{
    try
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/photos?page={page}&per_page={perPage}");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Photo>>(content) ?? new List<Photo>();
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"API Error: {ex.Message}");
        throw new GalleryException("Failed to load photos", ex);
    }
}
```

### Reactive Favorites Management
```csharp
public class FavoriteService : IFavoriteService
{
    public async Task ToggleFavoriteAsync(string photoId)
    {
        if (await IsFavoriteAsync(photoId))
            await RemoveFavoriteAsync(photoId);
        else
            await AddFavoriteAsync(photoId);
            
        // Notify UI of changes
        FavoritesChanged?.Invoke(this, EventArgs.Empty);
    }
}
```

## 📱 UI/UX Features

### Gallery Page
- **Grid Layout** - Responsive 3-column image grid
- **Smooth Scrolling** - Optimized CollectionView with virtualization
- **Loading States** - Elegant loading indicators and error handling
- **Pull to Refresh** - Intuitive content refresh gesture

### Image Details
- **Full-Screen View** - Optimized image display with zoom support
- **Favorite Toggle** - One-tap favorite management
- **Image Information** - Metadata and descriptions
- **Swipe Navigation** - Gesture-based browsing

## 🔧 Configuration

### Environment Setup
The app supports multiple configuration environments:

```json
// appsettings.Development.json
{
  "ApiSettings": {
    "BaseUrl": "https://api.unsplash.com",
    "TimeoutSeconds": 30
  },
  "AppSettings": {
    "PageSize": 30,
    "CacheDurationMinutes": 60
  }
}
```

### Platform-Specific Customizations
- **Android** - Material Design 3 components
- **iOS** - Native navigation patterns
- **Windows** - Desktop-optimized layout

## 🧪 Testing

### Unit Tests
```csharp
[Test]
public async Task GetPhotosAsync_ReturnsPhotos_OnSuccessfulApiCall()
{
    // Arrange
    var mockHttpClient = CreateMockHttpClientWithSampleData();
    var service = new ApiPhotoService(mockHttpClient);
    
    // Act
    var result = await service.GetPhotosAsync(1);
    
    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual(30, result.Count);
}
```

### UI Tests
```csharp
[Test]
public void GalleryPage_DisplaysImages_WhenLoaded()
{
    // Arrange
    var app = AppFactory.StartApp();
    
    // Act
    app.Tap("GalleryTab");
    
    // Assert
    app.WaitForElement("ImageCell");
    Assert.IsTrue(app.Query("ImageCell").Length > 0);
}
```

## 📊 Performance Optimizations

### Image Loading
- **Lazy Loading** - Images load as they enter viewport
- **Memory Cache** - Intelligent caching strategy
- **Downsizing** - Appropriate resolution for display size

### Network Efficiency
- **Request Batching** - Efficient pagination
- **Error Retry** - Automatic retry with exponential backoff
- **Offline Support** - Basic offline functionality

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

### Development Workflow
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 🙏 Acknowledgments

- **Unsplash** for providing the excellent image API
- **.NET MAUI Team** for the fantastic cross-platform framework
- **Material Design** for the design inspiration
- **Community Contributors** for their valuable input

## 🏆 What This Project Demonstrates

This application serves as a comprehensive example of modern .NET MAUI development, showcasing:

✅ **Enterprise Architecture Patterns**  
✅ **Cross-Platform Development Best Practices**  
✅ **Modern .NET Features** (C# 12, async/await, etc.)  
✅ **UI/UX Excellence**  
✅ **Performance Optimization**  
✅ **Code Maintainability**  

---

<div align="center">

**⭐ Star this repo if you found it helpful!**

*Built with ❤️ using .NET MAUI*

</div>

## 🎯 Roadmap

- [ ] **v1.1** - Advanced search and filtering
- [ ] **v1.2** - Offline mode with sync
- [ ] **v1.3** - Social features (sharing, comments)
- [ ] **v2.0** - AI-powered image recommendations

---

