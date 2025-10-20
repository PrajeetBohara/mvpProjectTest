using System.Collections.ObjectModel;
using Dashboard.Models;
using Dashboard.Services;

namespace Dashboard.Pages;

public partial class FacultyDirectoryPage : ContentPage
{
    private bool _isResearchDetailsVisible = false;
    private readonly ResearchImageService _researchImageService;
    
    public ObservableCollection<ResearchImage> ResearchImages { get; set; } = new();

    public FacultyDirectoryPage()
    {
        InitializeComponent();
        _researchImageService = Application.Current!.Handler!.MauiContext!.Services.GetService<ResearchImageService>()!;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadResearchImages();
    }

    private async Task LoadResearchImages()
    {
        try
        {
            var connectionOk = await _researchImageService.TestConnectionAsync();
            System.Diagnostics.Debug.WriteLine($"Faculty research images connection test: {connectionOk}");

            var researchImages = await _researchImageService.GetFacultyResearchImagesAsync();
            System.Diagnostics.Debug.WriteLine($"Loaded {researchImages.Count} faculty research images");

            ResearchImages.Clear();
            foreach (var image in researchImages)
            {
                ResearchImages.Add(image);
            }

            // Add fallback data if no images found
            if (ResearchImages.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No faculty research images found, adding fallback data");
                ResearchImages.Add(new ResearchImage
                {
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty_research/image%20(2).png",
                    Caption = "VR Lab Setup",
                    FileName = "vr_lab_setup.jpg"
                });
                
                ResearchImages.Add(new ResearchImage
                {
                    ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/faculty_research/image.png",
                    Caption = "Game Development",
                    FileName = "game_development.jpg"
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading faculty research images: {ex.Message}");
        }
    }

    private void OnExpandButtonClicked(object sender, EventArgs e)
    {
        try
        {
            _isResearchDetailsVisible = !_isResearchDetailsVisible;
            ResearchDetailsFrame.IsVisible = _isResearchDetailsVisible;
            
            // Update button text and arrow direction
            if (_isResearchDetailsVisible)
            {
                ExpandButton.Text = "Hide Research Details ▲";
            }
            else
            {
                ExpandButton.Text = "View Research Details ▼";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error toggling research details: {ex.Message}");
        }
    }
}
