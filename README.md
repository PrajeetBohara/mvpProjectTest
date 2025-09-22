# McNeese State University Engineering & Computer Science Dashboard

## 📋 Project Overview

This is a **Smart TV Kiosk Dashboard Application** developed for the McNeese State University Department of Engineering & Computer Science. The application is designed to display important information, announcements, events, and university data in an engaging and user-friendly interface optimized for large display screens, tablets, and mobile devices.

**Authors:** 
- Prajeet Bohara (Primary Developer - UI/UX, Frontend Architecture, API Integration)
- Jael Ruiz (Database Schema Design, Backend Architecture)

**Academic Context:** Senior Design Fall 2025 Project

---

## 🚀 Technical Architecture

### Technology Stack
- **Framework:** .NET MAUI (Multi-platform App UI) 8.0
- **Language:** C# 12.0
- **UI Markup:** XAML
- **Platforms:** Windows, Android, iOS, macOS
- **External APIs:** WeatherAPI.com for real-time weather data
- **Backend:** Supabase (PostgreSQL-based BaaS) - Schema designed but not yet integrated
- **Package Manager:** NuGet

### Key Technologies
- **Microsoft.Maui.Controls** - Cross-platform UI framework
- **System.Text.Json** - JSON serialization for API calls
- **Newtonsoft.Json** - Additional JSON handling capabilities
- **IDispatcherTimer** - Thread-safe UI updates for real-time data

---

## 📁 Project Structure

```
Dashboard/
├── 📂 Controls/                    # Reusable UI Components
│   ├── TopBar.xaml                 # Global navigation bar UI
│   └── TopBar.xaml.cs              # Navigation bar logic & timers
├── 📂 Pages/                       # Application screens
│   ├── HomePage.xaml               # Main dashboard layout
│   ├── HomePage.xaml.cs            # Homepage interactions
│   ├── AllEventsPage.xaml          # Events listing page
│   ├── AllEventsPage.xaml.cs       # Events page logic
│   ├── AllAnnouncementsPage.xaml   # Announcements listing
│   └── AllAnnouncementsPage.xaml.cs
├── 📂 Services/                    # Business logic & API services
│   └── WeatherService.cs           # Weather API integration
├── 📂 Resources/                   # Assets & styling
│   ├── 📂 Images/                  # Application images
│   │   ├── mcneeselogo.png         # University logo
│   │   ├── dotnet_bot.png          # Placeholder images
│   │   └── search.png              # UI icons
│   ├── 📂 Styles/                  # Application theming
│   │   ├── Colors.xaml             # Color definitions
│   │   └── Styles.xaml             # Global UI styles
│   ├── 📂 Fonts/                   # Custom fonts
│   └── 📂 Splash/                  # Splash screen assets
├── 📂 Platforms/                   # Platform-specific code
│   ├── 📂 Android/                 # Android configuration
│   ├── 📂 iOS/                     # iOS configuration
│   ├── 📂 Windows/                 # Windows configuration
│   └── 📂 MacCatalyst/             # macOS configuration
├── App.xaml                        # Global application resources
├── App.xaml.cs                     # Application lifecycle
├── AppShell.xaml                   # Navigation structure
├── AppShell.xaml.cs                # Shell configuration
├── MauiProgram.cs                  # Application startup
└── Dashboard.csproj                # Project configuration
```

---

## 🎨 Key Features

### 1. **Global Navigation Bar (TopBar)**
- **Location:** Always visible at the top of every page
- **Components:**
  - 🍔 Hamburger menu for navigation
  - 🏠 Home button for quick return to dashboard
  - 📍 Department title display
  - 🔍 Search functionality (placeholder)
  - 🌤️ Real-time weather with icons (Lake Charles, LA)
  - 🕐 Live clock with date display
- **Features:**
  - Responsive design for different screen sizes
  - Weather updates every 10 minutes
  - Clock updates every second
  - Touch-friendly interface for kiosk use

### 2. **Homepage Dashboard Layout**
- **Top Section:** Large slideshow area for student projects and achievements
- **Right Sidebar:** Upcoming events with scroll capability
- **Bottom Row:**
  - Maps section for campus navigation
  - Announcements section for latest updates
  - University logo display (transparent background)
- **Design Elements:**
  - Dark theme with card-based layout
  - Rounded corners and subtle shadows
  - Responsive grid system
  - Background logo with transparency

### 3. **Navigation Structure**
The application uses Shell-based navigation with the following routes:
- **Home** - Main dashboard
- **Student Clubs** - Student organization information
- **Senior Design Projects** - Project showcases
- **E-Week** - Engineering week events
- **Academic Catalogue** - Course information
- **Gallery** - Photo and media gallery
- **All Events** - Complete events listing
- **All Announcements** - Complete announcements
- **Faculty Directory** - Staff information
- **Sponsors and Donors** - Recognition page

### 4. **Weather Integration**
- **API:** WeatherAPI.com integration
- **Location:** Lake Charles, Louisiana (university location)
- **Data Display:** Temperature, condition text, and weather icons
- **Update Frequency:** Every 10 minutes
- **Fallback:** Static placeholder if API fails

---

## 🏗️ Code Architecture

### 1. **XAML Structure**
```xml
<!-- Example of responsive design pattern used throughout -->
<Label FontSize="{OnPlatform Default=16, Android=14, iOS=14}" />
```
- **OnPlatform** markup extension for responsive design
- **Grid** layouts for complex arrangements
- **Frame** controls for card-style containers
- **DynamicResource** for theme consistency

