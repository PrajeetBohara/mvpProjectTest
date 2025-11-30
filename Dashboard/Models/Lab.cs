// Code written for Lab model
namespace Dashboard.Models;

/// <summary>
/// Represents a laboratory facility in the Engineering and Computer Science department.
/// </summary>
public class Lab
{
    /// <summary>
    /// Name of the lab.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Location of the lab (e.g., "Drew 327", "ETL 100 & 101").
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Building where the lab is located (e.g., "Drew Hall", "ETL").
    /// </summary>
    public string Building { get; set; } = string.Empty;

    /// <summary>
    /// Type of lab (Teaching or Research).
    /// </summary>
    public LabType Type { get; set; }
}

/// <summary>
/// Enumeration of lab types.
/// </summary>
public enum LabType
{
    Teaching,
    Research
}

