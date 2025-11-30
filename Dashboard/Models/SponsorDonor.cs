// Code written for Sponsor/Donor model
namespace Dashboard.Models;

/// <summary>
/// Represents a sponsor or donor supporting the Engineering and Computer Science department.
/// </summary>
public class SponsorDonor
{
    /// <summary>
    /// Name of the sponsor or donor.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the support provided.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Type of sponsor/donor (Corporate or Individual/Organization).
    /// </summary>
    public SponsorDonorType Type { get; set; }
}

/// <summary>
/// Enumeration of sponsor/donor types.
/// </summary>
public enum SponsorDonorType
{
    Corporate,
    Individual,
    Organization
}

