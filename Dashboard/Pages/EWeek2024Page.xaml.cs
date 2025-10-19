using Microsoft.Maui.Controls;
using Dashboard.Models;
using Dashboard.Services;
using System.Collections.ObjectModel;

namespace Dashboard.Pages;

/// <summary>
/// EWeek2024Page for displaying E-Week 2024 archive with past events, winners, and memorable moments.
/// This page shows the previous year's E-Week information and achievements.
/// </summary>
public partial class EWeek2024Page : ContentPage
{
    private readonly EWeekGalleryService _galleryService;
    public ObservableCollection<EWeekGalleryImage> GalleryImages { get; set; } = new();
    public ObservableCollection<EWeekGalleryImage> FeaturedImages { get; set; } = new();

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the EWeek2024Page.
    /// </summary>
    public EWeek2024Page()
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

            // Load all gallery images for 2024
            var allImages = await _galleryService.GetGalleryImagesAsync(2024);
            System.Diagnostics.Debug.WriteLine($"Loaded {allImages.Count} gallery images");
            
            GalleryImages.Clear();
            foreach (var image in allImages)
            {
                GalleryImages.Add(image);
            }

            // Load featured images for 2024
            var featured = await _galleryService.GetFeaturedImagesAsync(2024);
            System.Diagnostics.Debug.WriteLine($"Loaded {featured.Count} featured images");
            
            FeaturedImages.Clear();
            foreach (var image in featured)
            {
                FeaturedImages.Add(image);
            }

            // If no images loaded, add some fallback data for testing
            if (FeaturedImages.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No images found, adding fallback data for testing");
                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Engineering Competition",
                    Description = "Students competing in engineering challenges",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo3.avif",
                    Category = "events",
                    DisplayOrder = 1,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                });
                
                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Award Ceremony",
                    Description = "Recognition for outstanding achievements",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo2.avif",
                    Category = "winners",
                    DisplayOrder = 2,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                });
                
                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Project Showcase",
                    Description = "Students presenting innovative solutions",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo1.avif",
                    Category = "projects",
                    DisplayOrder = 3,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                });

                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Engineering Competition",
                    Description = "Students competing in engineering challenges",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo4.avif",
                    Category = "events",
                    DisplayOrder = 4,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                });

                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Award Ceremony",
                    Description = "Recognition for outstanding achievements",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo5.avif",
                    Category = "winners",
                    DisplayOrder = 5,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                });

                FeaturedImages.Add(new EWeekGalleryImage
                {
                    Id = Guid.NewGuid(),
                    Year = 2024,
                    Title = "Project Showcase",
                    Description = "Students presenting innovative solutions",
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/eweek/2024/Expo6.avif",
                    Category = "projects",
                    DisplayOrder = 6,
                    IsFeatured = true,
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
