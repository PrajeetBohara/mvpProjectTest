using Microsoft.Maui.Controls;
using Dashboard.Models;
using Dashboard.Services;
using System.Collections.ObjectModel;

namespace Dashboard.Pages;

/// <summary>
/// EWeek2025Page for displaying E-Week 2025 events, competitions, and activities.
/// This page shows the current year's E-Week information.
/// </summary>
public partial class EWeek2025Page : ContentPage
{
    private readonly EWeekGalleryService _galleryService;
    public ObservableCollection<EWeekGalleryImage> GalleryImages { get; set; } = new();

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the EWeek2025Page.
    /// </summary>
    public EWeek2025Page()
    {
        InitializeComponent();
        _galleryService = Application.Current!.Handler!.MauiContext!.Services.GetService<EWeekGalleryService>()!;
        BindingContext = this;
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the back button click event.
    /// Navigates back to the main E-Week page.
    /// </summary>
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Use absolute route to ensure navigation works consistently
        await Shell.Current.GoToAsync("//E-Week");
    }

    /// <summary>
    /// Loads gallery images when the page appears
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadGalleryImages();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Loads gallery images from Supabase
    /// </summary>
    private async Task LoadGalleryImages()
    {
        try
        {
            // Test connection first
            var connectionOk = await _galleryService.TestConnectionAsync();
            System.Diagnostics.Debug.WriteLine($"Database connection test: {connectionOk}");

            // Load all gallery images for 2025
            var images = await _galleryService.GetGalleryImagesAsync(2025);
            System.Diagnostics.Debug.WriteLine($"Loaded {images.Count} gallery images for 2025");
            
            GalleryImages.Clear();
            foreach (var image in images)
            {
                GalleryImages.Add(image);
            }

            // If no images loaded, add some fallback data for testing
            if (GalleryImages.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No images found for 2025, adding fallback data for testing");
                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "E-Week 2025 Opening",
                    Description = "E-Week 2025 opening ceremony",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4903.jpg",           
                    Category = "events",
                    DisplayOrder = 1,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });
                
                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "Innovation Showcase",
                    Description = "Students presenting cutting-edge projects",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4867%20-%20Copy.jpg",
                    Category = "projects",
                    DisplayOrder = 2,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });
                
                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "Tech Competition",
                    Description = "Engineering technology competition",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4881.jpg",
                    Category = "events",
                    DisplayOrder = 3,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });

                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "E-Week 2025 Opening",
                    Description = "E-Week 2025 opening ceremony",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4882.jpg",
                    Category = "events",
                    DisplayOrder = 1,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });

                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "Innovation Showcase",
                    Description = "Students presenting cutting-edge projects",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4894.jpg",
                    Category = "projects",
                    DisplayOrder = 2,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });

                GalleryImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2025,
                    Title = "Tech Competition",
                    Description = "Engineering technology competition",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2025/_MG_4858%20-%20Copy.jpg",
                    Category = "events",
                    DisplayOrder = 3,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading gallery images: {ex.Message}");
            // Could show a user-friendly error message here
        }
    }
    #endregion
}
