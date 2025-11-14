// Code written for Campus Map Page functionality
using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the CampusMapPage xaml file.
/// Displays the campus map with a "you are here" location marker.
/// </summary>
public partial class CampusMapPage : ContentPage
{
    private double _mapWidth = 0;
    private double _mapHeight = 0;
    
    // Campus location coordinates (relative: 0.0 to 1.0)
    private const double CampusLocationX = 0.50; // Center of campus map
    private const double CampusLocationY = 0.50; // Center of campus map

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the CampusMapPage.
    /// Sets up the campus map with location marker.
    /// </summary>
    public CampusMapPage()
    {
        InitializeComponent();
        
        // Load campus map PNG image
        CampusMapImage.Source = "campus_map.png";
        
        // Set up image size tracking
        CampusMapImage.SizeChanged += OnImageSizeChanged;
        
        // Wait for image to load, then add location marker
        CampusMapImage.Loaded += (s, e) => {
            AddCurrentLocationMarker();
        };
    }
    #endregion

    #region Size Tracking
    /// <summary>
    /// Handles image size changes to recalculate location marker position.
    /// </summary>
    private void OnImageSizeChanged(object? sender, EventArgs e)
    {
        if (CampusMapImage.Width > 0 && CampusMapImage.Height > 0)
        {
            _mapWidth = CampusMapImage.Width;
            _mapHeight = CampusMapImage.Height;
            
            // Recalculate location marker when image size changes
            LocationOverlay.Children.Clear();
            AddCurrentLocationMarker();
        }
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

    #region Location Marker
    /// <summary>
    /// Adds the current location indicator ("You are here") to the map.
    /// Same style as department map but with green color instead of red.
    /// </summary>
    private void AddCurrentLocationMarker()
    {
        if (_mapWidth == 0 || _mapHeight == 0) return;
        
        // Calculate absolute position based on relative coordinates
        var x = CampusLocationX * _mapWidth;
        var y = CampusLocationY * _mapHeight;
        
        // Create pulsing location marker (McNeese Sunflower Gold) - same style as department map
        var marker = new BoxView
        {
            BackgroundColor = Color.FromArgb("#FFD204"), // McNeese Sunflower Gold
            WidthRequest = 20,
            HeightRequest = 20,
            CornerRadius = 10 // Makes it circular
        };
        
        // Position the marker
        AbsoluteLayout.SetLayoutBounds(marker, new Rect(x - 10, y - 10, 20, 20));
        LocationOverlay.Children.Add(marker);
        
        // Add label with gold text (McNeese brand color)
        var label = new Label
        {
            Text = "You are here",
            FontSize = 12,
            TextColor = Color.FromArgb("#FFD204"), // McNeese Sunflower Gold
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Colors.White,
            Padding = new Thickness(5, 2, 5, 2)
        };
        AbsoluteLayout.SetLayoutBounds(label, new Rect(x - 40, y - 35, 80, 20));
        LocationOverlay.Children.Add(label);
        
        // Start pulsing animation (same as department map)
        StartLocationPulseAnimation(marker);
    }

    /// <summary>
    /// Starts a pulsing animation for the location marker.
    /// Same animation as department map.
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
}

