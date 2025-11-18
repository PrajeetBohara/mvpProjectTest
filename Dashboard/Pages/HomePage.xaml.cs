//Code written by Prajeet Bohara

using Microsoft.Maui.Controls;
using Dashboard.Services;
using Dashboard.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.ApplicationModel;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the HomePage xaml file.
/// </summary>
public partial class HomePage : ContentPage
{
    private readonly HomePageImageService _imageService;
    private readonly ObservableCollection<HomePageImage> _homePageImages;
    private int _currentImageIndex = 0;
    private IDispatcherTimer? _slideshowTimer;

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the HomePage.
    /// Sets up the main dashboard with all sections.
    /// </summary>
    public HomePage(HomePageImageService imageService)
    {
        InitializeComponent();
        _imageService = imageService;
        _homePageImages = new ObservableCollection<HomePageImage>();
        
        // Load images when page appears
        this.Appearing += OnPageAppearing;
    }
    #endregion

    #region Image Loading
    /// <summary>
    /// Loads homepage images from Supabase when the page appears.
    /// </summary>
    private async void OnPageAppearing(object? sender, EventArgs e)
    {
        await LoadHomePageImages();
    }

    /// <summary>
    /// Fetches homepage images from Supabase and displays them in the slideshow.
    /// </summary>
    private async Task LoadHomePageImages()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("[HomePage] Starting to load homepage images...");
            var images = await _imageService.GetFeaturedHomePageImagesAsync(limit: 10);
            System.Diagnostics.Debug.WriteLine($"[HomePage] Received {images?.Count ?? 0} images from service");
            
            _homePageImages.Clear();
            if (images != null)
            {
                foreach (var image in images)
                {
                    System.Diagnostics.Debug.WriteLine($"[HomePage] Adding image: {image.Title} - {image.ImageUrl}");
                    _homePageImages.Add(image);
                }
            }

