using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

/// <summary>
/// Service for fetching E-Week gallery images from Supabase
/// </summary>
public class EWeekGalleryService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public EWeekGalleryService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;
        
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    /// <summary>
    /// Get all gallery images for a specific year
    /// </summary>
    /// <param name="year">The E-Week year (e.g., 2024, 2025)</param>
    /// <returns>List of gallery images ordered by display order</returns>
    public async Task<List<EWeekGalleryImage>> GetGalleryImagesAsync(int year)
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/eweek_gallery?year=eq.{year}&order=display_order.asc";
            System.Diagnostics.Debug.WriteLine($"Fetching gallery images from: {url}");
            
            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"API Response: {response}");
            
            var images = JsonSerializer.Deserialize<List<EWeekGalleryImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            System.Diagnostics.Debug.WriteLine($"Deserialized {images?.Count ?? 0} images");
            return images ?? new List<EWeekGalleryImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching gallery images for year {year}: {ex.Message}");
            return new List<EWeekGalleryImage>();
        }
    }

    /// <summary>
    /// Get featured gallery images for a specific year
    /// </summary>
    /// <param name="year">The E-Week year (e.g., 2024, 2025)</param>
    /// <returns>List of featured gallery images</returns>
    public async Task<List<EWeekGalleryImage>> GetFeaturedImagesAsync(int year)
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/eweek_gallery?year=eq.{year}&is_featured=eq.true&order=display_order.asc";
            System.Diagnostics.Debug.WriteLine($"Fetching featured images from: {url}");
            
            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Featured API Response: {response}");
            
            var images = JsonSerializer.Deserialize<List<EWeekGalleryImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            System.Diagnostics.Debug.WriteLine($"Deserialized {images?.Count ?? 0} featured images");
            return images ?? new List<EWeekGalleryImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching featured images for year {year}: {ex.Message}");
            return new List<EWeekGalleryImage>();
        }
    }

    /// <summary>
    /// Get images by category for a specific year
    /// </summary>
    /// <param name="year">The E-Week year</param>
    /// <param name="category">Category to filter by (events, winners, projects, ceremony, general)</param>
    /// <returns>List of gallery images in the specified category</returns>
    public async Task<List<EWeekGalleryImage>> GetImagesByCategoryAsync(int year, string category)
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/eweek_gallery?year=eq.{year}&category=eq.{category}&order=display_order.asc";
            var response = await _httpClient.GetStringAsync(url);
            var images = JsonSerializer.Deserialize<List<EWeekGalleryImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return images ?? new List<EWeekGalleryImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching images by category {category} for year {year}: {ex.Message}");
            return new List<EWeekGalleryImage>();
        }
    }

    /// <summary>
    /// Get a limited number of recent gallery images for a specific year
    /// </summary>
    /// <param name="year">The E-Week year</param>
    /// <param name="limit">Maximum number of images to return</param>
    /// <returns>List of recent gallery images</returns>
    public async Task<List<EWeekGalleryImage>> GetRecentImagesAsync(int year, int limit = 6)
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/eweek_gallery?year=eq.{year}&order=created_at.desc&limit={limit}";
            var response = await _httpClient.GetStringAsync(url);
            var images = JsonSerializer.Deserialize<List<EWeekGalleryImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return images ?? new List<EWeekGalleryImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching recent images for year {year}: {ex.Message}");
            return new List<EWeekGalleryImage>();
        }
    }

    /// <summary>
    /// Test method to verify database connection and table existence
    /// </summary>
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/eweek_gallery?limit=1";
            System.Diagnostics.Debug.WriteLine($"Testing connection to: {url}");
            
            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Connection test response: {response}");
            
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Connection test failed: {ex.Message}");
            return false;
        }
    }
}
