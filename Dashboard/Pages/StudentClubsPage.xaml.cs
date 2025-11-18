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
    /// Handles club selection to navigate to club details.
    /// </summary>
    private async void OnClubSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is StudentClub selectedClub)
        {
            // Clear selection
            ClubsCollectionView.SelectedItem = null;
            
            // Navigate to club detail page with query parameter
            await Shell.Current.GoToAsync($"//ClubDetail?clubId={selectedClub.Id}");
        }
    }
}

