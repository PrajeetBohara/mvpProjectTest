namespace Dashboard.Models;

/// <summary>
/// Configuration for faculty image alignment within their circular frames.
/// Allows manual positioning of images to ensure faces/subjects are properly centered.
/// </summary>
public static class FacultyImageAlignment
{
    // ============================================
    // IMAGE ALIGNMENT SETTINGS
    // ============================================
    // Options: "Center", "Start", "End", "Fill"
    // For HorizontalOptions: Start = Left, End = Right
    // For VerticalOptions: Start = Top, End = Bottom
    
    // Admin Section
    public static readonly (string Horizontal, string Vertical) AmbatipatiAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) ZhangAdminAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) OBrienAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) BennettAlignment = ("Center", "Center");
    
    // Coordinators Section
    public static readonly (string Horizontal, string Vertical) AghiliAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) DermisisAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) GarnerAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) LiAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) MenonCoordinatorAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) SubramaniamAlignment = ("Center", "Center");
    
    // Faculty Section
    public static readonly (string Horizontal, string Vertical) AmbatipatiFacultyAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) AndersonAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) GuoAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) LavergneFacultyAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) LiuAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) MenonFacultyAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) RostiAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) XieAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) ZhangFacultyAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) ZeitounAlignment = ("Center", "Center");
    public static readonly (string Horizontal, string Vertical) SinghAlignment = ("Center", "Center");
    
    /// <summary>
    /// Helper method to convert string alignment to LayoutOptions
    /// </summary>
    public static LayoutOptions GetHorizontalOptions(string alignment)
    {
        return alignment switch
        {
            "Start" => LayoutOptions.Start,
            "End" => LayoutOptions.End,
            "Fill" => LayoutOptions.Fill,
            "Center" => LayoutOptions.Center,
            _ => LayoutOptions.Center
        };
    }
    
    public static LayoutOptions GetVerticalOptions(string alignment)
    {
        return alignment switch
        {
            "Start" => LayoutOptions.Start,
            "End" => LayoutOptions.End,
            "Fill" => LayoutOptions.Fill,
            "Center" => LayoutOptions.Center,
            _ => LayoutOptions.Center
        };
    }
}

