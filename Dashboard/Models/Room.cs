// Code written for Room model to represent individual rooms in the floor plan
namespace Dashboard.Models;

/// <summary>
/// Represents a room or space within a building floor plan.
/// Contains location coordinates, dimensions, and descriptive information.
/// </summary>
public class Room
{
    /// <summary>
    /// The room number or identifier (e.g., "101", "301-A").
    /// </summary>
    public string RoomNumber { get; set; } = string.Empty;

    /// <summary>
    /// The name or title of the room (e.g., "Main Lecture Hall", "Software Engineering Lab").
    /// </summary>
    public string RoomName { get; set; } = string.Empty;

    /// <summary>
    /// The type of room (Lab, Office, Classroom, etc.).
    /// </summary>
    public RoomType RoomType { get; set; }

    /// <summary>
    /// Detailed description of the room, its purpose, and features.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// X coordinate of the room's top-left corner on the floor plan (in pixels or relative units).
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y coordinate of the room's top-left corner on the floor plan (in pixels or relative units).
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Width of the room on the floor plan (in pixels or relative units).
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Height of the room on the floor plan (in pixels or relative units).
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Square footage of the room.
    /// </summary>
    public int SquareFootage { get; set; }
}

/// <summary>
/// Enumeration of room types in the building.
/// </summary>
public enum RoomType
{
    Classroom,
    Lab,
    Office,
    ConferenceRoom,
    Restroom,
    Stairwell,
    Elevator,
    Storage,
    Lobby,
    Other
}

