# .NET MAUI App - Complete Documentation

This document explains every aspect of the .NET MAUI application in detail, written for beginners.

---

## What is .NET MAUI?

**.NET MAUI** stands for **.NET Multi-platform App UI**. It's a framework by Microsoft that lets you write one application that runs on:
- Windows (desktop)
- Android (phones and tablets)
- iOS (iPhones and iPads)
- macOS (Mac computers)

**Think of it like**: Writing one book that can be read in multiple languages automatically.

---

## How .NET MAUI Works

### The Architecture

```
Your Code (C# + XAML)
    ↓
.NET MAUI Framework
    ↓
Platform-Specific Code
    ↓
Windows / Android / iOS / macOS
```

1. **You write code once** in C# and XAML
2. **.NET MAUI translates** your code for each platform
3. **Each platform runs** the native version

### XAML vs C# Code

- **XAML** (`.xaml` files): Defines the **UI** (what you see)
  - Like HTML for websites, but for apps
  - Describes buttons, labels, layouts
  
- **C# Code** (`.xaml.cs` files): Defines the **logic** (what happens)
  - Handles button clicks, data loading, calculations
  - Like JavaScript for websites

**Example**:
```xml
<!-- XAML: This creates a button -->
<Button Text="Click Me" Clicked="OnButtonClicked" />
```

```csharp
// C#: This handles what happens when clicked
private void OnButtonClicked(object sender, EventArgs e)
{
    DisplayAlert("Hello", "Button was clicked!", "OK");
}
```

---

## Application Lifecycle

### 1. App Startup (`App.xaml.cs`)

When the app launches:

```csharp
public App()
{
    InitializeComponent();  // Loads XAML resources
    MainPage = new AppShell();  // Sets the main navigation
}
```

**What happens**:
1. App initializes
2. Loads global resources (colors, styles)
3. Sets `AppShell` as the main page (navigation system)

### 2. Service Registration (`MauiProgram.cs`)

Before the app runs, services are registered:

```csharp
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<HomePageImageService>();
// ... more services
```

**What this means**:
- Services are like tools available throughout the app
- `AddSingleton` means one instance is created and reused
- Other pages can request these services

**Dependency Injection**:
```csharp
// Instead of creating the service yourself:
var service = new WeatherService();  // ❌ Don't do this

// You request it, and MAUI provides it:
public HomePage(HomePageImageService imageService)  // ✅ MAUI gives it to you
{
    _imageService = imageService;
}
```

### 3. Navigation (`AppShell.xaml`)

The app uses **Shell Navigation** - think of it as a GPS system:

```xml
<FlyoutItem Title="Home" Route="Home">
    <ShellContent ContentTemplate="{DataTemplate pages:HomePage}" />
</FlyoutItem>
```

**Routes**:
- `//Home` - Goes to home page
- `//Clubs` - Goes to student clubs page
- `//Faculty` - Goes to faculty directory

**Navigation in code**:
```csharp
await Shell.Current.GoToAsync("//Home");
```

---

## Page-by-Page Breakdown

### HomePage - The Main Dashboard

**File**: `Pages/HomePage.xaml` / `HomePage.xaml.cs`

**What it does**:
- Displays the main dashboard with slideshow, events, and announcements

**Layout Structure**:
```
┌─────────────────────────────────────┐
│         TopBar (Navigation)         │
├─────────────────────────────────────┤
│                                     │
│      Slideshow (Large Images)       │
│                                     │
├──────────────────┬──────────────────┤
│                  │                  │
│   Quick Access   │  Upcoming Events │
│      Cards       │     (Sidebar)    │
│                  │                  │
│   - Maps         │  - Event 1       │
│   - Contact      │  - Event 2       │
│   - AI Advisor   │  - Event 3       │
│   - Projects     │                  │
│                  │                  │
└──────────────────┴──────────────────┘
```

**Key Features**:

1. **Image Slideshow**:
   ```csharp
   private async Task LoadHomePageImages()
   {
       var images = await _imageService.GetFeaturedHomePageImagesAsync(limit: 10);
       // Display images in slideshow
   }
   ```
   - Loads images from Supabase
   - Auto-rotates every 5 seconds
   - Shows pagination dots
   - Tap to manually navigate

2. **Event Handlers**:
   ```csharp
   private async void OnMapsTapped(object sender, EventArgs e)
   {
       await Shell.Current.GoToAsync("//Maps");
   }
   ```
   - Each card has a tap handler
   - Navigates to the appropriate page

