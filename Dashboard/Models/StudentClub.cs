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
    /// URL to the club's logo image from Supabase Storage.
    /// </summary>
    public string LogoUrl { get; set; } = string.Empty;

    /// <summary>
    /// List of gallery image URLs from Supabase Storage (images/clubs folder).
    /// </summary>
    public List<string> GalleryImageUrls { get; set; } = new();

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
    /// Meeting time (e.g., "6:00 PM", "Bi-Weekly", "Wednesdays Biweekly, Wednesday 03:00 PM").
    /// </summary>
    public string MeetingTime { get; set; } = string.Empty;

    /// <summary>
    /// Meeting location (e.g., "Drew 125", "DREW 317 (HACKERSPACE)").
    /// </summary>
    public string MeetingLocation { get; set; } = string.Empty;

    /// <summary>
    /// Year the organization was established.
    /// </summary>
    public int? YearEstablished { get; set; }

    /// <summary>
    /// Organizational mission statement.
    /// </summary>
    public string Mission { get; set; } = string.Empty;

    /// <summary>
    /// Organizational vision statement.
    /// </summary>
    public string Vision { get; set; } = string.Empty;

    /// <summary>
    /// Organizational values (comma-separated or formatted string).
    /// </summary>
    public string Values { get; set; } = string.Empty;

    /// <summary>
    /// Website URL (optional).
    /// </summary>
    public string WebsiteUrl { get; set; } = string.Empty;

    /// <summary>
    /// Facebook page URL.
    /// </summary>
    public string FacebookUrl { get; set; } = string.Empty;

    /// <summary>
    /// Instagram profile URL.
    /// </summary>
    public string InstagramUrl { get; set; } = string.Empty;

    /// <summary>
    /// LinkedIn page URL.
    /// </summary>
    public string LinkedInUrl { get; set; } = string.Empty;

    /// <summary>
    /// Twitter/X profile URL.
    /// </summary>
    public string TwitterUrl { get; set; } = string.Empty;
}

