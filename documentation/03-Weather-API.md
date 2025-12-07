# Weather API Integration - Complete Guide

This document explains how the Weather API works in this application, written for beginners.

---

## What is WeatherAPI.com?

**WeatherAPI.com** is a service that provides real-time weather data. Think of it as a weather station that you can ask questions to over the internet.

**What it provides**:
- Current temperature
- Weather conditions (sunny, cloudy, rainy, etc.)
- Weather icons
- Location information
- And much more!

---

## Why Do We Use It?

The dashboard displays current weather in the **TopBar** (the navigation bar at the top of every page). This gives users immediate information about the weather at the campus location (Lake Charles, Louisiana).

---

## How It Works - The Big Picture

```
User sees TopBar
    ↓
TopBar creates WeatherService
    ↓
WeatherService calls WeatherAPI.com
    ↓
WeatherAPI.com returns JSON data
    ↓
WeatherService parses JSON
    ↓
WeatherService returns WeatherData object
    ↓
TopBar displays temperature and icon
```

---

## The Code Explained

### Step 1: WeatherService Class

**File**: `Dashboard/Services/WeatherService.cs`

This class handles all communication with WeatherAPI.com.

#### Constructor

```csharp
public WeatherService()
{
    _httpClient = new HttpClient();
    _apiKey = "f3ea5473f30a4e14b0c35729251808";  // Your API key
}
```

**What this does**:
- Creates an `HttpClient` - this is like a web browser for code
- Stores the API key - this is your password to access WeatherAPI.com

**⚠️ Important**: In production, you should store the API key securely (not hardcoded). For now, it's in the code for simplicity.

#### The Main Method: GetCurrentWeatherAsync

```csharp
public async Task<WeatherData?> GetCurrentWeatherAsync(string location)
{
    // Step 1: Build the URL
    var url = $"{_baseUrl}/current.json?key={_apiKey}&q={location}&aqi=no";
    
    // Step 2: Make the request
    var response = await _httpClient.GetAsync(url);
    
    // Step 3: Check if successful
    if (response.IsSuccessStatusCode)
    {
        // Step 4: Read the response
        var json = await response.Content.ReadAsStringAsync();
        
        // Step 5: Parse JSON into objects
        var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json);
        
        // Step 6: Convert to our WeatherData format
        return new WeatherData
        {
            Temperature = weatherResponse.Current.TempF,
            Condition = weatherResponse.Current.Condition.Text,
            Icon = weatherResponse.Current.Condition.Icon,
            Location = weatherResponse.Location.Name
        };
    }
    
    return null;  // Failed
}
```

**Breaking it down**:

1. **Build URL**: 
   ```
   https://api.weatherapi.com/v1/current.json?key=YOUR_KEY&q=Lake%20Charles&aqi=no
   ```
   - `current.json` - endpoint for current weather
   - `key=...` - your API key
   - `q=Lake%20Charles` - location (URL encoded)
   - `aqi=no` - don't include air quality

2. **Make Request**:
   ```csharp
   var response = await _httpClient.GetAsync(url);
   ```
   - `GetAsync` sends a GET request (like typing URL in browser)
   - `await` waits for the response (async operation)

3. **Check Status**:
   ```csharp
   if (response.IsSuccessStatusCode)
   ```
   - Checks if response is 200 OK (success)
   - If not, returns null

4. **Read JSON**:
   ```csharp
   var json = await response.Content.ReadAsStringAsync();
   ```
   - Gets the response body as a string
   - JSON looks like: `{"location": {...}, "current": {...}}`

5. **Parse JSON**:
   ```csharp
   var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json);
   ```
   - Converts JSON string into C# objects
   - `WeatherApiResponse` matches the JSON structure

6. **Create WeatherData**:
   ```csharp
   return new WeatherData
   {
       Temperature = weatherResponse.Current.TempF,  // Fahrenheit
       Condition = weatherResponse.Current.Condition.Text,  // "Sunny"
       Icon = weatherResponse.Current.Condition.Icon,  // Icon URL
       Location = weatherResponse.Location.Name  // "Lake Charles"
   };
   ```

#### Convenience Method: GetCampusWeatherAsync

```csharp
public async Task<WeatherData?> GetCampusWeatherAsync()
{
    return await GetCurrentWeatherAsync("Lake Charles");
}
```

**What this does**:
- Calls `GetCurrentWeatherAsync` with "Lake Charles" as the location
- This is the campus location, so we always get campus weather

