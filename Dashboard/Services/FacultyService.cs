/// Code written by Jael Ruiz
/// 
/// This file defines a service that interacts with Supabase to fetch faculty
/// and staff data for display in the Dashboard. It provides methods for retrieving
/// all active faculty members, department heads, and key staff members.
/// It also includes a connection test method for debugging purposes.

using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

/// <summary>
/// Service responsible for fetching and organizing faculty-related data from Supabase.
/// Provides methods for retrieving all faculty, department heads, and key staff members.
/// </summary>
public class FacultyService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="FacultyService"/> class.
    /// Sets up the HTTP client and authentication headers needed for Supabase API requests.
    /// </summary>
    public FacultyService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        // Add necessary Supabase headers for authorization
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    /// <summary>
    /// Retrieves all active faculty members from the database.
    /// Includes joined profile information such as name, email, and department.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Faculty"/> objects representing all active faculty members.
    /// Returns an empty list if none are found or an error occurs.
    /// </returns>
    public async Task<List<Faculty>> GetAllFacultyAsync()
    {
        try
        {
            // Query joins faculty and profile data for complete details
            var url = $"{_supabaseUrl}/rest/v1/faculty?select=*,profiles(full_name,email,avatar_url,department,role)&is_active=eq.true&order=title.asc";
            System.Diagnostics.Debug.WriteLine($"Fetching faculty from: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Faculty API Response: {response}");

            var facultyData = JsonSerializer.Deserialize<List<FacultyData>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            System.Diagnostics.Debug.WriteLine($"Deserialized {facultyData?.Count ?? 0} faculty members");

            var faculty = new List<Faculty>();

            if (facultyData != null)
            {
                foreach (var item in facultyData)
                {
                    faculty.Add(new Faculty
                    {
                        Id = item.Id,
                        ProfileId = item.ProfileId,
                        Title = item.Title ?? string.Empty,
                        OfficeLocation = item.OfficeLocation,
                        OfficeHours = item.OfficeHours,
                        Phone = item.Phone,
                        Bio = item.Bio,
                        ResearchInterests = item.ResearchInterests,
                        Education = item.Education,
                        Publications = item.Publications,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        FullName = item.Profiles?.FullName,
                        Email = item.Profiles?.Email,
                        AvatarUrl = item.Profiles?.AvatarUrl,
                        Department = item.Profiles?.Department,
                        Role = item.Profiles?.Role
                    });
                }
            }

            return faculty;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching faculty: {ex.Message}");
            return new List<Faculty>();
        }
    }

    /// <summary>
    /// Retrieves active faculty members with leadership titles such as
    /// "Head", "Director", or "Chair". Used to display department heads or program leaders.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Faculty"/> objects representing department heads.
    /// Returns an empty list if none are found or an error occurs.
    /// </returns>
    public async Task<List<Faculty>> GetDepartmentHeadsAsync()
    {
        try
        {
            // Filter for leadership-related titles using "ilike" (case-insensitive match)
            var url = $"{_supabaseUrl}/rest/v1/faculty?select=*,profiles(full_name,email,avatar_url,department,role)&is_active=eq.true&or=(title.ilike.%head%,title.ilike.%director%,title.ilike.%chair%)&order=title.asc";
            System.Diagnostics.Debug.WriteLine($"Fetching department heads from: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Department heads API Response: {response}");

            var facultyData = JsonSerializer.Deserialize<List<FacultyData>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var faculty = new List<Faculty>();

            if (facultyData != null)
            {
                foreach (var item in facultyData)
                {
                    faculty.Add(new Faculty
                    {
                        Id = item.Id,
                        ProfileId = item.ProfileId,
                        Title = item.Title ?? string.Empty,
                        OfficeLocation = item.OfficeLocation,
                        OfficeHours = item.OfficeHours,
                        Phone = item.Phone,
                        Bio = item.Bio,
                        ResearchInterests = item.ResearchInterests,
                        Education = item.Education,
                        Publications = item.Publications,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        FullName = item.Profiles?.FullName,
                        Email = item.Profiles?.Email,
                        AvatarUrl = item.Profiles?.AvatarUrl,
                        Department = item.Profiles?.Department,
                        Role = item.Profiles?.Role
                    });
                }
            }

            return faculty;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching department heads: {ex.Message}");
            return new List<Faculty>();
        }
    }

    /// <summary>
    /// Retrieves active faculty members who are not department heads.
    /// These are typically teaching or support staff without leadership titles.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Faculty"/> objects representing key staff members.
    /// Returns an empty list if none are found or an error occurs.
    /// </returns>
    public async Task<List<Faculty>> GetKeyStaffAsync()
    {
        try
        {
            // Exclude faculty members with leadership-related titles
            var url = $"{_supabaseUrl}/rest/v1/faculty?select=*,profiles(full_name,email,avatar_url,department,role)&is_active=eq.true&not.and=(title.ilike.%head%,title.ilike.%director%,title.ilike.%chair%)&order=title.asc";
            System.Diagnostics.Debug.WriteLine($"Fetching key staff from: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Key staff API Response: {response}");

            var facultyData = JsonSerializer.Deserialize<List<FacultyData>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var faculty = new List<Faculty>();

            if (facultyData != null)
            {
                foreach (var item in facultyData)
                {
                    faculty.Add(new Faculty
                    {
                        Id = item.Id,
                        ProfileId = item.ProfileId,
                        Title = item.Title ?? string.Empty,
                        OfficeLocation = item.OfficeLocation,
                        OfficeHours = item.OfficeHours,
                        Phone = item.Phone,
                        Bio = item.Bio,
                        ResearchInterests = item.ResearchInterests,
                        Education = item.Education,
                        Publications = item.Publications,
                        IsActive = item.IsActive,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                        FullName = item.Profiles?.FullName,
                        Email = item.Profiles?.Email,
                        AvatarUrl = item.Profiles?.AvatarUrl,
                        Department = item.Profiles?.Department,
                        Role = item.Profiles?.Role
                    });
                }
            }

            return faculty;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching key staff: {ex.Message}");
            return new List<Faculty>();
        }
    }

    /// <summary>
    /// Tests the connection to the Supabase faculty table.
    /// Useful for confirming that the service is properly configured.
    /// </summary>
    /// <returns>
    /// True if the connection is successful, false if an error occurs.
    /// </returns>
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/faculty?limit=1";
            System.Diagnostics.Debug.WriteLine($"Testing faculty connection to: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Faculty connection test response: {response}");

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Faculty connection test failed: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Internal helper class for deserializing faculty JSON data.
    /// Represents the shape of the data returned by Supabase including nested profile info.
    /// </summary>
    private class FacultyData
    {
        public Guid Id { get; set; }
        public Guid? ProfileId { get; set; }
        public string? Title { get; set; }
        public string? OfficeLocation { get; set; }
        public string? OfficeHours { get; set; }
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public string[]? ResearchInterests { get; set; }
        public string[]? Education { get; set; }
        public string[]? Publications { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public FacultyProfile? Profiles { get; set; }
    }
}
