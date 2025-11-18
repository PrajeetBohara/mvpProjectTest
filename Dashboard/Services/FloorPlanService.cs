// Code written for FloorPlanService to manage floor plan data
using Dashboard.Models;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for managing floor plan data and room information.
/// Provides methods to retrieve floor plans, rooms, and building locations.
/// </summary>
public class FloorPlanService
{
    private readonly List<FloorPlan> _floorPlans;
    private readonly Dictionary<int, BuildingLocation> _floorLocations;

    /// <summary>
    /// Initializes the FloorPlanService with placeholder data.
    /// In the future, this data can be loaded from a database or API.
    /// </summary>
    public FloorPlanService()
    {
        _floorPlans = InitializeFloorPlans();
        _floorLocations = InitializeFloorLocations();
    }

    /// <summary>
    /// Gets all available floor plans.
    /// </summary>
    /// <returns>List of all floor plans.</returns>
    public List<FloorPlan> GetAllFloorPlans()
    {
        return _floorPlans;
    }

    /// <summary>
    /// Gets the floor plan for a specific floor number.
    /// </summary>
    /// <param name="floorNumber">The floor number (1, 2, or 3).</param>
    /// <returns>The floor plan for the specified floor, or null if not found.</returns>
    public FloorPlan? GetFloorPlan(int floorNumber)
    {
        return _floorPlans.FirstOrDefault(fp => fp.FloorNumber == floorNumber);
    }

    /// <summary>
    /// Gets the ETL (Electrical Technology Lab) floor plan.
    /// </summary>
    /// <returns>The ETL floor plan, or null if not found.</returns>
    public FloorPlan? GetETLFloorPlan()
    {
        return _floorPlans.FirstOrDefault(fp => fp.FloorNumber == 0);
    }

    /// <summary>
    /// Gets all rooms for a specific floor.
    /// </summary>
    /// <param name="floorNumber">The floor number.</param>
    /// <returns>List of rooms on the specified floor.</returns>
    public List<Room> GetRoomsForFloor(int floorNumber)
    {
        var floorPlan = GetFloorPlan(floorNumber);
        return floorPlan?.Rooms ?? new List<Room>();
    }

    /// <summary>
    /// Gets room details by room number and floor.
    /// </summary>
    /// <param name="floorNumber">The floor number.</param>
    /// <param name="roomNumber">The room number (e.g., "101").</param>
    /// <returns>The room if found, otherwise null.</returns>
    public Room? GetRoomDetails(int floorNumber, string roomNumber)
    {
        var floorPlan = GetFloorPlan(floorNumber);
        return floorPlan?.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
    }

    /// <summary>
    /// Gets the current building location (where the user is).
    /// </summary>
    /// <returns>The current building location.</returns>
    public BuildingLocation GetCurrentLocation()
    {
        // Return Floor 1 location as default
        return GetLocationForFloor(1);
    }

    /// <summary>
    /// Gets the location marker for a specific floor.
    /// </summary>
    /// <param name="floorNumber">The floor number (0 for ETL, 1, 2, or 3).</param>
    /// <returns>The building location for the specified floor, or null if not found.</returns>
    public BuildingLocation? GetLocationForFloor(int floorNumber)
    {
        return _floorLocations.TryGetValue(floorNumber, out var location) ? location : null;
    }

    /// <summary>
    /// Initializes floor plans with placeholder data.
    /// Currently only Floor 1 has detailed room data (Room 101 as placeholder).
    /// </summary>
    private List<FloorPlan> InitializeFloorPlans()
    {
        var floorPlans = new List<FloorPlan>();

        // ETL (Electrical Technology Lab) - Floor 0
        var etl = new FloorPlan
        {
            FloorNumber = 0,
            FloorName = "ETL",
            SvgPath = "etl",
            Rooms = new List<Room>()
            // Add ETL rooms here if needed
        };
        floorPlans.Add(etl);

        // Floor 1 - Main floor with placeholder data for Room 101
        var floor1 = new FloorPlan
        {
            FloorNumber = 1,
            FloorName = "First Floor",
            SvgPath = "floor1",
            Rooms = new List<Room>
            {
                // Room 101 - Main central space (placeholder implementation)
                new Room
                {
                    RoomNumber = "101",
                    RoomName = "Main Lecture Hall",
                    RoomType = RoomType.Classroom,
                    Description = "Large central lecture hall with seating capacity for 200+ students. Equipped with modern AV systems, projection screens, and sound amplification. Used for major department presentations, guest lectures, and large class sessions.",
                    X = 0.35, // Relative position (35% from left)
                    Y = 0.25, // Relative position (25% from top)
                    Width = 0.30, // Relative width (30% of image width)
                    Height = 0.20, // Relative height (20% of image height)
                    SquareFootage = 2797
                }
                // Additional rooms can be added here as needed
            }
        };
        floorPlans.Add(floor1);

        // Floor 2 - Placeholder (no rooms yet)
        var floor2 = new FloorPlan
        {
            FloorNumber = 2,
            FloorName = "Second Floor",
            SvgPath = "floor2",
            Rooms = new List<Room>()
        };
        floorPlans.Add(floor2);

        // Floor 3 - Placeholder (no rooms yet)
        var floor3 = new FloorPlan
        {
            FloorNumber = 3,
            FloorName = "Third Floor",
            SvgPath = "floor3",
            Rooms = new List<Room>()
        };
        floorPlans.Add(floor3);

        return floorPlans;
    }

    /// <summary>
    /// Initializes location markers for all floors (ETL, Floor 1, Floor 2, Floor 3).
    /// This is a fixed location for now, but could be made dynamic in the future.
    /// </summary>
    private Dictionary<int, BuildingLocation> InitializeFloorLocations()
    {
        var locations = new Dictionary<int, BuildingLocation>();

        // ETL Location (Floor 0)
        locations[0] = new BuildingLocation
        {
            FloorNumber = 0,
            X = 0.50, // Center of the floor plan (50% from left)
            Y = 0.50, // Center of the floor plan (50% from top)
            Description = "ETL Information Point"
        };

        // Floor 1 Location
        locations[1] = new BuildingLocation
        {
            FloorNumber = 1,
            X = 0.50, // Center of the floor plan (50% from left)
            Y = 0.50, // Center of the floor plan (50% from top)
            Description = "Main Information Desk"
        };

        // Floor 2 Location
        locations[2] = new BuildingLocation
        {
            FloorNumber = 2,
            X = 0.50, // Center of the floor plan (50% from left)
            Y = 0.50, // Center of the floor plan (50% from top)
            Description = "Second Floor Information Point"
        };

        // Floor 3 Location
        locations[3] = new BuildingLocation
        {
            FloorNumber = 3,
            X = 0.50, // Center of the floor plan (50% from left)
            Y = 0.50, // Center of the floor plan (50% from top)
            Description = "Third Floor Information Point"
        };

        return locations;
    }
}

