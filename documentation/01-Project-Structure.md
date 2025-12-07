# Project Directory Structure

This document explains every folder and important file in the project. Think of it as a map of the codebase.

---

## Root Directory Overview

```
Dashboard/
├── Dashboard/              # Main .NET MAUI application
├── Dashboard.sln          # Visual Studio solution file
├── documentation/          # This documentation folder
├── vercel.json            # Configuration for Vercel deployment
├── supabase_sample_data.sql  # Sample data for database
└── README.md              # Project overview
```

---

## Main Application Folder: `Dashboard/`

This is where all the main app code lives.

### Core Application Files

#### `App.xaml` and `App.xaml.cs`
- **What they do**: The entry point of the application - this is where the app starts
- **App.xaml**: Defines global resources (colors, styles, fonts)
- **App.xaml.cs**: Contains the `App` class that initializes the app
- **Think of it like**: The main door to a house - everything starts here

#### `AppShell.xaml` and `AppShell.xaml.cs`
- **What they do**: Defines the navigation structure (which pages exist and how to navigate)
- **AppShell.xaml**: Lists all pages and their routes (like a table of contents)
- **AppShell.xaml.cs**: Code that handles navigation logic
- **Think of it like**: A GPS system that knows all the destinations

#### `MauiProgram.cs`
- **What it does**: Registers all services and dependencies (like a registration desk)
- **Services registered here**: WeatherService, FacultyService, HomePageImageService, etc.
- **Why it's important**: Tells the app which services are available and how to create them
- **Think of it like**: A phone book that lists all available services

#### `Dashboard.csproj`
- **What it does**: Project configuration file - defines what the project needs
- **Contains**: 
  - Target platforms (Windows, Android, iOS, macOS)
  - NuGet packages (external libraries)
  - App metadata (name, version, icon)
- **Think of it like**: A shopping list for the project

---

## Pages Folder: `Dashboard/Pages/`

Each `.xaml` file defines the UI (what you see), and each `.xaml.cs` file contains the code (what happens when you interact).

### Main Pages

#### `HomePage.xaml` / `HomePage.xaml.cs`
- **Purpose**: The main dashboard screen
- **Features**:
  - Slideshow of images from Supabase
  - Upcoming events sidebar
  - Announcements section
  - Quick access cards (Maps, Contact, AI Advisor, etc.)
- **How it works**: 
  - Loads images from `HomePageImageService`
  - Auto-rotates images every 5 seconds
  - Handles taps to navigate to other pages

#### `CampusMapPage.xaml` / `CampusMapPage.xaml.cs`
- **Purpose**: Shows the full campus map
- **Features**:
  - Displays campus map image
  - Shows "You are here" marker
  - Supports pinch-to-zoom
- **How it works**: 
  - Loads `campus_map.png` from resources
  - Adds a pulsing marker at a fixed location
  - Handles zoom gestures

#### `DepartmentMapPage.xaml` / `DepartmentMapPage.xaml.cs`
- **Purpose**: Interactive department floor plan
- **Features**:
  - Embeds a web-based interactive map (hosted on Vercel)
  - Supports zoom and pan
  - Shows room details on click
- **How it works**: 
  - Uses a WebView to display the React-based map
  - Loads from: `https://interactive-map-for-encs-lng-dashbo.vercel.app/`
  - Enables touch gestures for interaction

#### `AiAdvisorPage.xaml` / `AiAdvisorPage.xaml.cs`
- **Purpose**: Displays AI advisor conversations
- **Features**:
  - Shows chat transcript from Flask API
  - Displays QR code for mobile access
  - Auto-refreshes when new messages arrive
- **How it works**: 
  - Polls the Flask API every second for updates
  - Uses `AiAdvisorMirrorService` to fetch messages
  - Displays messages in a chat-like interface

#### `StudentClubsPage.xaml` / `StudentClubsPage.xaml.cs`
- **Purpose**: Lists all student clubs
- **Features**:
  - Grid of club cards
  - Search functionality
  - Tap to see club details
- **How it works**: 
  - Fetches clubs from Supabase via `StudentClubService`
  - Displays in a scrollable grid

#### `FacultyDirectoryPage.xaml` / `FacultyDirectoryPage.xaml.cs`
- **Purpose**: Shows all faculty members
- **Features**:
  - Grid of faculty cards with photos
  - Search and filter
  - Tap for detailed information
- **How it works**: 
  - Loads faculty data from Supabase via `FacultyService`

#### `AcademicCataloguePage.xaml` / `AcademicCataloguePage.xaml.cs`
- **Purpose**: Lists academic programs
- **Features**:
  - Program cards with images
  - Navigation to program details
- **How it works**: 
  - Uses `AcademicProgramService` to fetch from Supabase

#### Other Pages:
- `AllEventsPage` - Full list of events
- `AllAnnouncementsPage` - Full list of announcements
- `ContactPage` - Department contact information
- `LabsPage` - Laboratory information
- `SponsorsAndDonorsPage` - Sponsor information
- `EWeekPage`, `EWeek2024Page`, `EWeek2025Page` - Engineering Week galleries

