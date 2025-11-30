// Code written for Room Tooltip Control functionality
using Dashboard.Models;

namespace Dashboard.Controls;

/// <summary>
/// Reusable tooltip control that displays room information (room number, professor name, room name).
/// </summary>
public partial class RoomTooltip : ContentView
{
    public RoomTooltip()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Updates the tooltip content with room information.
    /// </summary>
    /// <param name="room">The room to display information for.</param>
    public void UpdateContent(Room room)
    {
        if (room == null)
        {
            return;
        }

        // Set room number
        RoomNumberLabel.Text = $"Room {room.RoomNumber}";

        // Set professor name (if available)
        if (!string.IsNullOrEmpty(room.ProfessorName))
        {
            ProfessorNameLabel.Text = room.ProfessorName;
            ProfessorNameLabel.IsVisible = true;
        }
        else
        {
            ProfessorNameLabel.IsVisible = false;
        }

        // Set room name/type (if available)
        if (!string.IsNullOrEmpty(room.RoomName))
        {
            RoomNameLabel.Text = room.RoomName;
            RoomNameLabel.IsVisible = true;
        }
        else
        {
            RoomNameLabel.IsVisible = false;
        }
    }
}

