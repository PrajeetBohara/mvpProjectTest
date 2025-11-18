// Code written for StudentClub model
namespace Dashboard.Models;

/// <summary>
/// Represents a student club or organization.
/// </summary>
public class StudentClub
{
    /// <summary>
    /// Unique identifier for the club.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Short name/acronym of the club (e.g., "ACM", "ASME").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the club.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// URL or path to the club's logo/image.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Description of the club and its activities.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Contact information (email, advisor name, etc.).
    /// </summary>
    public string ContactInfo { get; set; } = string.Empty;

    /// <summary>
    /// Meeting times and location.
    /// </summary>
    public string MeetingInfo { get; set; } = string.Empty;

    /// <summary>
    /// Website or social media links.
    /// </summary>
    public string WebsiteUrl { get; set; } = string.Empty;
}