**How the Slideshow Works**:

```csharp
// 1. Timer is created
_slideshowTimer = Dispatcher.CreateTimer();
_slideshowTimer.Interval = TimeSpan.FromSeconds(5);

// 2. Timer ticks every 5 seconds
_slideshowTimer.Tick += (s, e) => NextImage();

// 3. NextImage() moves to next image
private void NextImage()
{
    _currentImageIndex = (_currentImageIndex + 1) % _homePageImages.Count;
    UpdateSlideshowImage();  // Updates the displayed image
}
```

---

### CampusMapPage - Full Campus Map

**File**: `Pages/CampusMapPage.xaml` / `CampusMapPage.xaml.cs`

**What it does**:
- Shows the entire campus map
- Displays "You are here" marker
- Supports zoom and pan

**How it works**:

1. **Loading the Map**:
   ```csharp
   CampusMapImage.Source = "campus_map.png";
   ```
   - Loads image from Resources folder

2. **Adding Location Marker**:
   ```csharp
   private void AddCurrentLocationMarker()
   {
       var x = CampusLocationX * _mapWidth;  // Calculate position
       var y = CampusLocationY * _mapHeight;
       
       // Create marker
       var marker = new BoxView
       {
           BackgroundColor = Color.FromArgb("#FFD204"),  // Gold color
           WidthRequest = 20,
           HeightRequest = 20,
           CornerRadius = 10  // Makes it circular
       };
       
       // Position it
       AbsoluteLayout.SetLayoutBounds(marker, new Rect(x - 10, y - 10, 20, 20));
   }
   ```

3. **Zoom Functionality**:
   ```csharp
   private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
   {
       _currentScale = _startScale * e.Scale;  // Calculate new zoom
       CampusMapImage.Scale = _currentScale;  // Apply zoom
   }
   ```

---

### DepartmentMapPage - Interactive Floor Plan

**File**: `Pages/DepartmentMapPage.xaml` / `DepartmentMapPage.xaml.cs`

**What it does**:
- Embeds a web-based interactive map
- The map is a React app hosted on Vercel
- Shows room details when clicked

**How it works**:

1. **WebView Component**:
   ```xml
   <WebView x:Name="InteractiveMapWebView"
            Navigating="OnWebViewNavigating"
            Navigated="OnWebViewNavigated" />
   ```
   - WebView is like a mini browser inside the app
   - Can display any website

2. **Loading the Map**:
   ```csharp
   private void LoadVercelMap()
   {
       InteractiveMapWebView.Source = new UrlWebViewSource
       {
           Url = "https://interactive-map-for-encs-lng-dashbo.vercel.app/"
       };
   }
   ```

3. **Platform-Specific Configuration**:
   ```csharp
   #if ANDROID
   // Android-specific: Enable zoom controls
   settings.SetSupportZoom(true);
   settings.BuiltInZoomControls = true;
   #elif IOS
   // iOS-specific: Configure WebKit
   iosWebView.Configuration.AllowsInlineMediaPlayback = true;
   #elif WINDOWS
   // Windows: WebView2 supports zoom by default
   #endif
   ```

**Why use WebView?**
- The interactive map is built with React (web technology)
- WebView lets us display web content inside the native app
- Best of both worlds: native app + web interactivity

---

### AiAdvisorPage - AI Chat Display

**File**: `Pages/AiAdvisorPage.xaml` / `AiAdvisorPage.xaml.cs`

**What it does**:
- Displays chat messages from the AI Advisor
- Shows QR code for mobile access
- Auto-refreshes when new messages arrive

**How it works**:

1. **Polling for Updates**:
   ```csharp
   public async Task StartSmartRefreshAsync(...)
   {
       while (!token.IsCancellationRequested)
       {
           var currentLastUpdated = await mirrorService.GetLastUpdatedAsync(token);
           
           // Only refresh if timestamp changed
           if (currentLastUpdated != lastKnownTime)
           {
               await RefreshAsync();  // Fetch new messages
           }
           
           await Task.Delay(intervalMs, token);  // Wait 1 second
       }
   }
   ```
   - Checks every second if new messages exist
   - Only refreshes if something changed (efficient!)

2. **Displaying Messages**:
   ```csharp
   public async Task RefreshAsync()
   {
       var transcript = await _mirrorService.GetTranscriptAsync();
       
       Messages.Clear();
       foreach (var m in transcript)
       {
           Messages.Add(m);  // Add to collection
       }
   }
   ```
   - Fetches all messages from Flask API
   - Displays in a chat-like interface

