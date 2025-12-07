# Interactive Map - Complete Guide

This document explains how the interactive map works, including the React/Next.js web application and how it's integrated into the .NET MAUI app.

---

## What is the Interactive Map?

The interactive map is a **web-based floor plan** of the department building. Users can:
- **Zoom in/out** to see details
- **Pan around** to explore different areas
- **Click on rooms** to see room information
- **Navigate** through different floors

**Why is it web-based?**
- Built with React (web technology) for rich interactivity
- Hosted on Vercel (free web hosting)
- Embedded in the MAUI app using WebView (like a mini browser)

---

## Architecture Overview

```
User opens DepartmentMapPage in MAUI app
    ↓
MAUI app loads WebView component
    ↓
WebView navigates to Vercel URL
    ↓
Vercel serves React/Next.js app
    ↓
React app displays interactive map
    ↓
User interacts with map (zoom, pan, click)
    ↓
React app handles interactions
    ↓
Room details displayed on click
```

---

## Part 1: The React/Next.js Web App

### What is React?

**React** is a JavaScript library for building user interfaces. Think of it as:
- **Lego blocks** - You build components and combine them
- **Smart HTML** - HTML that can change dynamically
- **Component-based** - Reusable pieces of UI

### What is Next.js?

**Next.js** is a framework built on top of React that provides:
- **Server-side rendering** - Faster page loads
- **API routes** - Backend functionality
- **Easy deployment** - Deploy to Vercel with one click

### Project Structure

