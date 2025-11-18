// Code written for FloorPlan model to represent a building floor
namespace Dashboard.Models;

/// <summary>
/// Represents a complete floor plan for a building floor.
/// Contains floor information and a list of all rooms on that floor.
/// </summary>
public class FloorPlan
{
    /// <summary>
    /// The floor number (1 = first floor, 2 = second floor, etc.).
    /// </summary>
    public int FloorNumber { get; set; }

    /// <summary>
    /// The display name of the floor (e.g., "First Floor", "Ground Floor").
    /// </summary>
    public string FloorName { get; set; } = string.Empty;

    /// <summary>
    /// Path to the SVG image file for this floor plan.
    /// </summary>
    public string SvgPath { get; set; } = string.Empty;

    /// <summary>
    /// List of all rooms on this floor.
    /// </summary>
    public List<Room> Rooms { get; set; } = new List<Room>();
}

