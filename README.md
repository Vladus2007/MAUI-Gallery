

# 🖼️ Gallery App - .NET MAUI Image Gallery

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-purple.svg)](https://dotnet.microsoft.com/apps/maui)
[![Platforms](https://img.shields.io/badge/platforms-Android%20|%20iOS-blue.svg)](https://dotnet.microsoft.com/apps/maui)

## 📋 Project Overview

**Gallery App** is a cross-platform mobile application for viewing and managing photos, developed as a test assignment using .NET MAUI.

**Contact Information:**
- 📧 **Email:** suskovladislav9@gmail.com

### 🎯 Implemented Requirements

**Core Features:**
- ✅ **Image Gallery** - View photos from Unsplash API
- ✅ **Favorites System** - Local storage of favorite photos
- ✅ **Pagination** - Auto-loading on scroll
- ✅ **Detailed View** - Enlarged image screen
- ✅ **Loading Indicators** - Visual feedback

**Technical Requirements:**
- ✅ **.NET MAUI 9.0** - Modern cross-platform development
- ✅ **MVVM Architecture** - Clean separation of concerns
- ✅ **SOLID Principles** - High-quality, maintainable code
- ✅ **Asynchronous Programming** - async/await patterns
- ✅ **Local Storage** - Saving favorites via Preferences

### 🔮 Additional Features

**Beyond Requirements:**
- 🚀 **Performance Optimization** - Image caching
- 🎨 **Custom Converters** - Dynamic favorite icons
- 📱 **Responsive UI** - Support for different screen sizes
- ⚡ **Error Handling** - Resilience to network issues

**Architectural Improvements:**
- 🏗 **Dependency Injection** - Flexible architecture
- 📡 **Service Layer** - Isolation of business logic
- 🔄 **Reactive UI** - Automatic interface updates

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with .NET MAUI workload
- Android/iOS device or emulator
- [Unsplash API Key](https://unsplash.com/developers) (free)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/gallery-app.git
cd gallery-app
```

2. **Configure API Key**
```csharp
// AppConstants.cs
public static class AppConstants
{
    public const string UnsplashApiKey = "YOUR_UNSPLASH_ACCESS_KEY";
    public const string UnsplashBaseUrl = "https://api.unsplash.com";
}
```

3. **Run the application**
```bash
# Android
dotnet build -t:Run -f net9.0-android

# iOS (requires Mac)
dotnet build -t:Run -f net9.0-ios
```

## 🏗 Architecture

### Project Structure
```
GalleryApp/
├── Models/           # Data models (Photo, Urls)
├── Services/         # Business logic layer
│   ├── Interfaces/   # Contracts (IPhotoService, IFavoriteService)
│   ├── ApiPhotoService.cs    # Unsplash API integration
│   └── FavoriteService.cs    # Local storage management
├── ViewModels/       # Presentation logic
│   ├── BaseViewModel.cs      # INotifyPropertyChanged base
│   ├── GalleryViewModel.cs   # Main screen logic
│   └── DetailsViewModel.cs   # Detail screen logic
├── Views/            # UI layer
│   ├── GalleryPage.xaml      # Image grid
│   └── DetailsPage.xaml      # Full-size view
├── Converters/       # Value converters
│   └── BoolToHeartIconConverter.cs
└── Resources/        # Assets and styling
```

### Key Patterns
- **MVVM** - Clean separation of UI and logic
- **Repository** - Data access abstraction
- **Dependency Injection** - Dependency injection
- **Observer** - Reactive UI updates

## 💾 Data Flow

```
Unsplash API → ApiPhotoService → GalleryViewModel → GalleryPage
      ↑              ↓                  ↓              ↓
   JSON Data     HTTP Client      ObservableCollection   DataTemplate
      ↑              ↓                  ↓              ↓
Local Storage ← FavoriteService ← DetailsViewModel ← DetailsPage
```

## 🔧 Configuration

### Application Settings
The application uses constants for easy deployment:

```csharp
public static class AppConstants
{
    public const string UnsplashApiKey = "your_key_here";
    public const int PageSize = 30;
    public const string FavoritesStorageKey = "favorite_photos";
}
```

## 📱 UI/UX Features

### Gallery Screen
- **3-column grid** - Optimal use of space
- **Infinite scroll** - Pagination with 5-item threshold
- **Loading indicators** - Visual feedback
- **Favorite icons** - Quick access to favorite photos

### Detail Screen  
- **Full-screen view** - Maximum image area
- **Photo information** - Title and description
- **Favorite toggle** - One-tap management

## 🧪 Testing Strategy

### Unit Testing (Planned)
```csharp
// Example unit test
[Test]
public void ToggleFavorite_AddsToFavorites_WhenNotFavorite()
{
    var service = new FavoriteService();
    var photoId = "test_photo_123";
    
    service.ToggleFavoriteAsync(photoId);
    
    Assert.IsTrue(service.IsFavoriteAsync(photoId).Result);
}
```

### Manual Testing
- [x] Loading images from API
- [x] Pagination and infinite scroll
- [x] Adding/removing from favorites
- [x] Navigation between screens
- [x] Network error handling

## 📊 Performance

### Optimizations
- **Image caching** - Reuse of loaded resources
- **List virtualization** - Efficient rendering of large collections
- **Asynchronous operations** - Non-blocking UI
- **Local storage** - Minimization of network requests

## 🤝 Contributing

### Commit Convention
```
feat: Added pagination support
fix: Resolved image loading issue
refactor: Improved service layer architecture
docs: Updated README with setup instructions
```

## 📄 License

This project is developed as a test assignment. All rights reserved.

## 🙏 Acknowledgments

- **Unsplash** for providing excellent free API
- **.NET MAUI Team** for cross-platform framework
- **Material Design** for UI inspiration

---

<div align="center">

**Developed with ❤️ using .NET MAUI**

*Test assignment completed in compliance with all requirements and best practices*

</div>

## 🎯 Future Enhancements

- [ ] **Search and filtering** - Advanced photo search
- [ ] **Offline mode** - Viewing cached images
- [ ] **Analytics** - Tracking user activity
- [ ] **Dark theme** - Support for system preferences

---

*This project demonstrates a complete understanding of .NET MAUI, modern development practices, and architectural patterns.*