---

### Step 2: Data Models

#### WeatherData (What We Return)

```csharp
public class WeatherData
{
    public double Temperature { get; set; }      // 75.0
    public string Condition { get; set; }        // "Sunny"
    public string Icon { get; set; }            // "//cdn.weatherapi.com/..."
    public string Location { get; set; }         // "Lake Charles"
    public DateTime LastUpdated { get; set; }    // When we fetched it
    
    // Helper properties for display
    public string FormattedTemperature => $"{Temperature:F0}F";  // "75F"
    public string FormattedWeather => $"{FormattedTemperature}  {Condition}";  // "75F  Sunny"
}
```

**Properties**:
- `Temperature` - The temperature in Fahrenheit
- `Condition` - Text description ("Sunny", "Cloudy", etc.)
- `Icon` - URL to weather icon image
- `Location` - City name
- `LastUpdated` - When we fetched the data
- `FormattedTemperature` - Ready-to-display temperature string
- `FormattedWeather` - Combined temperature and condition

#### WeatherApiResponse (What API Returns)

```csharp
internal class WeatherApiResponse
{
    [JsonPropertyName("location")]
    public WeatherLocation Location { get; set; }
    
    [JsonPropertyName("current")]
    public WeatherCurrent Current { get; set; }
}
```

**`[JsonPropertyName("location")]`**:
- Tells the JSON parser: "When you see 'location' in JSON, put it in this property"
- JSON uses lowercase, C# uses PascalCase, so we need this mapping

**The actual JSON looks like**:
```json
{
  "location": {
    "name": "Lake Charles",
    "region": "Louisiana",
    "country": "United States of America"
  },
  "current": {
    "temp_f": 75.0,
    "temp_c": 23.9,
    "condition": {
      "text": "Sunny",
      "icon": "//cdn.weatherapi.com/weather/64x64/day/113.png"
    }
  }
}
```

**Our C# classes match this structure**:
```csharp
internal class WeatherLocation
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("region")]
    public string Region { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
}

internal class WeatherCurrent
{
    [JsonPropertyName("temp_f")]
    public double TempF { get; set; }
    
    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }
    
    [JsonPropertyName("condition")]
    public WeatherCondition Condition { get; set; }
}

internal class WeatherCondition
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}
```

---

### Step 3: Using WeatherService in TopBar

**File**: `Dashboard/Controls/TopBar.xaml.cs`

#### Creating the Service

```csharp
public TopBar()
{
    InitializeComponent();
    _weatherService = new WeatherService();  // Create service
    Loaded += OnTopBarLoaded;  // When TopBar loads, start weather updates
}
```

#### Starting Weather Updates

```csharp
private void OnTopBarLoaded(object? sender, EventArgs e)
{
    StartWeatherUpdates();  // Start the timer
    UpdateInitialDisplay();  // Get weather immediately
}

private void StartWeatherUpdates()
{
    _weatherTimer = Dispatcher.CreateTimer();
    _weatherTimer.Interval = TimeSpan.FromMinutes(10);  // Update every 10 minutes
    _weatherTimer.IsRepeating = true;
    _weatherTimer.Tick += OnWeatherTick;  // Call this when timer ticks
    _weatherTimer.Start();
}
```

**What this does**:
- Creates a timer that fires every 10 minutes
- When timer fires, calls `OnWeatherTick`
- Also fetches weather immediately when TopBar loads

#### Updating Weather Display

```csharp
private async void OnWeatherTick(object? sender, EventArgs e)
{
    await UpdateWeatherAsync();  // Fetch and display weather
}

private async Task UpdateWeatherAsync()
{
    try
    {
        // Step 1: Get weather data
        var weatherData = await _weatherService.GetCampusWeatherAsync();
        
        if (weatherData != null)
        {
            // Step 2: Update temperature label
            WeatherLabel.Text = weatherData.FormattedWeather;  // "75F  Sunny"
            
            // Step 3: Update weather icon
            WeatherIcon.Source = ImageSource.FromUri(
                new Uri($"https:{weatherData.Icon}")
            );
        }
        else
        {
            // If failed, show loading message
            WeatherLabel.Text = "Weather Loading";
            WeatherIcon.Source = null;
        }
    }
    catch (Exception ex)
    {
        // Handle errors
        WeatherLabel.Text = "Weather Loading";
    }
}
```

**Breaking it down**:

