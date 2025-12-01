// Code written for Interactive Department Map Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the DepartmentMapPage xaml file.
/// Provides interactive floor plan navigation with room details and current location.
/// </summary>
public partial class DepartmentMapPage : ContentPage
{
    private readonly FloorPlanService _floorPlanService;
    private double _currentZoom = 1.0;
    private bool _isUpdatingZoom = false;
    private double _panX = 0;
    private double _panY = 0;
    
    // Vercel deployment URL
    private const string VercelMapUrl = "https://interactive-map-seven-kappa.vercel.app/";
    
    // Zoom constants
    private const double MinZoom = 0.5;
    private const double MaxZoom = 3.0;
    private const double ZoomStep = 0.1;
    
    // Pan constants
    private const double PanStep = 50; // Pixels to pan per click

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the DepartmentMapPage.
    /// Sets up the interactive map with floor selection and room interactions.
    /// </summary>
    public DepartmentMapPage(FloorPlanService floorPlanService)
    {
        InitializeComponent();
        _floorPlanService = floorPlanService;
        
        // Enable zoom and pan for WebView
        EnableWebViewZoom();
        
        // Load the Vercel interactive map
        LoadVercelMap();
    }
    
    /// <summary>
    /// Enables zoom and pan functionality for the WebView on all platforms.
    /// </summary>
    private void EnableWebViewZoom()
    {
        #if ANDROID
        // Android-specific: Enable zoom controls and scrolling
        InteractiveMapWebView.HandlerChanged += (s, e) =>
        {
            if (InteractiveMapWebView.Handler?.PlatformView is Android.Webkit.WebView androidWebView)
            {
                var settings = androidWebView.Settings;
                settings.SetSupportZoom(true);
                settings.BuiltInZoomControls = true;
                settings.DisplayZoomControls = false; // Hide default zoom controls, use pinch-to-zoom
                settings.UseWideViewPort = true;
                settings.LoadWithOverviewMode = true;
                settings.JavaScriptEnabled = true;
                settings.DomStorageEnabled = true;
                
                // Enable scrolling/panning
                androidWebView.ScrollBarStyle = Android.Views.ScrollbarStyles.OutsideOverlay;
                androidWebView.ScrollbarFadingEnabled = false;
            }
        };
        #elif IOS || MACCATALYST
        // iOS/Mac: Zoom is enabled by default, but we can configure it
        InteractiveMapWebView.HandlerChanged += (s, e) =>
        {
            if (InteractiveMapWebView.Handler?.PlatformView is WebKit.WKWebView iosWebView)
            {
                iosWebView.Configuration.AllowsInlineMediaPlayback = true;
                iosWebView.Configuration.MediaTypesRequiringUserActionForPlayback = WebKit.WKAudiovisualMediaTypes.None;
            }
        };
        #elif WINDOWS
        // Windows: WebView2 supports zoom by default
        // Zoom can be controlled via Ctrl+Plus, Ctrl+Minus, or mouse wheel
        InteractiveMapWebView.HandlerChanged += (s, e) =>
        {
            // WebView2 on Windows supports zoom natively
            // No additional configuration needed
        };
        #endif
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

    #region Map Loading
    /// <summary>
    /// Loads the Vercel-deployed interactive map in the WebView.
    /// The map supports zoom, pan, and all interactive features natively through the WebView.
    /// </summary>
    private void LoadVercelMap()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Loading Vercel map: {VercelMapUrl}");
            
            InteractiveMapWebView.Source = new UrlWebViewSource
            {
                Url = VercelMapUrl
            };
            
            System.Diagnostics.Debug.WriteLine($"✓ Vercel map loaded successfully");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"✗ Error loading Vercel map: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Error", $"Could not load interactive map.\n\n{ex.Message}", "OK");
            });
        }
    }
    
    /// <summary>
    /// Handles WebView navigation start.
    /// </summary>
    private void OnWebViewNavigating(object? sender, WebNavigatingEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"WebView navigating to: {e.Url}");
    }
    
    /// <summary>
    /// Handles WebView navigation completion.
    /// Ensures panning is enabled after page loads.
    /// </summary>
    private async void OnWebViewNavigated(object? sender, WebNavigatedEventArgs e)
    {
        if (e.Result == WebNavigationResult.Success)
        {
            System.Diagnostics.Debug.WriteLine($"WebView navigated successfully");
            
            // Wait a bit for the page to fully load
            await Task.Delay(500);
            
            // Ensure the page can handle touch gestures (pan/zoom) directly inside the WebView
            try
            {
                var enableInteractionScript = @"
                    (function() {
                        var body = document.body;
                        var html = document.documentElement;
                        
                        // Allow scrolling and touch gestures
                        body.style.overflow = 'auto';
                        html.style.overflow = 'auto';
                        
                        body.style.touchAction = 'manipulation';
                        html.style.touchAction = 'manipulation';
                        
                        // Ensure pointer events are enabled
                        body.style.pointerEvents = 'auto';
                        html.style.pointerEvents = 'auto';
                    })();
                ";
                
                await InteractiveMapWebView.EvaluateJavaScriptAsync(enableInteractionScript);
                
                System.Diagnostics.Debug.WriteLine($"✓ WebView interactions enabled");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Error enabling interactions: {ex.Message}");
            }
        }
    }
    #endregion

    #region Zoom Controls
    #endregion


}