### 2. **C# Code Patterns**
```csharp
// Async/await pattern for API calls
public async Task<WeatherData?> GetCurrentWeatherAsync(string location)

// Timer implementation for real-time updates
private IDispatcherTimer? _clockTimer;

// Event handlers for user interactions
private async void OnHomeTapped(object sender, EventArgs e)
```

### 3. **Dependency Management**
- **Services:** Dependency injection ready (WeatherService)
- **Models:** Data classes for API responses
- **Attributes:** JsonPropertyName for API serialization
- **Error Handling:** Try-catch blocks with debugging output

---

## ⚙️ Setup & Installation

### Prerequisites
- **Visual Studio 2022** (17.8 or later) with MAUI workload
- **.NET 8.0 SDK**
- **Platform SDKs** (Android, iOS, Windows as needed)

### Installation Steps
1. **Clone the repository**
   ```bash
   git clone [repository-url]
   cd Dashboard
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run on specific platform**
   ```bash
   # Windows
   dotnet run --framework net8.0-windows10.0.19041.0
   
   # Android (with emulator/device)
   dotnet run --framework net8.0-android
   ```

### Configuration
- **Weather API:** Replace API key in `WeatherService.cs` (line 21)
- **Location:** Modify location in `GetCampusWeatherAsync()` method
- **Colors:** Customize in `App.xaml` and `Resources/Styles/Colors.xaml`
- **University Branding:** Replace logo in `Resources/Images/mcneeselogo.png`

---

## 🎯 Usage Instructions

### For End Users (Kiosk Operation)
1. **Navigation:** Use hamburger menu (☰) to access different sections
2. **Home Return:** Tap home icon (🏠) from any page
3. **Content Interaction:** 
   - Tap slideshow for full-screen view
   - Tap "View All" buttons for detailed pages
   - Use scroll in Events/Announcements sections
4. **Information Display:** Weather and time update automatically

### For Developers
1. **Adding New Pages:**
   ```csharp
   // 1. Create XAML page in Pages/ folder
   // 2. Add to AppShell.xaml navigation
   // 3. Include TopBar control in layout
   <controls:TopBar Grid.Row="0" />
   ```

2. **Modifying Styles:**
   - Global colors: `App.xaml` or `Resources/Styles/Colors.xaml`
   - Component styles: `Resources/Styles/Styles.xaml`
   - Responsive sizing: Use `OnPlatform` markup extension

3. **API Integration:**
   - Follow `WeatherService.cs` pattern
   - Use async/await for HTTP calls
   - Implement proper error handling

---

## 🔮 Future Development Plans

### Phase 1: Backend Integration
- [ ] Connect to Supabase database
- [ ] Implement user authentication
- [ ] Dynamic content management
- [ ] Real-time data synchronization

### Phase 2: Enhanced Features
- [ ] Complete search functionality
- [ ] Interactive campus maps
- [ ] Full slideshow implementation
- [ ] Push notifications for announcements

### Phase 3: Advanced Capabilities
- [ ] Analytics and usage tracking
- [ ] Administrative dashboard
- [ ] Content scheduling system
- [ ] Multi-language support

### Phase 4: Smart Features
- [ ] AI-powered content recommendations
- [ ] Voice interaction capabilities
- [ ] Gesture recognition for kiosk use
- [ ] Accessibility enhancements

---

## 🛠️ Troubleshooting

### Common Issues
1. **Build Errors:**
   - Ensure all NuGet packages are restored
   - Check .NET 8.0 SDK installation
   - Verify MAUI workload in Visual Studio

2. **Weather Not Loading:**
   - Check internet connectivity
   - Verify API key validity
   - Review debug output for API errors

3. **UI Layout Issues:**
   - Test on different screen sizes
   - Check OnPlatform configurations
   - Verify Grid row/column definitions

### Debug Information
- Weather API calls logged to debug output
- Error handling in all async operations
- Comprehensive exception catching

---

## 📚 Learning Resources

### .NET MAUI Documentation
- [Official MAUI Documentation](https://docs.microsoft.com/en-us/dotnet/maui/)
- [XAML Syntax Guide](https://docs.microsoft.com/en-us/dotnet/desktop/xaml-services/)
- [Shell Navigation](https://docs.microsoft.com/en-us/dotnet/maui/fundamentals/shell)

### APIs & Services
- [WeatherAPI.com Documentation](https://www.weatherapi.com/docs/)
- [Supabase Documentation](https://supabase.com/docs)

---

## 📄 License & Academic Use

This project is developed as part of the McNeese State University Senior Design course. The code is intended for educational and university purposes. Please respect any licensing requirements of third-party libraries and APIs used.

---

## 🤝 Contributing

For academic purposes and course requirements:
1. Follow existing code patterns and comments
2. Maintain responsive design principles
3. Update documentation for new features
4. Test across multiple platforms before submission

---

## 📞 Contact & Support

**Development Team:**
- Prajeet Bohara - Primary Developer
- Jael Ruiz - Database Architect

**Academic Supervisor:** [Professor Name]
**Institution:** McNeese State University, Department of Engineering & Computer Science
**Course:** Senior Design Fall 2025

---

*Last Updated: September 2025*
*Version: 1.0.0*
