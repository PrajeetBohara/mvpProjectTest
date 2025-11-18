// Code written for BuildingLocation model to represent current user location
namespace Dashboard.Models;

/// <summary>
/// Represents a location point within the building where the user is currently located.
/// Used to display the "You are here" indicator on the interactive map.
/// </summary>
public class BuildingLocation
{
    /// <summary>
    /// The floor number where the location is (1 = first floor, 2 = second floor, etc.).
    /// </summary>
    public int FloorNumber { get; set; }

    /// <summary>
    /// X coordinate of the location on the floor plan (in pixels or relative units).
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y coordinate of the location on the floor plan (in pixels or relative units).
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Description or label for this location (e.g., "Main Entrance", "Information Desk").
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

