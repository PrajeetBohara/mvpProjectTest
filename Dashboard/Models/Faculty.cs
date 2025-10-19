namespace Dashboard.Models;

public class Faculty
{
    public Guid Id { get; set; }
    public Guid? ProfileId { get; set; }
    public string Title { get; set; } = string.Empty; // Professor, Associate Professor, etc.
    public string? OfficeLocation { get; set; }
    public string? OfficeHours { get; set; }
    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public string[]? ResearchInterests { get; set; }
    public string[]? Education { get; set; }
    public string[]? Publications { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Profile information (from joined profiles table)
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Department { get; set; }
    public string? Role { get; set; }
}

public class FacultyProfile
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
    public string Role { get; set; } = string.Empty;
    public string? Department { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