1. **Fetch Weather**:
   ```csharp
   var weatherData = await _weatherService.GetCampusWeatherAsync();
   ```
   - Calls the service
   - Waits for response (async)
   - Returns `WeatherData` object or `null`

2. **Update Label**:
   ```csharp
   WeatherLabel.Text = weatherData.FormattedWeather;
   ```
   - Sets the text to "75F  Sunny"
   - `WeatherLabel` is defined in XAML

3. **Update Icon**:
   ```csharp
   WeatherIcon.Source = ImageSource.FromUri(new Uri($"https:{weatherData.Icon}"));
   ```
   - WeatherAPI returns icon as `//cdn.weatherapi.com/...`
   - We add `https:` to make it a full URL
   - MAUI loads the image automatically

---

## The XAML (UI) Side

**File**: `Dashboard/Controls/TopBar.xaml`

```xml
<Grid>
    <!-- Weather Icon -->
    <Image x:Name="WeatherIcon"
           WidthRequest="24"
           HeightRequest="24"
           VerticalOptions="Center" />
    
    <!-- Weather Text -->
    <Label x:Name="WeatherLabel"
           Text="Weather Loading"
           FontSize="14"
           VerticalOptions="Center" />
</Grid>
```

**What this does**:
- Creates an `Image` for the weather icon
- Creates a `Label` for the temperature/condition text
- `x:Name` allows C# code to reference these elements

---

## Error Handling

The code includes error handling:

```csharp
try
{
    var weatherData = await _weatherService.GetCampusWeatherAsync();
    // Use weatherData
}
catch (Exception ex)
{
    // If anything goes wrong:
    WeatherLabel.Text = "Weather Loading";  // Show friendly message
    WeatherIcon.Source = null;  // Hide icon
    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");  // Log error
}
```

**What can go wrong**:
- No internet connection
- API key invalid
- API service down
- Invalid location

**What we do**:
- Show "Weather Loading" message
- Don't crash the app
- Log error for debugging

---

## API Key Management

### Current Implementation (Simple)

```csharp
_apiKey = "f3ea5473f30a4e14b0c35729251808";  // Hardcoded
```

**Pros**: Simple, works immediately  
**Cons**: Not secure, key is visible in code

### Better Approach (Production)

1. **Use Environment Variables**:
   ```csharp
   _apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
   ```

2. **Use Configuration File**:
   - Create `appsettings.json`
   - Store key there
   - Load at runtime

3. **Use Secure Storage** (MAUI):
   ```csharp
   await SecureStorage.SetAsync("weather_api_key", apiKey);
   var key = await SecureStorage.GetAsync("weather_api_key");
   ```

---

## Getting Your Own API Key

1. Go to https://www.weatherapi.com/
2. Click "Sign Up" (free account)
3. Verify your email
4. Go to Dashboard
5. Copy your API key
6. Replace in `WeatherService.cs`:
   ```csharp
   _apiKey = "YOUR_API_KEY_HERE";
   ```

**Free Tier Limits**:
- 1 million calls per month
- Perfect for this project!

---

## Testing the Weather API

### Test in Code

```csharp
var service = new WeatherService();
var weather = await service.GetCampusWeatherAsync();

if (weather != null)
{
    Console.WriteLine($"Temperature: {weather.Temperature}F");
    Console.WriteLine($"Condition: {weather.Condition}");
    Console.WriteLine($"Location: {weather.Location}");
}
```

### Test in Browser

Open this URL (replace YOUR_KEY):
```
https://api.weatherapi.com/v1/current.json?key=YOUR_KEY&q=Lake%20Charles&aqi=no
```

You should see JSON response with weather data.

---

## How Often Does It Update?

- **Initial Load**: When TopBar first appears
- **Automatic Updates**: Every 10 minutes
- **Manual Refresh**: User can't manually refresh (automatic only)

**Why 10 minutes?**
- Weather doesn't change every second
- Reduces API calls (saves quota)
- Good balance between freshness and efficiency

---

## Summary

1. **WeatherService** calls WeatherAPI.com
2. **API returns JSON** with weather data
3. **Service parses JSON** into C# objects
4. **TopBar displays** temperature and icon
5. **Updates every 10 minutes** automatically

**Key Concepts**:
- **API** - Service you can ask for data
- **JSON** - Data format (like XML, but simpler)
- **Async/Await** - Wait for network requests
- **Timer** - Schedule periodic updates
- **Error Handling** - Gracefully handle failures

The weather feature is now fully explained! 🌤️