---

## Services Folder: `Dashboard/Services/`

Services handle communication with external APIs and data processing.

### Key Services

#### `WeatherService.cs`
- **Purpose**: Fetches weather data from WeatherAPI.com
- **Methods**:
  - `GetCurrentWeatherAsync(location)` - Gets weather for any location
  - `GetCampusWeatherAsync()` - Gets weather for Lake Charles (campus)
- **How it works**: 
  - Makes HTTP request to WeatherAPI.com
  - Parses JSON response
  - Returns `WeatherData` object

#### `SupabaseService.cs`
- **Purpose**: Stores Supabase connection credentials
- **Contains**:
  - `SupabaseUrl` - The base URL of your Supabase project
  - `SupabaseAnonKey` - The public API key
- **Why it exists**: Centralized configuration - other services reference this

#### `HomePageImageService.cs`
- **Purpose**: Fetches homepage slideshow images
- **Methods**:
  - `GetHomePageImagesAsync()` - Gets all active images
  - `GetFeaturedHomePageImagesAsync(limit)` - Gets limited images
- **How it works**: 
  - Queries Supabase `homepage_images` table
  - Falls back to hardcoded URLs if database fails
  - Returns list of `HomePageImage` objects

#### `FacultyService.cs`
- **Purpose**: Fetches faculty directory data
- **How it works**: Queries Supabase `faculty` table

#### `StudentClubService.cs`
- **Purpose**: Fetches student club information
- **How it works**: Queries Supabase `clubs` table

#### `FloorPlanService.cs`
- **Purpose**: Manages floor plan and room data
- **How it works**: Queries Supabase for room information

#### `AiAdvisorMirrorService.cs`
- **Purpose**: Fetches AI advisor chat transcript from Flask API
- **Methods**:
  - `GetTranscriptAsync()` - Gets all messages
  - `GetLastUpdatedAsync()` - Checks if new messages exist
  - `ClearTranscriptAsync()` - Clears the chat
- **How it works**: 
  - Makes HTTP GET requests to Flask API endpoints
  - Parses JSON responses into `AiAdvisorMessage` objects

#### `AiAdvisorConfig.cs`
- **Purpose**: Stores AI Advisor API endpoints
- **Contains**:
  - `ChatUrl` - URL for the web chat interface
  - `TranscriptEndpoint` - API endpoint for messages
  - `LastUpdatedEndpoint` - API endpoint for update timestamp
  - `ClearTranscriptEndpoint` - API endpoint to clear chat

---

## Models Folder: `Dashboard/Models/`

These are data structures - think of them as templates for information.

### Key Models

#### `WeatherData.cs`
- **Purpose**: Represents weather information
- **Properties**: Temperature, Condition, Icon, Location, LastUpdated

#### `HomePageImage.cs`
- **Purpose**: Represents a homepage slideshow image
- **Properties**: Id, Title, Description, ImageUrl, DisplayOrder, IsActive

#### `Faculty.cs`
- **Purpose**: Represents a faculty member
- **Properties**: Name, Email, Phone, Office, Department, PhotoUrl, etc.

#### `StudentClub.cs`
- **Purpose**: Represents a student organization
- **Properties**: Name, Description, MeetingTime, Location, ContactInfo, etc.

#### `AiAdvisorMessage.cs`
- **Purpose**: Represents a chat message
- **Properties**: Role (user/assistant), Content, Timestamp

#### `Room.cs`
- **Purpose**: Represents a room on the floor plan
- **Properties**: RoomNumber, Name, Description, Coordinates, etc.

---

## Controls Folder: `Dashboard/Controls/`

Reusable UI components that appear on multiple pages.

### `TopBar.xaml` / `TopBar.xaml.cs`
- **Purpose**: Global navigation bar (appears on every page)
- **Features**:
  - Hamburger menu (opens navigation drawer)
  - Home button
  - Search bar with global search
  - Weather display (temperature and icon)
  - Clock (updates every second)
- **How it works**: 
  - Uses `WeatherService` to fetch weather every 10 minutes
  - Uses `IDispatcherTimer` to update clock every second
  - Implements search functionality with keyword matching

### `RoomTooltip.xaml` / `RoomTooltip.xaml.cs`
- **Purpose**: Tooltip that appears when hovering over rooms on the map
- **Features**: Shows room name and description

---

## Converters Folder: `Dashboard/Converters/`

These convert data from one format to another for display.

#### `ClubImageConverter.cs`
- **Purpose**: Converts club data to an image source
- **Use case**: When displaying club logos

#### `FirstImageConverter.cs`
- **Purpose**: Extracts the first image from a collection
- **Use case**: When showing a preview image

---

## Platforms Folder: `Dashboard/Platforms/`

Platform-specific code for different operating systems.

