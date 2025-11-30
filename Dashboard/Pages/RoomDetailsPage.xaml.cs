// Code written for Room Details Page functionality
using Dashboard.Models;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the RoomDetailsPage xaml file.
/// Displays detailed information about a selected room from the floor plan.
/// </summary>
public partial class RoomDetailsPage : ContentPage
{
    private Room? _room;

    /// <summary>
    /// Initializes a new instance of the RoomDetailsPage.
    /// </summary>
    public RoomDetailsPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the RoomDetailsPage with room data.
    /// </summary>
    /// <param name="room">The room to display details for.</param>
    public RoomDetailsPage(Room room) : this()
    {
        _room = room;
        LoadRoomDetails();
    }

    /// <summary>
    /// Loads and displays the room details.
    /// </summary>
    private void LoadRoomDetails()
    {
        if (_room == null) return;

        // Room Number
        RoomNumberLabel.Text = $"Room {_room.RoomNumber}";

        // Room Name
        RoomNameLabel.Text = !string.IsNullOrEmpty(_room.RoomName) 
            ? _room.RoomName 
            : "Room";

        // Room Type
        RoomTypeLabel.Text = _room.RoomType.ToString();

        // Square Footage
        SquareFootageLabel.Text = $"{_room.SquareFootage} sq ft";

        // Professor Name (if applicable)
        if (!string.IsNullOrEmpty(_room.ProfessorName))
        {
            ProfessorNameLabel.Text = _room.ProfessorName;
            ProfessorNameContainer.IsVisible = true;
        }
        else
        {
            ProfessorNameContainer.IsVisible = false;
        }

        // Description
        if (!string.IsNullOrEmpty(_room.Description))
        {
            DescriptionLabel.Text = _room.Description;
            DescriptionCard.IsVisible = true;
        }
        else
        {
            DescriptionCard.IsVisible = false;
        }
    }

    /// <summary>
    /// Handles back button click to navigate back to the map.
    /// </summary>
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync();
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating back: {ex.Message}");
        }
    }
}

