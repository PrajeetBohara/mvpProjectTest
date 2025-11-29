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
    /// Degree type (e.g., "BS", "BSChE", "BSME", "Minor", "MEng", "Dual Degree").
    /// </summary>
    public string DegreeType { get; set; } = string.Empty;

    /// <summary>
    /// URL to the program's page on the website.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Description of the program.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