            if (_homePageImages.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[HomePage] Successfully loaded {_homePageImages.Count} images. Updating slideshow...");
                // Update the slideshow image
                UpdateSlideshowImage();
                
                // Start auto-rotation if multiple images
                if (_homePageImages.Count > 1)
                {
                    StartSlideshowTimer();
                }
                
                // Update pagination dots
                UpdatePaginationDots();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("[HomePage] No images found. Showing default image.");
                // Show default image if no images from backend
                SlideshowImage.Source = "mcneeselogo.png";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[HomePage] Error loading homepage images: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[HomePage] Error Type: {ex.GetType().Name}");
            System.Diagnostics.Debug.WriteLine($"[HomePage] Stack Trace: {ex.StackTrace}");
            // Fallback to default image
            SlideshowImage.Source = "mcneeselogo.png";
        }
    }

    /// <summary>
    /// Updates the slideshow image to display the current image.
    /// </summary>
    private void UpdateSlideshowImage()
    {
        if (_homePageImages.Count > 0 && _currentImageIndex < _homePageImages.Count)
        {
            var currentImage = _homePageImages[_currentImageIndex];
            System.Diagnostics.Debug.WriteLine($"[HomePage] Updating slideshow image to: {currentImage.ImageUrl}");
            
            // Ensure we're on the UI thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (Uri.TryCreate(currentImage.ImageUrl, UriKind.Absolute, out var imageUri))
                    {
                        // Set opacity to 1.0 to make image fully visible
                        SlideshowImage.Opacity = 1.0;
                        
                        // Set the image source directly as a string - MAUI handles this automatically
                        SlideshowImage.Source = currentImage.ImageUrl;
                        
                        System.Diagnostics.Debug.WriteLine($"[HomePage] Image source set successfully: {currentImage.ImageUrl}");
                        System.Diagnostics.Debug.WriteLine($"[HomePage] Image Opacity: {SlideshowImage.Opacity}");
                        System.Diagnostics.Debug.WriteLine($"[HomePage] Image Source: {SlideshowImage.Source}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[HomePage] Invalid image URL format: {currentImage.ImageUrl}");
                        SlideshowImage.Source = "mcneeselogo.png";
                        SlideshowImage.Opacity = 1.0;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[HomePage] Error setting image source: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"[HomePage] Stack Trace: {ex.StackTrace}");
                    SlideshowImage.Source = "mcneeselogo.png";
                    SlideshowImage.Opacity = 1.0;
                }
                
                // Title and description labels removed - no text displayed on slideshow
            });
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"[HomePage] Cannot update slideshow - Images count: {_homePageImages.Count}, Current index: {_currentImageIndex}");
        }
    }

    /// <summary>
    /// Updates the pagination dots to reflect the current image.
    /// </summary>
    private void UpdatePaginationDots()
    {
        // Clear existing dots
        PaginationContainer.Children.Clear();
        
        for (int i = 0; i < _homePageImages.Count; i++)
        {
            var dot = new BoxView
            {
                WidthRequest = 12,
                HeightRequest = 12,
                CornerRadius = 6,
                BackgroundColor = i == _currentImageIndex ? Colors.White : Color.FromArgb("#666666"),
                Margin = new Thickness(6, 0, 0, 0)
            };
            
            // Make dots tappable
            var tapGesture = new TapGestureRecognizer();
            int index = i; // Capture index for closure
            tapGesture.Tapped += (s, e) => NavigateToImage(index);
            dot.GestureRecognizers.Add(tapGesture);
            
            PaginationContainer.Children.Add(dot);
        }
    }

    /// <summary>
    /// Navigates to a specific image in the slideshow.
    /// </summary>
    private void NavigateToImage(int index)
    {
        if (index >= 0 && index < _homePageImages.Count)
        {
            _currentImageIndex = index;
            UpdateSlideshowImage();
            UpdatePaginationDots();
            ResetSlideshowTimer();
        }
    }

    /// <summary>
    /// Starts the automatic slideshow timer to rotate images.
    /// </summary>
    private void StartSlideshowTimer()
    {
        _slideshowTimer?.Stop();
        _slideshowTimer = Dispatcher.CreateTimer();
        _slideshowTimer.Interval = TimeSpan.FromSeconds(5); // Change image every 5 seconds
        _slideshowTimer.Tick += (s, e) => NextImage();
        _slideshowTimer.Start();
    }

    /// <summary>
    /// Resets the slideshow timer.
    /// </summary>
    private void ResetSlideshowTimer()
    {
        if (_homePageImages.Count > 1)
        {
            StartSlideshowTimer();
        }
    }

    /// <summary>
    /// Moves to the next image in the slideshow.
    /// </summary>
    private void NextImage()
    {
        if (_homePageImages.Count > 0)
        {
            _currentImageIndex = (_currentImageIndex + 1) % _homePageImages.Count;
            UpdateSlideshowImage();
            UpdatePaginationDots();
        }
    }

    /// <summary>
    /// Moves to the previous image in the slideshow.
    /// </summary>
    private void PreviousImage()
    {
        if (_homePageImages.Count > 0)
        {
            _currentImageIndex = (_currentImageIndex - 1 + _homePageImages.Count) % _homePageImages.Count;
            UpdateSlideshowImage();
            UpdatePaginationDots();
        }
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles slideshow tap to open image gallery.
    /// Later on for Smart it will open full-screen image slideshow.
    /// </summary>
    private async void OnSlideshowTapped(object sender, EventArgs e)
    {
        try
        {
            await DisplayAlert("Slideshow", "Opening image gallery...", "OK");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening slideshow: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles "View All Announcements" button tap.
    /// On tapping view all, it will route to the all announcements page.
    /// </summary>
    private async void OnViewAllAnnouncementsClicked(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//AllAnnouncements");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening announcements: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles "View All Events" button tap.
    /// On tapping view all, it will route to the all events page.
    /// </summary>
    private async void OnViewAllEventsClicked(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//AllEvents");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening events: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles maps section tap to navigate to Maps page.
    /// The Maps page provides options for Department Map and Campus Map.
    /// </summary>
    private async void OnMapsTapped(object sender, EventArgs e)
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
            System.Diagnostics.Debug.WriteLine($"Error opening maps: {ex.Message}");
            await DisplayAlert("Error", "Unable to open maps. Please try again later.", "OK");
        }
    }

    /// <summary>
    /// Handles contact section tap to navigate to Contact page.
    /// </summary>
    private async void OnContactTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Contact");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening contact: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles labs section tap to navigate to Labs page.
    /// </summary>
    private async void OnLabsTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Labs");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening labs: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles projects section tap to navigate to Senior Design Projects page.
    /// </summary>
    private async void OnProjectsTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Senior Projects");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening projects: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles faculty directory section tap to navigate to Faculty Directory page.
    /// </summary>
    private async void OnFacultyDirectoryTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Faculty");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening faculty directory: {ex.Message}");
            await DisplayAlert("Error", "Unable to open faculty directory. Please try again later.", "OK");
        }
    }

    /// <summary>
    /// Handles student clubs section tap to navigate to Student Clubs page.
    /// </summary>
    private async void OnStudentClubsTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//Clubs");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening student clubs: {ex.Message}");
            await DisplayAlert("Error", "Unable to open student clubs. Please try again later.", "OK");
        }
    }

            ///// <summary>
            ///// Handles stats section tap to open detailed statistics.
            ///// We are not using it right now. This is kept for future reference
            ///// </summary>
            //private async void OnStatsTapped(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        // TODO: Navigate to detailed stats page
            //        await DisplayAlert("Statistics & Accreditation", "Opening detailed statistics and accreditation information...", "OK");
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"Error opening stats: {ex.Message}");
            //    }
            //}
    #endregion
}
