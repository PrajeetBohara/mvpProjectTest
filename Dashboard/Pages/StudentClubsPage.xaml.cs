// Code written for Student Clubs Page functionality
using Dashboard.Models;
using Dashboard.Services;
using System.Collections.ObjectModel;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the StudentClubsPage xaml file.
/// Displays a list of student clubs and handles navigation to club details.
/// </summary>
public partial class StudentClubsPage : ContentPage
{
    private readonly StudentClubService _clubService;
    public ObservableCollection<StudentClub> Clubs { get; set; } = new();

    public StudentClubsPage(StudentClubService clubService)
    {
        InitializeComponent();
        _clubService = clubService;
        BindingContext = this;
        
        LoadClubs();
    }

    /// <summary>
    /// Loads all student clubs from the service.
    /// </summary>
    private void LoadClubs()
    {
        var clubs = _clubService.GetAllClubs();
        Clubs.Clear();
        foreach (var club in clubs)
        {
            Clubs.Add(club);
        }
    }

    /// <summary>
    /// Handles club tap to navigate to club details.
    /// </summary>
    private async void OnClubTapped(object? sender, TappedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("=== OnClubTapped triggered ===");
        
        if (sender is Frame frame && frame.BindingContext is StudentClub selectedClub)
        {
            System.Diagnostics.Debug.WriteLine($"Tapped club: {selectedClub.Name} (ID: {selectedClub.Id})");
            
            try
            {
                // Navigate to club detail page using registered route
                var route = $"ClubDetail?clubId={selectedClub.Id}";
                System.Diagnostics.Debug.WriteLine($"Navigating to: {route}");
                await Shell.Current.GoToAsync(route);
                System.Diagnostics.Debug.WriteLine("✓ Navigation completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Navigation error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                await DisplayAlert("Navigation Error", $"Could not open club details.\n\nError: {ex.Message}", "OK");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("✗ Tapped item is not a StudentClub or BindingContext is null");
            if (sender != null)
            {
                System.Diagnostics.Debug.WriteLine($"Sender type: {sender.GetType().Name}");
            }
        }
    }
    
    /// <summary>
    /// Handles club selection to navigate to club details (kept for backward compatibility).
    /// </summary>
    private async void OnClubSelected(object? sender, SelectionChangedEventArgs e)
    {
        // This method is kept but not used - we use OnClubTapped instead
        System.Diagnostics.Debug.WriteLine("OnClubSelected called (should use OnClubTapped)");
    }
}

