// Code written for Interactive Department Map Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;
using System.Text;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the DepartmentMapPage xaml file.
/// Provides interactive floor plan navigation with room details and current location.
/// </summary>
public partial class DepartmentMapPage : ContentPage
{
    private readonly FloorPlanService _floorPlanService;
    private int _currentFloor = 1;
    private double _imageWidth = 0;
    private double _imageHeight = 0;
    private Dictionary<string, View> _roomZones = new Dictionary<string, View>();
    private double _currentZoom = 1.0;

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the DepartmentMapPage.
    /// Sets up the interactive map with floor selection and room interactions.
    /// </summary>
    public DepartmentMapPage(FloorPlanService floorPlanService)
    {
        InitializeComponent();
        _floorPlanService = floorPlanService;
        
        // Load initial floor
        LoadFloor(1);
        
        // Set up WebView size tracking
        FloorPlanWebView.SizeChanged += OnWebViewSizeChanged;
        
        // Set up zoom/pan gestures
        SetupZoomPanGestures();
    }
    #endregion

    #region Navigation
    /// <summary>
    /// Handles back button click to navigate back to Maps selection page.
    /// </summary>
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Maps");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating back to maps: {ex.Message}");
        }
    }
    #endregion

    #region Floor Management
    /// <summary>
    /// Handles floor selection button clicks.
    /// </summary>
    private void OnFloorSelected(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string floorStr)
        {
            if (int.TryParse(floorStr, out int floorNumber))
            {
                LoadFloor(floorNumber);
            }
        }
    }

    /// <summary>
    /// Loads and displays the specified floor plan.
    /// </summary>
    /// <param name="floorNumber">The floor number to load (1, 2, or 3).</param>
    private async void LoadFloor(int floorNumber)
    {
        _currentFloor = floorNumber;
        
        // Update floor button styles
        UpdateFloorButtonStyles();
        
        // Load floor plan SVG
        var floorPlan = _floorPlanService.GetFloorPlan(floorNumber);
        if (floorPlan != null)
        {
            // Clear existing room zones
            ClearRoomZones();
            LocationOverlay.Children.Clear();
            
            // Load SVG file as HTML content in WebView
            await LoadSvgInWebView(floorPlan.SvgPath);
            
            // Wait a bit for WebView to render, then add interactive zones
            await Task.Delay(500);
            AddRoomZones(floorPlan);
            AddCurrentLocationMarker();
        }
    }

    /// <summary>
    /// Loads an SVG file into the WebView as HTML content.
    /// This avoids bitmap conversion and memory issues with large SVGs.
    /// </summary>
    private async Task LoadSvgInWebView(string svgFileName)
    {
        try
        {
            // Add .svg extension if not present
            var fullFileName = svgFileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase) 
                ? svgFileName 
                : $"{svgFileName}.svg";
            
            // Read the SVG file from resources (loaded as MauiAsset)
            using var stream = await FileSystem.OpenAppPackageFileAsync(fullFileName);
            using var reader = new StreamReader(stream);
            var svgContent = await reader.ReadToEndAsync();
            
            // Create HTML wrapper for the SVG
            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=5.0, user-scalable=yes"">
    <style>
        body {{
            margin: 0;
            padding: 0;
            overflow: auto;
            background-color: #F5F5F5;
        }}
        svg {{
            width: 100%;
            height: auto;
            display: block;
        }}
    </style>
</head>
<body>
    {svgContent}
</body>
</html>";
            
            // Load HTML into WebView
            var htmlSource = new HtmlWebViewSource { Html = html };
            FloorPlanWebView.Source = htmlSource;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading SVG: {ex.Message}");
            // Fallback: show error message
            var errorHtml = $@"
<!DOCTYPE html>
<html>
<body style=""padding: 20px; font-family: Arial;"">
    <h3>Error loading floor plan</h3>
    <p>Could not load {svgFileName}</p>
    <p>{ex.Message}</p>
</body>
</html>";
            FloorPlanWebView.Source = new HtmlWebViewSource { Html = errorHtml };
        }
    }

    /// <summary>
    /// Updates the visual style of floor selection buttons to highlight the current floor.
    /// </summary>
    private void UpdateFloorButtonStyles()
    {
        // McNeese colors: Sunflower Gold for selected, Dark Blue for unselected
        Floor1Button.BackgroundColor = _currentFloor == 1 ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor1Button.TextColor = _currentFloor == 1 ? Color.FromArgb("#003087") : Colors.White;
        Floor2Button.BackgroundColor = _currentFloor == 2 ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor2Button.TextColor = _currentFloor == 2 ? Color.FromArgb("#003087") : Colors.White;
        Floor3Button.BackgroundColor = _currentFloor == 3 ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor3Button.TextColor = _currentFloor == 3 ? Color.FromArgb("#003087") : Colors.White;
    }
    #endregion

    #region WebView Size Tracking
    /// <summary>
    /// Handles WebView size changes to recalculate room zone positions.
    /// </summary>
    private void OnWebViewSizeChanged(object? sender, EventArgs e)
    {
        if (FloorPlanWebView.Width > 0 && FloorPlanWebView.Height > 0)
        {
            _imageWidth = FloorPlanWebView.Width;
            _imageHeight = FloorPlanWebView.Height;
            
            // Recalculate room zones when WebView size changes
            var floorPlan = _floorPlanService.GetFloorPlan(_currentFloor);
            if (floorPlan != null)
            {
                ClearRoomZones();
                AddRoomZones(floorPlan);
                AddCurrentLocationMarker();
            }
        }
    }
    #endregion

    #region Room Interaction Zones
    /// <summary>
    /// Adds interactive tap zones for all rooms on the current floor.
    /// </summary>
    private void AddRoomZones(FloorPlan floorPlan)
    {
        if (_imageWidth == 0 || _imageHeight == 0) return;
        
        foreach (var room in floorPlan.Rooms)
        {
            // Calculate absolute positions based on relative coordinates
            var x = room.X * _imageWidth;
            var y = room.Y * _imageHeight;
            var width = room.Width * _imageWidth;
            var height = room.Height * _imageHeight;
            
            // Create invisible tap zone
            var tapZone = new BoxView
            {
                BackgroundColor = Colors.Transparent,
                Opacity = 0.01, // Slightly visible for debugging, can be set to 0
                WidthRequest = width,
                HeightRequest = height
            };
            
            // Add tap gesture
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnRoomTapped(room);
            tapZone.GestureRecognizers.Add(tapGesture);
            
            // Add long press gesture (for hover-like behavior on mobile)
            var longPressGesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            // Note: MAUI doesn't have built-in long press, so we'll use tap for now
            // Can be enhanced with custom gesture recognizer if needed
            
            // Position the zone
            AbsoluteLayout.SetLayoutBounds(tapZone, new Rect(x, y, width, height));
            RoomZonesOverlay.Children.Add(tapZone);
            
            _roomZones[room.RoomNumber] = tapZone;
        }
    }

    /// <summary>
    /// Clears all room interaction zones from the overlay.
    /// </summary>
    private void ClearRoomZones()
    {
        RoomZonesOverlay.Children.Clear();
        _roomZones.Clear();
    }
    #endregion

    #region Room Interaction Handlers
    /// <summary>
    /// Handles room tap to show room details.
    /// </summary>
    private async void OnRoomTapped(Room room)
    {
        try
        {
            await ShowRoomDetails(room);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error showing room details: {ex.Message}");
        }
    }

    /// <summary>
    /// Displays room details in a modal popup.
    /// </summary>
    private async Task ShowRoomDetails(Room room)
    {
        var detailsPage = new ContentPage
        {
            Title = $"Room {room.RoomNumber}",
            BackgroundColor = Color.FromArgb("#1C2028"),
            Padding = new Thickness(20)
        };

        var content = new ScrollView();
        var stackLayout = new VerticalStackLayout
        {
            Spacing = 15
        };

        // Room Number and Name
        stackLayout.Children.Add(new Label
        {
            Text = room.RoomNumber,
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        });

        stackLayout.Children.Add(new Label
        {
            Text = room.RoomName,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#4CAF50")
        });

        // Room Type
        stackLayout.Children.Add(new Label
        {
            Text = $"Type: {room.RoomType}",
            FontSize = 18,
            TextColor = Color.FromArgb("#CCCCCC")
        });

        // Square Footage
        stackLayout.Children.Add(new Label
        {
            Text = $"Size: {room.SquareFootage} sq ft",
            FontSize = 16,
            TextColor = Color.FromArgb("#CCCCCC")
        });

        // Description
        if (!string.IsNullOrEmpty(room.Description))
        {
            stackLayout.Children.Add(new Label
            {
                Text = "Description:",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                Margin = new Thickness(0, 10, 0, 5)
            });

            stackLayout.Children.Add(new Label
            {
                Text = room.Description,
                FontSize = 16,
                TextColor = Color.FromArgb("#CCCCCC"),
                LineBreakMode = LineBreakMode.WordWrap
            });
        }

        // Close Button
        var closeButton = new Button
        {
            Text = "Close",
            BackgroundColor = Color.FromArgb("#4CAF50"),
            TextColor = Colors.White,
            FontSize = 18,
            CornerRadius = 8,
            Margin = new Thickness(0, 20, 0, 0),
            HeightRequest = 50
        };
        closeButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        stackLayout.Children.Add(closeButton);

        content.Content = stackLayout;
        detailsPage.Content = content;

        await Navigation.PushModalAsync(detailsPage);
    }
    #endregion

    #region Current Location Marker
    /// <summary>
    /// Adds the current location indicator to the map.
    /// </summary>
    private void AddCurrentLocationMarker()
    {
        if (_imageWidth == 0 || _imageHeight == 0) return;
        
        var location = _floorPlanService.GetCurrentLocation();
        
        // Only show marker if on the same floor
        if (location.FloorNumber != _currentFloor) return;
        
        // Calculate absolute position
        var x = location.X * _imageWidth;
        var y = location.Y * _imageHeight;
        
        // Create pulsing location marker (using BoxView with CornerRadius for circular shape)
        var marker = new BoxView
        {
            BackgroundColor = Color.FromArgb("#FF0000"),
            WidthRequest = 20,
            HeightRequest = 20,
            CornerRadius = 10 // Makes it circular
        };
        
        // Position the marker
        AbsoluteLayout.SetLayoutBounds(marker, new Rect(x - 10, y - 10, 20, 20));
        LocationOverlay.Children.Add(marker);
        
        // Add label
        var label = new Label
        {
            Text = "You are here",
            FontSize = 12,
            TextColor = Colors.Red,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.White,
            Padding = new Thickness(5, 2, 5, 2)
        };
        AbsoluteLayout.SetLayoutBounds(label, new Rect(x - 40, y - 35, 80, 20));
        LocationOverlay.Children.Add(label);
        
        // Start pulsing animation
        StartLocationPulseAnimation(marker);
    }

    /// <summary>
    /// Starts a pulsing animation for the location marker.
    /// </summary>
    private void StartLocationPulseAnimation(BoxView marker)
    {
        var animation = new Animation();
        
        // Scale animation
        var scaleUp = new Animation(v => marker.Scale = v, 1.0, 1.5);
        var scaleDown = new Animation(v => marker.Scale = v, 1.5, 1.0);
        
        animation.Add(0, 0.5, scaleUp);
        animation.Add(0.5, 1.0, scaleDown);
        
        animation.Commit(marker, "Pulse", 16, 1000, Easing.Linear, (v, c) => marker.Scale = 1.0, () => true);
    }
    #endregion

    #region Zoom and Pan Gestures
    /// <summary>
    /// Sets up pinch-to-zoom and pan gestures for the map.
    /// ScrollView already provides basic zoom, but we enhance it here.
    /// </summary>
    private void SetupZoomPanGestures()
    {
        // Pinch gesture for zoom
        var pinchGesture = new PinchGestureRecognizer();
        pinchGesture.PinchUpdated += OnPinchUpdated;
        MapContainer.GestureRecognizers.Add(pinchGesture);
        
        // Pan gesture (ScrollView handles this, but we can add custom handling if needed)
        // The ScrollView with ZoomMode="Enabled" already provides pan functionality
    }

    /// <summary>
    /// Handles pinch gesture for zooming the map.
    /// </summary>
    private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        if (e.Status == GestureStatus.Running)
        {
            // Calculate zoom scale
            _currentZoom = Math.Max(1.0, Math.Min(5.0, _currentZoom * e.Scale));
            
            // Update ScrollView zoom programmatically if needed
            // Note: ScrollView handles zoom automatically, but we track it for room zone recalculation
        }
        else if (e.Status == GestureStatus.Completed)
        {
            // Recalculate room zones after zoom completes
            var floorPlan = _floorPlanService.GetFloorPlan(_currentFloor);
            if (floorPlan != null && _imageWidth > 0 && _imageHeight > 0)
            {
                // Room zones will automatically adjust with the image size
                // The AbsoluteLayout positions are relative to the scaled image
            }
        }
    }
    #endregion
}

