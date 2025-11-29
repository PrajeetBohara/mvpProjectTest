// Code written for Student Club Detail Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the StudentClubDetailPage xaml file.
/// Displays detailed information about a selected student club.
/// </summary>
[QueryProperty(nameof(ClubId), "clubId")]
public partial class StudentClubDetailPage : ContentPage
{
    private readonly StudentClubService _clubService;
    private StudentClub? _currentClub;
    private string _clubId = string.Empty;
    
    /// <summary>
    /// Observable collection for gallery images to enable data binding.
    /// </summary>
    public ObservableCollection<string> GalleryImages { get; set; } = new();

    public StudentClubDetailPage(StudentClubService clubService)
    {
        InitializeComponent();
        _clubService = clubService;
        BindingContext = this; // Set binding context for gallery images
    }

    /// <summary>
    /// Gets or sets the club ID from query parameters.
    /// </summary>
    public string ClubId
    {
        get => _clubId;
        set
        {
            System.Diagnostics.Debug.WriteLine($"=== ClubId property set ===");
            System.Diagnostics.Debug.WriteLine($"Received value: '{value}'");
            _clubId = value ?? string.Empty;
            LoadClubDetails();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (string.IsNullOrEmpty(_clubId))
        {
            LoadClubDetails();
        }
    }

    /// <summary>
    /// Loads club details from query parameters.
    /// </summary>
    private void LoadClubDetails()
    {
        System.Diagnostics.Debug.WriteLine($"=== LoadClubDetails called ===");
        System.Diagnostics.Debug.WriteLine($"Current _clubId: '{_clubId}'");
        
        // Get club ID from query parameters
        if (!string.IsNullOrEmpty(_clubId) && int.TryParse(_clubId, out var clubId))
        {
            System.Diagnostics.Debug.WriteLine($"Parsed clubId: {clubId}");
            _currentClub = _clubService.GetClubById(clubId);
            if (_currentClub != null)
            {
                System.Diagnostics.Debug.WriteLine($"Found club: {_currentClub.Name}");
                DisplayClubDetails(_currentClub);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"✗ No club found with ID: {clubId}");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"✗ Could not parse clubId from: '{_clubId}'");
            
            // Fallback: try to get from Shell query
            if (Shell.Current?.CurrentState?.Location is not null)
            {
                var location = Shell.Current.CurrentState.Location;
                System.Diagnostics.Debug.WriteLine($"Shell location: {location}");
                var queryString = location.Query;
                System.Diagnostics.Debug.WriteLine($"Query string: '{queryString}'");
                
                if (!string.IsNullOrEmpty(queryString))
                {
                    // Parse query string manually (format: ?clubId=1)
                    var parts = queryString.TrimStart('?').Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2 && keyValue[0] == "clubId" && int.TryParse(keyValue[1], out var id))
                        {
                            System.Diagnostics.Debug.WriteLine($"Found clubId in query: {id}");
                            _currentClub = _clubService.GetClubById(id);
                            if (_currentClub != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found club: {_currentClub.Name}");
                                DisplayClubDetails(_currentClub);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("✗ Shell.Current or CurrentState is null");
            }
        }
    }

    /// <summary>
    /// Displays the club details in the UI.
    /// Combines all information (Description, Mission, Vision, Values) into one About section.
    /// </summary>
    private void DisplayClubDetails(StudentClub club)
    {
        ClubNameLabel.Text = club.Name;
        ClubFullNameLabel.Text = club.FullName;
        
        // Set club logo from Supabase (fallback to ImageUrl if LogoUrl is empty)
        if (!string.IsNullOrEmpty(club.LogoUrl))
        {
            ClubImage.Source = club.LogoUrl;
        }
        else if (!string.IsNullOrEmpty(club.ImageUrl))
        {
            ClubImage.Source = club.ImageUrl;
        }
        else
        {
            ClubImage.Source = "mcneeselogo.png"; // Default fallback
        }
        
        // Combine all information into one About section
        var aboutContent = new System.Text.StringBuilder();
        
        // Add Description
        if (!string.IsNullOrEmpty(club.Description))
        {
            // Clean up bullet points and formatting
            var description = club.Description
                .Replace("•", "")
                .Replace("\n\n", "\n")
                .Trim();
            aboutContent.AppendLine(description);
        }
        
        // Add Mission if available
        if (!string.IsNullOrEmpty(club.Mission))
        {
            if (aboutContent.Length > 0) aboutContent.AppendLine("\n");
            aboutContent.AppendLine("Mission:");
            aboutContent.AppendLine(club.Mission);
        }
        
        // Add Vision if available
        if (!string.IsNullOrEmpty(club.Vision))
        {
            if (aboutContent.Length > 0) aboutContent.AppendLine("\n");
            aboutContent.AppendLine("Vision:");
            aboutContent.AppendLine(club.Vision);
        }
        
        // Add Values if available
        if (!string.IsNullOrEmpty(club.Values))
        {
            if (aboutContent.Length > 0) aboutContent.AppendLine("\n");
            aboutContent.AppendLine("Values:");
            // Format values nicely (handle comma-separated or newline-separated)
            var values = club.Values
                .Replace("\n\n", "\n")
                .Replace(", ", "\n• ")
                .Replace(",", "\n• ");
            if (!values.StartsWith("•")) values = "• " + values;
            aboutContent.AppendLine(values);
        }
        
        AboutContentLabel.Text = aboutContent.ToString().Trim();
        
        // Year Established
        if (club.YearEstablished.HasValue)
        {
            YearEstablishedLabel.Text = club.YearEstablished.Value.ToString();
            YearEstablishedFrame.IsVisible = true;
        }
        else
        {
            YearEstablishedFrame.IsVisible = false;
        }
        
        // Meeting Time
        if (!string.IsNullOrEmpty(club.MeetingTime))
        {
            MeetingTimeLabel.Text = club.MeetingTime;
            MeetingTimeFrame.IsVisible = true;
        }
        else
        {
            MeetingTimeFrame.IsVisible = false;
        }
        
        // Meeting Location
        if (!string.IsNullOrEmpty(club.MeetingLocation))
        {
            MeetingLocationLabel.Text = club.MeetingLocation;
            MeetingLocationFrame.IsVisible = true;
        }
        else
        {
            MeetingLocationFrame.IsVisible = false;
        }
        
        // Social Media Links (Instagram and LinkedIn only)
        bool hasAnySocial = false;
        
        if (!string.IsNullOrEmpty(club.InstagramUrl))
        {
            InstagramUrlLabel.Text = club.InstagramUrl;
            InstagramFrame.IsVisible = true;
            hasAnySocial = true;
        }
        else
        {
            InstagramFrame.IsVisible = false;
        }
        
        if (!string.IsNullOrEmpty(club.LinkedInUrl))
        {
            LinkedInUrlLabel.Text = club.LinkedInUrl;
            LinkedInFrame.IsVisible = true;
            hasAnySocial = true;
        }
        else
        {
            LinkedInFrame.IsVisible = false;
        }
        
        SocialsFrame.IsVisible = hasAnySocial;
        
        // Gallery Images
        GalleryImages.Clear();
        if (club.GalleryImageUrls != null && club.GalleryImageUrls.Count > 0)
        {
            foreach (var imageUrl in club.GalleryImageUrls)
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    GalleryImages.Add(imageUrl);
                }
            }
            GalleryFrame.IsVisible = GalleryImages.Count > 0;
        }
        else
        {
            GalleryFrame.IsVisible = false;
        }
    }

    /// <summary>
    /// Handles back button click to navigate back to clubs list.
    /// </summary>
    private async void OnBackButtonClicked(object? sender, EventArgs e)
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
            System.Diagnostics.Debug.WriteLine($"Error navigating back to clubs: {ex.Message}");
        }
    }


}

