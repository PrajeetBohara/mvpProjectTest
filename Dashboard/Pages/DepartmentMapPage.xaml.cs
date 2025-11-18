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
    private string _currentSelection = "1"; // "ETL" or "1", "2", "3"
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
        
        // Load initial floor (Floor 1)
        LoadFloor(1);
        
        // Set up Image size tracking
        FloorPlanImage.SizeChanged += OnImageSizeChanged;
        
        // Initialize zoom controls
        InitializeZoomControls();
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
            if (floorStr == "ETL")
            {
                LoadETL();
            }
            else if (int.TryParse(floorStr, out int floorNumber))
            {
                LoadFloor(floorNumber);
            }
        }
    }

    /// <summary>
    /// Loads and displays the ETL (Electrical Technology Lab) map.
    /// </summary>
    private async void LoadETL()
    {
        _currentSelection = "ETL";
        _currentFloor = 0; // Use 0 to represent ETL
        
        // Update button styles
        UpdateFloorButtonStyles();
        
        // Clear existing room zones
        ClearRoomZones();
        LocationOverlay.Children.Clear();
        
        // Load ETL JPG image
        LoadImage("etl.jpg");
        
        // Wait a bit for image to load
        await Task.Delay(300);
        
        // ETL can have rooms too if needed - for now just load the map
        var etlFloorPlan = _floorPlanService.GetETLFloorPlan();
        if (etlFloorPlan != null)
        {
            AddRoomZones(etlFloorPlan);
            AddCurrentLocationMarker();
        }
    }

    /// <summary>
    /// Loads and displays the specified floor plan.
    /// </summary>
    /// <param name="floorNumber">The floor number to load (1, 2, or 3).</param>
    private async void LoadFloor(int floorNumber)
    {
        _currentFloor = floorNumber;
        _currentSelection = floorNumber.ToString();
        
        // Update floor button styles
        UpdateFloorButtonStyles();
        
        // Load floor plan SVG
        var floorPlan = _floorPlanService.GetFloorPlan(floorNumber);
        if (floorPlan != null)
        {
            // Clear existing room zones
            ClearRoomZones();
            LocationOverlay.Children.Clear();
            
            // Load JPG image
            LoadImage($"{floorPlan.SvgPath}.jpg");
            
            // Wait a bit for image to load, then add interactive zones
            await Task.Delay(300);
            AddRoomZones(floorPlan);
            AddCurrentLocationMarker();
        }
    }

    /// <summary>
    /// Loads a JPG image file into the Image control.
    /// </summary>
    private void LoadImage(string imageFileName)
    {
        try
        {
            // Reset zoom when loading new image
            SetZoom(1.0);
            
            // Set the image source - MAUI will automatically load from Resources/Images
            FloorPlanImage.Source = imageFileName;
            
            System.Diagnostics.Debug.WriteLine($"Loading image: {imageFileName}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
            // Fallback: show placeholder or error
            FloorPlanImage.Source = "mcneeselogo.png";
        }
    }

    /// <summary>
    /// Updates the visual style of floor selection buttons to highlight the current selection.
    /// </summary>
    private void UpdateFloorButtonStyles()
    {
        // McNeese colors: Sunflower Gold for selected, Dark Blue for unselected
        bool isETLSelected = _currentSelection == "ETL";
        bool isFloor1Selected = _currentSelection == "1";
        bool isFloor2Selected = _currentSelection == "2";
        bool isFloor3Selected = _currentSelection == "3";
        
        ETLButton.BackgroundColor = isETLSelected ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        ETLButton.TextColor = isETLSelected ? Color.FromArgb("#003087") : Colors.White;
        Floor1Button.BackgroundColor = isFloor1Selected ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor1Button.TextColor = isFloor1Selected ? Color.FromArgb("#003087") : Colors.White;
        Floor2Button.BackgroundColor = isFloor2Selected ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor2Button.TextColor = isFloor2Selected ? Color.FromArgb("#003087") : Colors.White;
        Floor3Button.BackgroundColor = isFloor3Selected ? Color.FromArgb("#FFD204") : Color.FromArgb("#002a54");
        Floor3Button.TextColor = isFloor3Selected ? Color.FromArgb("#003087") : Colors.White;
    }
    #endregion

    #region Image Size Tracking
    /// <summary>
    /// Handles Image size changes to recalculate room zone positions and update container size.
    /// </summary>
    private void OnImageSizeChanged(object? sender, EventArgs e)
    {
        if (FloorPlanImage.Width > 0 && FloorPlanImage.Height > 0)
        {
            _imageWidth = FloorPlanImage.Width;
            _imageHeight = FloorPlanImage.Height;
            
            // Update container size for scrolling (allows panning when zoomed)
            MapContainer.MinimumWidthRequest = FloorPlanImage.Width * _currentZoom;
            MapContainer.MinimumHeightRequest = FloorPlanImage.Height * _currentZoom;
            
            // Recalculate room zones when Image size changes
            FloorPlan? floorPlan = null;
            if (_currentSelection == "ETL")
            {
                floorPlan = _floorPlanService.GetETLFloorPlan();
            }
            else
            {
                floorPlan = _floorPlanService.GetFloorPlan(_currentFloor);
            }
            
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
            BackgroundColor = Color.FromArgb("#003087"),
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
            TextColor = Color.FromArgb("#FFD204") // McNeese Sunflower Gold
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
            BackgroundColor = Color.FromArgb("#FFD204"), // McNeese Sunflower Gold
            TextColor = Color.FromArgb("#003087"), // Royal Blue text
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
    /// Adds the current location indicator to the map for the current floor/ETL.
    /// </summary>
    private void AddCurrentLocationMarker()
    {
        if (_imageWidth == 0 || _imageHeight == 0) return;
        
        // Get location for the current floor/ETL being viewed
        BuildingLocation? location = null;
        if (_currentSelection == "ETL")
        {
            location = _floorPlanService.GetLocationForFloor(0); // ETL is floor 0
        }
        else
        {
            location = _floorPlanService.GetLocationForFloor(_currentFloor);
        }
        
        // If no location found for this floor, don't show marker
        if (location == null) return;
        
        // Calculate absolute position
        var x = location.X * _imageWidth;
        var y = location.Y * _imageHeight;
        
        // Create pulsing location marker (using BoxView with CornerRadius for circular shape)
        // Using McNeese Orange Gold to differentiate from campus map (which uses Sunflower Gold)
        var marker = new BoxView
        {
            BackgroundColor = Color.FromArgb("#f2b32e"), // McNeese Orange Gold
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
            TextColor = Color.FromArgb("#f2b32e"), // McNeese Orange Gold
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

    #region Zoom Controls
    private const double MinZoom = 1.0;
    private const double MaxZoom = 5.0;
    private const double ZoomStep = 0.25;
    private bool _isUpdatingZoom = false;
    
    /// <summary>
    /// Initializes zoom controls (slider and buttons).
    /// </summary>
    private void InitializeZoomControls()
    {
        ZoomSlider.Minimum = MinZoom;
        ZoomSlider.Maximum = MaxZoom;
        ZoomSlider.Value = _currentZoom;
        UpdateZoomLevelLabel();
    }
    
    /// <summary>
    /// Handles zoom in button click.
    /// </summary>
    private void OnZoomInClicked(object? sender, EventArgs e)
    {
        var newZoom = Math.Min(MaxZoom, _currentZoom + ZoomStep);
        SetZoom(newZoom);
    }
    
    /// <summary>
    /// Handles zoom out button click.
    /// </summary>
    private void OnZoomOutClicked(object? sender, EventArgs e)
    {
        var newZoom = Math.Max(MinZoom, _currentZoom - ZoomStep);
        SetZoom(newZoom);
    }
    
    /// <summary>
    /// Handles zoom slider value change.
    /// </summary>
    private void OnZoomSliderValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (!_isUpdatingZoom)
        {
            SetZoom(e.NewValue);
        }
    }
    
    /// <summary>
    /// Sets the zoom level and updates all related controls.
    /// </summary>
    private void SetZoom(double zoomLevel)
    {
        _currentZoom = Math.Max(MinZoom, Math.Min(MaxZoom, zoomLevel));
        
        // Update slider without triggering event
        _isUpdatingZoom = true;
        ZoomSlider.Value = _currentZoom;
        _isUpdatingZoom = false;
        
        // Apply scale to image
        FloorPlanImage.Scale = _currentZoom;
        
        // Update container size to allow scrolling when zoomed
        if (FloorPlanImage.Width > 0 && FloorPlanImage.Height > 0)
        {
            MapContainer.MinimumWidthRequest = FloorPlanImage.Width * _currentZoom;
            MapContainer.MinimumHeightRequest = FloorPlanImage.Height * _currentZoom;
        }
        
        // Update zoom level label
        UpdateZoomLevelLabel();
        
        // Recalculate room zones for new zoom level
        RecalculateRoomZones();
    }
    
    /// <summary>
    /// Updates the zoom level label to show current percentage.
    /// </summary>
    private void UpdateZoomLevelLabel()
    {
        var percentage = (int)(_currentZoom * 100);
        ZoomLevelLabel.Text = $"{percentage}%";
    }
    
    /// <summary>
    /// Recalculates room zones for the current zoom level.
    /// </summary>
    private void RecalculateRoomZones()
    {
        FloorPlan? floorPlan = null;
        if (_currentSelection == "ETL")
        {
            floorPlan = _floorPlanService.GetETLFloorPlan();
        }
        else
        {
            floorPlan = _floorPlanService.GetFloorPlan(_currentFloor);
        }
        
        if (floorPlan != null && _imageWidth > 0 && _imageHeight > 0)
        {
            ClearRoomZones();
            AddRoomZones(floorPlan);
            AddCurrentLocationMarker();
        }
    }
    #endregion
}

