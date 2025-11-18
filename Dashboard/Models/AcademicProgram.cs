// Code written for AcademicProgram model
namespace Dashboard.Models;

/// <summary>
/// Represents an academic program or degree track.
/// </summary>
public class AcademicProgram
{
    /// <summary>
    /// Unique identifier for the program.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the program (e.g., "Industrial CS", "General CS").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Full name/description of the program.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Department/Section this program belongs to (e.g., "Computer Science").
    /// </summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// List of image URLs for the program catalogue.
    /// </summary>
    public List<string> ImageUrls { get; set; } = new List<string>();

    /// <summary>
    /// Description of the program.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

