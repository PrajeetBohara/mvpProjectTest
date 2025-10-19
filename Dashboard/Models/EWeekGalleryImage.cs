namespace Dashboard.Models;

/// <summary>
/// Model representing an E-Week gallery image stored in Supabase
/// </summary>
public class EWeekGalleryImage
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; }
}
