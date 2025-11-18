// Code written for Student Club Detail Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;

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

    public StudentClubDetailPage(StudentClubService clubService)
    {
        InitializeComponent();
        _clubService = clubService;
    }

    /// <summary>
    /// Gets or sets the club ID from query parameters.
    /// </summary>
    public string ClubId
    {
        get => _clubId;
        set
        {
            _clubId = value;
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
        // Get club ID from query parameters
        if (!string.IsNullOrEmpty(_clubId) && int.TryParse(_clubId, out var clubId))
        {
            _currentClub = _clubService.GetClubById(clubId);
            if (_currentClub != null)
            {
                DisplayClubDetails(_currentClub);
            }
        }
        else
        {
            // Fallback: try to get from Shell query
            if (Shell.Current?.CurrentState?.Location is not null)
            {
                var queryString = Shell.Current.CurrentState.Location.Query;
                if (!string.IsNullOrEmpty(queryString))
                {
                    // Parse query string manually (format: ?clubId=1)
                    var parts = queryString.TrimStart('?').Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2 && keyValue[0] == "clubId" && int.TryParse(keyValue[1], out var id))
                        {
                            _currentClub = _clubService.GetClubById(id);
                            if (_currentClub != null)
                            {
                                DisplayClubDetails(_currentClub);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Displays the club details in the UI.
    /// </summary>
    private void DisplayClubDetails(StudentClub club)
    {
        ClubNameLabel.Text = club.Name;
        ClubFullNameLabel.Text = club.FullName;
        ClubImage.Source = club.ImageUrl;
        DescriptionLabel.Text = club.Description;
        ContactInfoLabel.Text = club.ContactInfo;
        MeetingInfoLabel.Text = club.MeetingInfo;
        
        // Show website section if URL is available
        if (!string.IsNullOrEmpty(club.WebsiteUrl))
        {
            WebsiteFrame.IsVisible = true;
        }
        else
        {
            WebsiteFrame.IsVisible = false;
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

    /// <summary>
    /// Handles website button click to open the club's website.
    /// </summary>
    private async void OnWebsiteButtonClicked(object? sender, EventArgs e)
    {
        if (_currentClub != null && !string.IsNullOrEmpty(_currentClub.WebsiteUrl))
        {
            try
            {
                await Microsoft.Maui.ApplicationModel.Launcher.Default.OpenAsync(_currentClub.WebsiteUrl);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error opening website: {ex.Message}");
                await DisplayAlert("Error", "Could not open website. Please check the URL.", "OK");
            }
        }
    }
}

