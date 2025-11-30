// Code written for Sponsor/Donor Gallery Image model
namespace Dashboard.Models;

/// <summary>
/// Represents an image in the sponsors and donors gallery.
/// </summary>
public class SponsorDonorImage
{
    /// <summary>
    /// URL of the image from Supabase backend.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Description/caption for the image.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

