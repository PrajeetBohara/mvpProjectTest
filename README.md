Interactive Dashboard: A Cross-Platform Prototype for the Department of Engineering and Computer Science (ENCS) and LNG Center

1. Project Overview

This is a "Smart TV Dashboard Application Prototype" developed for the McNeese State University Department of Engineering & Computer Science. The application is designed to display important information, announcements, events, and university data in an engaging and user-friendly interface optimized for large display smart screen. However, the prototype will be displayed in an android tablet.

Authors:
- Prajeet Bohara (Senior, Computer Science, McNeese State University)
- Jael Ruiz (Senior, Computer Science, McNeese State University)

Academic Context: Senior Design Project Fall 2025 


2. Technical Architecture

Technology Stack
- Framework: .NET MAUI (Multi-platform App UI) 8.0
- Language: C# 
- UI Markup: XAML
- Platforms: Windows, Android, iOS, macOS
- External APIs: WeatherAPI.com for real-time weather data
- Backend: Supabase (PostgreSQL-based BaaS) - Schema designed but not yet integrated
- Package Manager: NuGet

Key Technologies
- Microsoft.Maui.Controls - Cross-platform UI framework
- System.Text.Json - JSON serialization for API calls
- Newtonsoft.Json - Additional JSON handling capabilities
- IDispatcherTimer - Thread-safe UI updates for real-time data

3. Project Structure

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

4. Key Features (As of 09/21/2025)

a. Global Navigation Bar (TopBar)
- Location: Always visible at the top of every page
- Components:
  - Hamburger menu for navigation
  - Home button for quick return to dashboard
  - Department title display
  - Search functionality (placeholder)
  - Real-time weather with icons (Weatherapi.com)
  - Live clock with date display
- Features:
  - Responsive design for different screen sizes
  - Current weather
  - Clock updates every second
  - Touch-friendly interface for kiosk use

b. Homepage Dashboard Layout
- Top Section: Large slideshow area for student projects and recent activities
- Right Sidebar: Upcoming events with scroll capability
- Bottom Row:
  - Maps section for campus navigation (placeholder)
  - Announcements section for latest updates (placeholder)
  - University logo display (transparent background)
-Design Elements:
  - Dark theme with card-based layout
  - Rounded corners and subtle shadows
  - Responsive grid system
  - Background logo with transparency

c. Navigation Structure
The application uses Shell-based navigation with the following routes:
- Home - Main dashboard
- Student Clubs - Student organization information
- Senior Design Projects - Project showcases
- E-Week - Engineering week events
- Academic Catalogue - Course information
- Gallery - Photo and media gallery
- All Events - Complete events listing
- All Announcements - Complete announcements
- Faculty Directory - Staff information
- Sponsors and Donors - Recognition page

d. Weather Integration
- API: WeatherAPI.com integration
- Location: Lake Charles, Louisiana (For prototype)
- Data Display: Temperature, condition text, and weather icons
- Fallback: Static placeholder if API fails


### 3. **Dependency Management**
- **Services:** Dependency injection ready (WeatherService)
- **Models:** Data classes for API responses
- **Attributes:** JsonPropertyName for API serialization
- **Error Handling:** Try-catch blocks with debugging output

---

5. Setup & Installation

 Prerequisites
- Visual Studio 2022 (17.8 or later) with MAUI workload
- .NET 8.0 SDK
- Platform SDKs (Android, iOS, Windows as needed)


6. License & Academic Use

This project is developed as part of the McNeese State University Senior Design course. The code is intended for educational and university purposes. Please respect any licensing requirements of third-party libraries and APIs used.

📞 Contact & Support

Development Team:
- Prajeet Bohara 
- Jael Ruiz 

Academic Supervisor: Dr. Jennifer Lavergne
Institution: McNeese State University, Department of Engineering & Computer Science
Course: CSCI 491


Last Updated: September 2025
Version: 1.0.0
