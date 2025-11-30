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
        
        StudentClub? selectedClub = null;
        
        // Try to get the club from the sender's BindingContext
        if (sender is Frame frame && frame.BindingContext is StudentClub club)
        {
            selectedClub = club;
        }
        // Try to get from the parent's BindingContext if sender is TapGestureRecognizer
        else if (sender is TapGestureRecognizer recognizer && recognizer.Parent?.BindingContext is StudentClub club2)
        {
            selectedClub = club2;
        }
        // Try to get from the parent view's BindingContext
        else if (sender is View view && view.Parent?.BindingContext is StudentClub club3)
        {
            selectedClub = club3;
        }
        // Last resort: try to get from the parent's parent
        else if (sender is View view2 && view2.Parent is View parent && parent.BindingContext is StudentClub club4)
        {
            selectedClub = club4;
        }
        
        if (selectedClub != null)
        {
            System.Diagnostics.Debug.WriteLine($"Tapped club: {selectedClub.Name} (ID: {selectedClub.Id})");
            
            try
            {
                // Navigate to club detail page using registered route
                var route = $"ClubDetail?clubId={selectedClub.Id}";
                System.Diagnostics.Debug.WriteLine($"Navigating to: {route}");
                
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync(route);
                    System.Diagnostics.Debug.WriteLine("✓ Navigation completed successfully");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("✗ Shell.Current is null");
                    await DisplayAlert("Navigation Error", "Navigation system is not available.", "OK");
                }
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
            System.Diagnostics.Debug.WriteLine("✗ Could not find StudentClub from tap");
            if (sender != null)
            {
                System.Diagnostics.Debug.WriteLine($"Sender type: {sender.GetType().Name}");
                if (sender is View v)
                {
                    System.Diagnostics.Debug.WriteLine($"View BindingContext type: {v.BindingContext?.GetType().Name ?? "null"}");
                }
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