### `Platforms/Windows/`
- **Files**: `App.xaml`, `App.xaml.cs`, `Package.appxmanifest`
- **Purpose**: Windows-specific configuration
- **Contains**: App manifest, certificate for signing

### `Platforms/Android/`
- **Files**: `MainActivity.cs`, `MainApplication.cs`, `AndroidManifest.xml`
- **Purpose**: Android-specific configuration
- **Contains**: App permissions, activity lifecycle

### `Platforms/iOS/`
- **Files**: `AppDelegate.cs`, `Info.plist`
- **Purpose**: iOS-specific configuration
- **Contains**: App permissions, iOS settings

### `Platforms/MacCatalyst/`
- **Purpose**: macOS app configuration

---

## Resources Folder: `Dashboard/Resources/`

All images, fonts, and styling files.

### `Resources/Images/`
- **Contains**: App icons, logos, UI images
- **Examples**: `mcneeselogo.png`, `search.png`, `campus_map.png`

### `Resources/Fonts/`
- **Contains**: Custom fonts
- **Examples**: `OpenSans-Regular.ttf`, `OpenSans-Semibold.ttf`

### `Resources/Styles/`
- **Contains**: Global styling
- **Files**: 
  - `Colors.xaml` - Color definitions
  - `Styles.xaml` - Reusable styles

### `Resources/AppIcon/`
- **Contains**: App icon files (SVG format)

### `Resources/Splash/`
- **Contains**: Splash screen image

---

## Backend Folder: `Dashboard/Backend/`

Database setup scripts and documentation.

### `supabase_schema.sql`
- **Purpose**: SQL script to create all database tables
- **Contains**: CREATE TABLE statements for:
  - announcements
  - clubs
  - events
  - faculty
  - homepage_images
  - etc.

### `SUPABASE_SETUP_GUIDE.md`
- **Purpose**: Instructions for setting up Supabase

---

## AI Advisor Components

### `Dashboard/AiAdvisorApi/`
- **Purpose**: Flask API for AI Advisor
- **Main file**: `app.py`
- **What it does**: 
  - Receives chat messages
  - Sends to OpenAI API
  - Stores transcript in memory
  - Returns responses

### `Dashboard/ai-advisor-web/`
- **Purpose**: Next.js web interface for AI Advisor
- **Structure**:
  - `app/` - Next.js app directory
    - `page.tsx` - Main chat interface
    - `api/chat/route.ts` - API route for chat
    - `api/transcript/route.ts` - API route for transcript
  - `lib/` - Utility functions
    - `model.ts` - OpenAI client setup
    - `vectorStore.ts` - Vector search for RAG
    - `ingest.ts` - Document ingestion

---

## Configuration Files

### `vercel.json`
- **Purpose**: Configuration for Vercel deployment
- **Contains**: Build command, output directory, framework type

### `.gitignore`
- **Purpose**: Tells Git which files to ignore
- **Contains**: Build outputs, temporary files, API keys

### `Dashboard.sln`
- **Purpose**: Visual Studio solution file
- **Contains**: References to all projects in the solution

---

## How Everything Connects

```
User opens app
    ↓
App.xaml.cs starts
    ↓
MauiProgram.cs registers services
    ↓
AppShell.xaml defines navigation
    ↓
HomePage loads
    ↓
HomePage uses HomePageImageService
    ↓
HomePageImageService queries Supabase
    ↓
SupabaseService provides credentials
    ↓
Data displayed on screen
```

**Example Flow: Weather Display**

```
TopBar loads
    ↓
TopBar creates WeatherService
    ↓
WeatherService calls WeatherAPI.com
    ↓
WeatherAPI.com returns JSON
    ↓
WeatherService parses JSON
    ↓
TopBar displays temperature and icon
```

**Example Flow: AI Advisor**

```
User opens AiAdvisorPage
    ↓
Page creates AiAdvisorMirrorService
    ↓
Service polls Flask API every second
    ↓
Flask API returns transcript
    ↓
Page displays messages
    ↓
User scans QR code on phone
    ↓
Phone opens Next.js web app
    ↓
Web app sends question to Flask API
    ↓
Flask API sends to OpenAI
    ↓
OpenAI returns answer
    ↓
Flask API stores in transcript
    ↓
MAUI app polls and shows new message
```

---

## File Naming Conventions

- **`.xaml`** = UI definition (XML format)
- **`.xaml.cs`** = Code-behind (C# code)
- **`.cs`** = C# source code
- **`.csproj`** = Project configuration
- **`.sql`** = Database script
- **`.json`** = Configuration/data file
- **`.ts`** / **`.tsx`** = TypeScript code (React)
- **`.py`** = Python code (Flask)

---

## Summary

- **Pages/** = Screens users see
- **Services/** = Code that talks to APIs/databases
- **Models/** = Data structures
- **Controls/** = Reusable UI components
- **Resources/** = Images, fonts, styles
- **Platforms/** = Platform-specific code
- **Backend/** = Database scripts

Each folder has a specific purpose, making the codebase organized and easy to navigate!
