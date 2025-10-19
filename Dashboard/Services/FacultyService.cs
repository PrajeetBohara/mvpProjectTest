using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

public class FacultyService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public FacultyService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    public async Task<List<Faculty>> GetAllFacultyAsync()
    {
        try
        {
            // Join faculty with profiles table to get complete information
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

    public async Task<List<Faculty>> GetDepartmentHeadsAsync()
    {
        try
        {
            // Get faculty with titles that suggest department leadership
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

    public async Task<List<Faculty>> GetKeyStaffAsync()
    {
        try
        {
            // Get faculty excluding department heads
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

    // Helper classes for JSON deserialization
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
