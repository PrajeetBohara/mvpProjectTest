/// Code written by Jael Ruiz
/// This file defines a service responsible for fetching Engineering Week (E-Week)
/// gallery images from the Supabase backend. The service provides methods for 
/// retrieving images by year, category, featured status, and recency.

using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

/// <summary>
/// Handles communication with Supabase to retrieve E-Week gallery data.
/// Provides methods to fetch images by year, category, and other filters.
/// </summary>
public class EWeekGalleryService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    /// <summary>
    /// Initializes the EWeekGalleryService and sets up HTTP headers for Supabase access.
    /// </summary>
    public EWeekGalleryService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        // Add authorization headers required for Supabase REST API calls
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    /// <summary>
    /// Retrieves all gallery images for a specific E-Week year.
    /// </summary>
    /// <param name="year">The E-Week year (e.g., 2024, 2025).</param>
    /// <returns>
    /// A list of <see cref="EWeekGalleryImage"/> objects ordered by display order.
    /// Returns an empty list if no data is found or an error occurs.
    /// </returns>
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
    /// Retrieves featured gallery images for a specific year.
    /// Featured images are typically highlighted on the main gallery or homepage.
    /// </summary>
    /// <param name="year">The E-Week year (e.g., 2024, 2025).</param>
    /// <returns>
    /// A list of featured <see cref="EWeekGalleryImage"/> objects ordered by display order.
    /// Returns an empty list if none are found or an error occurs.
    /// </returns>
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
    /// Retrieves gallery images filtered by category for a given year.
    /// </summary>
    /// <param name="year">The E-Week year (e.g., 2025).</param>
    /// <param name="category">
    /// The image category to filter by, such as:
    /// "events", "winners", "projects", "ceremony", or "general".
    /// </param>
    /// <returns>
    /// A list of images that belong to the specified category.
    /// Returns an empty list if no matching data is found or an error occurs.
    /// </returns>
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
            System.Diagnostics.Debug.WriteLine($"Error fetching images by category '{category}' for year {year}: {ex.Message}");
            return new List<EWeekGalleryImage>();
        }
    }

    /// <summary>
    /// Retrieves the most recent gallery images for a specific year.
    /// This can be used to populate "recent uploads" or "latest highlights" sections.
    /// </summary>
    /// <param name="year">The E-Week year.</param>
    /// <param name="limit">The maximum number of images to return (default is 6).</param>
    /// <returns>
    /// A list of the most recently created <see cref="EWeekGalleryImage"/> objects.
    /// Returns an empty list if no data is found or an error occurs.
    /// </returns>
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
    /// Tests the connection to Supabase to ensure the gallery table exists and is accessible.
    /// Useful for debugging connection issues or verifying configuration.
    /// </summary>
    /// <returns>
    /// True if the connection is successful; otherwise, false.
    /// </returns>
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