3. **QR Code Generation**:
   ```csharp
   public string QrImageUrl => BuildQrUrl(AiAdvisorConfig.ChatUrl);
   
   private static string BuildQrUrl(string target)
   {
       var encoded = Uri.EscapeDataString(target);
       return $"https://api.qrserver.com/v1/create-qr-code/?size=240x240&data={encoded}";
   }
   ```
   - Uses QR Server API to generate QR code
   - QR code links to the web chat interface

---

### StudentClubsPage - Club Directory

**File**: `Pages/StudentClubsPage.xaml` / `StudentClubsPage.xaml.cs`

**What it does**:
- Lists all student clubs in a grid
- Allows searching and filtering
- Tap to see club details

**How it works**:

1. **Loading Clubs**:
   ```csharp
   private async Task LoadClubsAsync()
   {
       var clubs = await _clubService.GetAllClubsAsync();
       ClubsCollection.Clear();
       foreach (var club in clubs)
       {
           ClubsCollection.Add(club);
       }
   }
   ```

2. **Data Binding**:
   ```xml
   <CollectionView ItemsSource="{Binding ClubsCollection}">
       <CollectionView.ItemTemplate>
           <DataTemplate>
               <Grid>
                   <Label Text="{Binding Name}" />
                   <Image Source="{Binding LogoUrl}" />
               </Grid>
           </DataTemplate>
       </CollectionView.ItemTemplate>
   </CollectionView>
   ```
   - `ItemsSource` connects to the C# collection
   - `{Binding Name}` displays the club name
   - When collection updates, UI updates automatically

---

### FacultyDirectoryPage - Faculty Listing

**File**: `Pages/FacultyDirectoryPage.xaml` / `FacultyDirectoryPage.xaml.cs`

**What it does**:
- Shows all faculty members in a grid
- Displays photos, names, departments
- Search and filter functionality

**Similar to StudentClubsPage** but for faculty data.

---

## TopBar - Global Navigation

**File**: `Controls/TopBar.xaml` / `TopBar.xaml.cs`

**What it does**:
- Appears on every page (global component)
- Provides navigation, search, weather, and clock

**Components**:

1. **Hamburger Menu**:
   ```csharp
   private void OnMenuTapped(object sender, EventArgs e)
   {
       Shell.Current.FlyoutIsPresented = true;  // Opens navigation drawer
   }
   ```

2. **Weather Display**:
   ```csharp
   private async Task UpdateWeatherAsync()
   {
       var weatherData = await _weatherService.GetCampusWeatherAsync();
       WeatherLabel.Text = weatherData.FormattedWeather;  // "75F  Sunny"
       WeatherIcon.Source = weatherData.Icon;  // Weather icon image
   }
   ```
   - Updates every 10 minutes
   - Uses `WeatherService` to fetch data

3. **Clock**:
   ```csharp
   private void UpdateClock()
   {
       var now = DateTime.Now;
       ClockLabel.Text = $"{now:MMM dd} • {now:hh:mm:ss tt}";
       // Example: "Jan 15 • 02:30:45 PM"
   }
   ```
   - Updates every second
   - Uses `IDispatcherTimer`

4. **Global Search**:
   ```csharp
   private void PerformSearch(string query)
   {
       var results = SearchIndex
           .Where(item => item.Matches(query))  // Filter matching items
           .GroupBy(item => item.Category)   // Group by category
           .ToList();
       
       RenderSearchResults(results, query);
   }
   ```
   - Searches through predefined index
   - Matches against titles, descriptions, keywords
   - Groups results by category

---

## Data Binding

**What is Data Binding?**
- Connects UI elements to data
- When data changes, UI updates automatically
- No manual UI updates needed!

**Example**:
```xml
<!-- XAML -->
<Label Text="{Binding Temperature}" />
```

```csharp
// C# - Set the binding context
public class WeatherViewModel
{
    public string Temperature { get; set; } = "75F";
}

// In page:
BindingContext = new WeatherViewModel();
// Label automatically shows "75F"
```

**Two-Way Binding**:
```xml
<Entry Text="{Binding UserName, Mode=TwoWay}" />
```
- User types → Updates `UserName` property
- Code changes `UserName` → Updates text box

---

## Navigation System

### Shell Navigation