**Location**: `Dashboard/ai-advisor-web/` (Note: This folder also contains AI Advisor code, but we're focusing on the map)

```
ai-advisor-web/
├── app/
│   ├── page.tsx          # Main page (AI Advisor interface)
│   ├── layout.tsx        # App layout
│   └── api/              # API routes
├── lib/                  # Utility functions
├── public/               # Static files (images, etc.)
├── package.json          # Dependencies
└── next.config.ts        # Next.js configuration
```

**Note**: The interactive map is a separate React app hosted on Vercel. The code might be in a different repository or folder. For this documentation, we'll explain how it works conceptually.

---

## How React Components Work

### Basic Component

```tsx
// RoomMarker.tsx
export function RoomMarker({ roomNumber, x, y, onClick }) {
  return (
    <div
      style={{
        position: 'absolute',
        left: `${x}px`,
        top: `${y}px`,
        width: '20px',
        height: '20px',
        backgroundColor: 'blue',
        cursor: 'pointer'
      }}
      onClick={() => onClick(roomNumber)}
    >
      {roomNumber}
    </div>
  );
}
```

**What this does**:
- Creates a clickable marker on the map
- Positioned at `(x, y)` coordinates
- Shows room number
- Calls `onClick` when clicked

### Using Components

```tsx
// MapPage.tsx
import { RoomMarker } from './RoomMarker';

export function MapPage() {
  const handleRoomClick = (roomNumber) => {
    alert(`Clicked room ${roomNumber}`);
  };

  return (
    <div>
      <img src="/floor-plan.png" alt="Floor Plan" />
      <RoomMarker 
        roomNumber="101" 
        x={100} 
        y={200} 
        onClick={handleRoomClick}
      />
      <RoomMarker 
        roomNumber="102" 
        x={150} 
        y={200} 
        onClick={handleRoomClick}
      />
    </div>
  );
}
```

**What this does**:
- Displays floor plan image
- Adds multiple room markers
- Each marker is clickable

---

## Interactive Features

### 1. Zoom Functionality

```tsx
import { useState } from 'react';

export function MapPage() {
  const [zoom, setZoom] = useState(1.0);  // Start at 100% zoom

  const handleZoomIn = () => {
    setZoom(prev => Math.min(prev + 0.1, 3.0));  // Max 300%
  };

  const handleZoomOut = () => {
    setZoom(prev => Math.max(prev - 0.1, 0.5));  // Min 50%
  };

  return (
    <div>
      <button onClick={handleZoomIn}>Zoom In</button>
      <button onClick={handleZoomOut}>Zoom Out</button>
      <div style={{ transform: `scale(${zoom})` }}>
        <img src="/floor-plan.png" alt="Floor Plan" />
      </div>
    </div>
  );
}
```

**How it works**:
- `useState` stores the current zoom level
- Buttons change zoom level
- CSS `transform: scale()` applies zoom
- Zoom is clamped between 0.5x and 3.0x

### 2. Pan Functionality

```tsx
import { useState } from 'react';

export function MapPage() {
  const [position, setPosition] = useState({ x: 0, y: 0 });
  const [isDragging, setIsDragging] = useState(false);
  const [dragStart, setDragStart] = useState({ x: 0, y: 0 });

  const handleMouseDown = (e) => {
    setIsDragging(true);
    setDragStart({ x: e.clientX - position.x, y: e.clientY - position.y });
  };

  const handleMouseMove = (e) => {
    if (isDragging) {
      setPosition({
        x: e.clientX - dragStart.x,
        y: e.clientY - dragStart.y
      });
    }
  };

  const handleMouseUp = () => {
    setIsDragging(false);
  };

  return (
    <div
      onMouseDown={handleMouseDown}
      onMouseMove={handleMouseMove}
      onMouseUp={handleMouseUp}
      style={{ cursor: isDragging ? 'grabbing' : 'grab' }}
    >
      <div style={{ transform: `translate(${position.x}px, ${position.y}px)` }}>
        <img src="/floor-plan.png" alt="Floor Plan" />
      </div>
    </div>
  );
}
```

**How it works**:
- Tracks mouse position when dragging
- Updates map position as mouse moves
- Uses CSS `transform: translate()` to move map
- Cursor changes to show dragging state

### 3. Room Click Detection

```tsx
export function MapPage() {
  const rooms = [
    { number: '101', x: 100, y: 200, name: 'Computer Lab' },
    { number: '102', x: 150, y: 200, name: 'Office' },
    // ... more rooms
  ];

  const [selectedRoom, setSelectedRoom] = useState(null);

  const handleRoomClick = (room) => {
    setSelectedRoom(room);
  };

  return (
    <div>
      <img src="/floor-plan.png" alt="Floor Plan" />
      {rooms.map(room => (
        <div
          key={room.number}
          style={{
            position: 'absolute',
            left: `${room.x}px`,
            top: `${room.y}px`,
            width: '30px',
            height: '30px',
            backgroundColor: selectedRoom?.number === room.number ? 'yellow' : 'blue',
            cursor: 'pointer'
          }}
          onClick={() => handleRoomClick(room)}
        >
          {room.number}
        </div>
      ))}
      {selectedRoom && (
        <div className="room-details">
          <h3>Room {selectedRoom.number}</h3>
          <p>{selectedRoom.name}</p>
        </div>
      )}
    </div>
  );
}
```

**How it works**:
- Rooms are defined with coordinates
- Each room is a clickable div
- Click updates `selectedRoom` state
- Room details panel shows selected room info

---

## Part 2: Integration with MAUI App

### DepartmentMapPage

**File**: `Dashboard/Pages/DepartmentMapPage.xaml` / `DepartmentMapPage.xaml.cs`

#### XAML (UI Definition)

```xml
<ContentPage>
    <Grid>
        <!-- Back Button -->
        <Button Text="← Back" 
                Clicked="OnBackButtonClicked" 
                HorizontalOptions="Start" />
        
        <!-- WebView for Interactive Map -->
        <WebView x:Name="InteractiveMapWebView"
                 Navigating="OnWebViewNavigating"
                 Navigated="OnWebViewNavigated" />
    </Grid>
</ContentPage>
```

**What this does**:
- Creates a WebView component (like a mini browser)
- WebView will display the React app
- Back button to return to maps selection

#### C# Code

```csharp
public partial class DepartmentMapPage : ContentPage
{
    private const string VercelMapUrl = "https://interactive-map-for-encs-lng-dashbo.vercel.app/";

    public DepartmentMapPage()
    {
        InitializeComponent();
        LoadVercelMap();  // Load the map when page opens
        EnableWebViewZoom();  // Enable zoom/pan
    }

    private void LoadVercelMap()
    {
        InteractiveMapWebView.Source = new UrlWebViewSource
        {
            Url = VercelMapUrl
        };
    }
}
```

**What this does**:
1. Sets the WebView source to the Vercel URL
2. WebView loads the React app
3. User can interact with the map

#### Platform-Specific Configuration

Different platforms need different WebView settings:

```csharp
private void EnableWebViewZoom()
{
    #if ANDROID
    InteractiveMapWebView.HandlerChanged += (s, e) =>
    {
        if (InteractiveMapWebView.Handler?.PlatformView is Android.Webkit.WebView androidWebView)
        {
            var settings = androidWebView.Settings;
            settings.SetSupportZoom(true);  // Enable zoom
            settings.BuiltInZoomControls = true;  // Show zoom controls
            settings.DisplayZoomControls = false;  // Hide default controls (use pinch)
            settings.JavaScriptEnabled = true;  // Enable JavaScript (required for React)
        }
    };
    #elif IOS
    // iOS configuration
    InteractiveMapWebView.HandlerChanged += (s, e) =>
    {
        if (InteractiveMapWebView.Handler?.PlatformView is WebKit.WKWebView iosWebView)
        {
            iosWebView.Configuration.AllowsInlineMediaPlayback = true;
        }
    };
    #elif WINDOWS
    // Windows WebView2 supports zoom by default
    #endif
}
```

**Why platform-specific?**
- Each platform has different WebView implementations
- Android uses Android.Webkit.WebView
- iOS uses WebKit.WKWebView
- Windows uses WebView2

---

## Part 3: Deployment to Vercel

### What is Vercel?

**Vercel** is a platform for hosting web applications. It's:
- **Free** for personal projects
- **Easy to deploy** - Connect GitHub, auto-deploys
- **Fast** - Global CDN (Content Delivery Network)
- **Perfect for React/Next.js** - Built by the Next.js team

### Setting Up Vercel

1. **Create Vercel Account**:
   - Go to https://vercel.com/
   - Sign up with GitHub

2. **Create New Project**:
   - Click "Add New Project"
   - Import your GitHub repository (or upload code)
   - Vercel detects Next.js automatically

3. **Configure Build Settings**:
   - **Framework Preset**: Next.js
   - **Build Command**: `npm run build` (automatic)
   - **Output Directory**: `.next` (automatic)
   - **Install Command**: `npm install` (automatic)

4. **Deploy**:
   - Click "Deploy"
   - Wait 1-2 minutes
   - Get your URL: `https://your-project.vercel.app`

### Vercel Configuration

**File**: `vercel.json` (if needed)

```json
{
  "version": 2,
  "buildCommand": "cd ai-advisor-web && npm install && npm run build",
  "outputDirectory": "ai-advisor-web/.next",
  "framework": "nextjs"
}
```

**What this does**:
- Tells Vercel how to build the project
- Specifies output directory
- Sets framework type

---

## How Everything Connects

### Complete Flow

```
1. User opens MAUI app
   ↓
2. User navigates to Maps → Department Map
   ↓
3. DepartmentMapPage loads
   ↓
4. WebView component created
   ↓
5. WebView navigates to: https://interactive-map.vercel.app
   ↓
6. Vercel serves React/Next.js app
   ↓
7. React app loads floor plan image
   ↓
8. React app renders room markers
   ↓
9. User interacts (zoom, pan, click)
   ↓
10. React app handles interactions
   ↓
11. Room details displayed
```

### Communication

**MAUI → React**: 
- MAUI loads React app in WebView
- No direct communication needed
- React app is independent

**React → MAUI**:
- React app runs in WebView
- Can use JavaScript to call MAUI (if needed):
  ```csharp
  // In MAUI
  InteractiveMapWebView.AddWebViewJavaScriptHandler("roomClicked", (roomId) =>
  {
      // Handle room click in MAUI
  });
  ```

---

## Room Data Structure

Rooms are typically defined as:

```typescript
interface Room {
  id: string;
  number: string;
  name: string;
  description: string;
  coordinates: {
    x: number;  // Pixel position on map
    y: number;
  };
  floor: number;
  type: 'office' | 'lab' | 'classroom' | 'common';
}
```

**Example**:
```typescript
const rooms: Room[] = [
  {
    id: '1',
    number: '101',
    name: 'Computer Lab',
    description: 'Main computer laboratory with 30 workstations',
    coordinates: { x: 100, y: 200 },
    floor: 1,
    type: 'lab'
  },
  // ... more rooms
];
```

---

## Styling the Map

### CSS for Map Container

```css
.map-container {
  position: relative;
  width: 100%;
  height: 100vh;
  overflow: hidden;
  background-color: #f0f0f0;
}

.floor-plan-image {
  width: 100%;
  height: auto;
  user-select: none;  /* Prevent text selection */
  pointer-events: auto;
}

.room-marker {
  position: absolute;
  width: 30px;
  height: 30px;
  background-color: #007bff;
  border: 2px solid white;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  font-size: 12px;
  font-weight: bold;
  transition: transform 0.2s;
}

.room-marker:hover {
  transform: scale(1.2);
  background-color: #0056b3;
}
```

---

## Testing

### Test React App Locally

1. Navigate to project folder:
   ```bash
   cd ai-advisor-web
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run development server:
   ```bash
   npm run dev
   ```

4. Open browser: http://localhost:3000

### Test in MAUI App

1. Build and run MAUI app
2. Navigate to Maps → Department Map
3. Verify map loads
4. Test zoom (pinch or mouse wheel)
5. Test pan (drag)
6. Test room clicks

---

## Troubleshooting

### Map Not Loading

**Problem**: WebView shows blank or error

**Solutions**:
1. Check internet connection
2. Verify Vercel URL is correct
3. Check Vercel deployment is active
4. Enable JavaScript in WebView (should be automatic)

### Zoom Not Working

**Problem**: Can't zoom in/out

**Solutions**:
1. Verify platform-specific code is correct
2. Check WebView settings enable zoom
3. Test on different platforms

### Room Clicks Not Working

**Problem**: Clicking rooms does nothing

**Solutions**:
1. Check JavaScript is enabled
2. Verify React app is fully loaded
3. Check browser console for errors (in WebView debugging)

---

## Summary

1. **Interactive map** is a React/Next.js web app
2. **Hosted on Vercel** for free
3. **Embedded in MAUI** using WebView
4. **Supports zoom, pan, and room clicks**
5. **Platform-specific configuration** for each OS

**Key Technologies**:
- **React** - UI framework
- **Next.js** - React framework with deployment
- **Vercel** - Hosting platform
- **WebView** - Embed web content in native app
- **CSS** - Styling

The interactive map provides a rich, web-based experience within the native app! 🗺️