**Routes** (defined in `AppShell.xaml`):
- `//Home` - Home page
- `//Clubs` - Student clubs
- `//Faculty` - Faculty directory
- `//Maps` - Maps selection page
- `//DepartmentMap` - Department floor plan
- `//CampusMap` - Campus map
- `//AIAdvisor` - AI Advisor page

**Navigation Methods**:

1. **Simple Navigation**:
   ```csharp
   await Shell.Current.GoToAsync("//Home");
   ```

2. **Navigation with Parameters**:
   ```csharp
   await Shell.Current.GoToAsync($"ClubDetail?clubId={club.Id}");
   ```

3. **Back Navigation**:
   ```csharp
   await Shell.Current.GoToAsync("..");  // Go back
   ```

### Flyout Menu

The hamburger menu (flyout) shows all available pages:
- Home
- Maps
- Student Clubs
- Projects
- E-Week
- Academic Catalogue
- etc.

---

## Services and Dependency Injection

### What are Services?

Services are classes that handle specific tasks:
- `WeatherService` - Fetches weather
- `HomePageImageService` - Fetches images
- `FacultyService` - Fetches faculty data

### Dependency Injection

Instead of creating services yourself:
```csharp
// ❌ Bad: Creating service manually
var service = new WeatherService();
```

You request them, and MAUI provides them:
```csharp
// ✅ Good: Requesting service
public HomePage(HomePageImageService imageService)
{
    _imageService = imageService;  // MAUI gives it to you
}
```

**Registration** (in `MauiProgram.cs`):
```csharp
builder.Services.AddSingleton<WeatherService>();
// Now any page can request WeatherService
```

**Lifetimes**:
- `AddSingleton` - One instance for entire app
- `AddTransient` - New instance each time
- `AddScoped` - One instance per scope (not commonly used in MAUI)

---

## Platform-Specific Code

Sometimes you need different code for different platforms:

```csharp
#if ANDROID
    // Android-specific code
    settings.SetSupportZoom(true);
#elif IOS
    // iOS-specific code
    iosWebView.Configuration.AllowsInlineMediaPlayback = true;
#elif WINDOWS
    // Windows-specific code
    // WebView2 supports zoom by default
#endif
```

**When to use**:
- Platform-specific APIs
- Different UI behaviors
- Platform-specific optimizations

---

## Resources and Assets

### Images

**Included in project**:
```xml
<MauiImage Include="Resources\Images\*" />
```

**Usage**:
```xml
<Image Source="mcneeselogo.png" />
```

### Fonts

**Registration**:
```csharp
fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
```

**Usage**:
```xml
<Label FontFamily="OpenSansRegular" Text="Hello" />
```

### Styles

**Defined in** `Resources/Styles/Styles.xaml`:
```xml
<Style x:Key="Headline" TargetType="Label">
    <Setter Property="FontSize" Value="32" />
    <Setter Property="FontAttributes" Value="Bold" />
</Style>
```

**Usage**:
```xml
<Label Style="{StaticResource Headline}" Text="Title" />
```

---

## Error Handling

**Try-Catch Blocks**:
```csharp
try
{
    var data = await service.FetchDataAsync();
    // Use data
}
catch (HttpRequestException ex)
{
    // Handle network error
    DisplayAlert("Error", "Could not connect to server", "OK");
}
catch (Exception ex)
{
    // Handle any other error
    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
}
```

**Best Practices**:
- Always wrap API calls in try-catch
- Show user-friendly error messages
- Log errors for debugging

---

## Performance Tips

1. **Use ObservableCollection**:
   - Automatically updates UI when items change
   ```csharp
   private ObservableCollection<Club> _clubs = new();
   ```

2. **Lazy Loading**:
   - Load data only when needed
   ```csharp
   protected override async void OnAppearing()
   {
       await LoadDataAsync();  // Load when page appears
   }
   ```

3. **Image Caching**:
   - MAUI caches images automatically
   - Reuse image sources when possible

4. **Dispose Timers**:
   ```csharp
   protected override void OnDisappearing()
   {
       _timer?.Stop();  // Stop timer when leaving page
   }
   ```

---

## Summary

- **XAML** defines UI, **C#** defines logic
- **Services** handle data and API calls
- **Data Binding** connects UI to data
- **Shell Navigation** handles page navigation
- **Platform-Specific Code** for platform differences
- **Resources** for images, fonts, styles

The app follows a clear structure:
1. User interacts with UI (XAML)
2. Event handler runs (C#)
3. Service fetches data
4. Data updates UI (binding)

This pattern repeats throughout the app! 🎉
