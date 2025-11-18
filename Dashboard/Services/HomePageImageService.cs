// Code for fetching homepage/dashboard images from Supabase
// This service provides methods for retrieving images to display on the homepage

using Dashboard.Models;
using System.Linq;
using System.Text.Json;

namespace Dashboard.Services;

/// <summary>
/// Handles communication with Supabase to retrieve homepage/dashboard images.
/// Provides methods to fetch images for display on the homepage slideshow.
/// </summary>
public class HomePageImageService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    /// <summary>
    /// Initializes the HomePageImageService and sets up HTTP headers for Supabase access.
    /// </summary>
    public HomePageImageService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        // Add authorization headers required for Supabase REST API calls
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    #region Direct Image URL Configuration
    /// <summary>
    /// Direct image URLs from Supabase Storage.
    /// Add your image URLs here if you want to hardcode them or use as fallback.
    /// Format: https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/[filename]
    /// </summary>
    private static List<HomePageImage> GetDirectImageUrls()
    {
        return new List<HomePageImage>
        {
            // ============================================
            // ADD YOUR IMAGE URLS HERE
            // ============================================
            // Copy the Public URL from Supabase Storage (images/homepage folder)
            // and paste them below. Uncomment and modify as needed.
            
            // Example 1:
            new HomePageImage
            {
                Id = Guid.NewGuid(),
                Title = "",
                Description = "",
                ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/image1.jpg",
                DisplayOrder = 1,
                IsActive = true
            },

             //Example 2:
             new HomePageImage
             {
                 Id = Guid.NewGuid(),
                 Title = "",
                 Description = "Celebrating achievements in engineering and computer science",
                 ImageUrl = "https://kvvoooyijzvxxnejykjv.supabase.co/storage/v1/object/public/images/homepage/geauxbluelights-3-3-768x432.jpg.webp",
                 DisplayOrder = 2,
                 IsActive = true
             },
            
            // Add more images below...
            // ============================================
        };
    }
    #endregion

    /// <summary>
    /// Retrieves all active homepage images ordered by display order.
    /// First tries to fetch from database, then falls back to direct URLs if configured.
    /// </summary>
    /// <returns>
    /// A list of <see cref="HomePageImage"/> objects ordered by display order.
    /// Returns an empty list if no data is found or an error occurs.
    /// </returns>
    public async Task<List<HomePageImage>> GetHomePageImagesAsync()
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/homepage_images?is_active=eq.true&order=display_order.asc";
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Fetching homepage images from: {url}");

            var httpResponse = await _httpClient.GetAsync(url);
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Status: {httpResponse.StatusCode}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Error Response: {errorContent}");
                
                // Fallback to direct URLs on HTTP error
                var directUrls = GetDirectImageUrls();
                if (directUrls.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {directUrls.Count} direct image URLs (HTTP error: {httpResponse.StatusCode})");
                    return directUrls;
                }
                return new List<HomePageImage>();
            }

            var response = await httpResponse.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] API Response: {response}");

            // Check if response is empty or just brackets
            if (string.IsNullOrWhiteSpace(response) || response.Trim() == "[]")
            {
                System.Diagnostics.Debug.WriteLine("[HomePageImageService] Database returned empty array");
                var directUrls = GetDirectImageUrls();
                if (directUrls.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {directUrls.Count} direct image URLs (database was empty)");
                    return directUrls;
                }
                return new List<HomePageImage>();
            }

            var images = JsonSerializer.Deserialize<List<HomePageImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // If database returns images, use them
            if (images != null && images.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Successfully deserialized {images.Count} homepage images from database");
                return images;
            }

            // Fallback to direct URLs if database is empty
            var fallbackUrls = GetDirectImageUrls();
            if (fallbackUrls.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {fallbackUrls.Count} direct image URLs (database returned null or empty list)");
                return fallbackUrls;
            }

            // Fallback to an empty list so calling code does not need to guard against null values.
            System.Diagnostics.Debug.WriteLine("[HomePageImageService] No homepage images found (database empty and no direct URLs configured)");
            return new List<HomePageImage>();
        }
        catch (HttpRequestException httpEx)
        {
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Request Error: {httpEx.Message}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Stack Trace: {httpEx.StackTrace}");
            
            // On HTTP error, try to return direct URLs as fallback
            var directUrls = GetDirectImageUrls();
            if (directUrls.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {directUrls.Count} direct image URLs (HTTP request error occurred)");
                return directUrls;
            }
            
            return new List<HomePageImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] General Error fetching homepage images: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Error Type: {ex.GetType().Name}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Stack Trace: {ex.StackTrace}");
            
            // On error, try to return direct URLs as fallback
            var directUrls = GetDirectImageUrls();
            if (directUrls.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {directUrls.Count} direct image URLs (error occurred, using fallback)");
                return directUrls;
            }
            
            return new List<HomePageImage>();
        }
    }

    /// <summary>
    /// Retrieves a limited number of homepage images for quick display.
    /// First tries to fetch from database, then falls back to direct URLs if configured.
    /// </summary>
    /// <param name="limit">The maximum number of images to return (default is 5).</param>
    /// <returns>
    /// A list of active homepage images ordered by display order, limited to the specified count.
    /// Returns an empty list if no data is found or an error occurs.
    /// </returns>
    public async Task<List<HomePageImage>> GetFeaturedHomePageImagesAsync(int limit = 5)
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/homepage_images?is_active=eq.true&order=display_order.asc&limit={limit}";
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Fetching featured homepage images from: {url}");

            var httpResponse = await _httpClient.GetAsync(url);
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Status: {httpResponse.StatusCode}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Error Response: {errorContent}");
                
                // Fallback to direct URLs on HTTP error
                var directUrls = GetDirectImageUrls();
                if (directUrls.Count > 0)
                {
                    var limitedDirectUrls = directUrls.Take(limit).ToList();
                    System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {limitedDirectUrls.Count} direct image URLs (HTTP error: {httpResponse.StatusCode})");
                    return limitedDirectUrls;
                }
                return new List<HomePageImage>();
            }

            var response = await httpResponse.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Featured API Response: {response}");

            // Check if response is empty or just brackets
            if (string.IsNullOrWhiteSpace(response) || response.Trim() == "[]")
            {
                System.Diagnostics.Debug.WriteLine("[HomePageImageService] Database returned empty array for featured images");
                var directUrls = GetDirectImageUrls();
                if (directUrls.Count > 0)
                {
                    var limitedDirectUrls = directUrls.Take(limit).ToList();
                    System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {limitedDirectUrls.Count} direct image URLs (database was empty)");
                    return limitedDirectUrls;
                }
                return new List<HomePageImage>();
            }

            var images = JsonSerializer.Deserialize<List<HomePageImage>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // If database returns images, use them (limit to requested count)
            if (images != null && images.Count > 0)
            {
                var limitedImages = images.Take(limit).ToList();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Successfully deserialized {limitedImages.Count} featured homepage images from database");
                return limitedImages;
            }

            // Fallback to direct URLs if database is empty
            var fallbackUrls = GetDirectImageUrls();
            if (fallbackUrls.Count > 0)
            {
                var limitedFallbackUrls = fallbackUrls.Take(limit).ToList();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {limitedFallbackUrls.Count} direct image URLs (database returned null or empty list)");
                return limitedFallbackUrls;
            }

            // Returning an empty list keeps UI bindings simple when no images exist.
            System.Diagnostics.Debug.WriteLine("[HomePageImageService] No featured homepage images found (database empty and no direct URLs configured)");
            return new List<HomePageImage>();
        }
        catch (HttpRequestException httpEx)
        {
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] HTTP Request Error fetching featured images: {httpEx.Message}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Stack Trace: {httpEx.StackTrace}");
            
            // On HTTP error, try to return direct URLs as fallback
            var directUrls = GetDirectImageUrls();
            if (directUrls.Count > 0)
            {
                var limitedDirectUrls = directUrls.Take(limit).ToList();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {limitedDirectUrls.Count} direct image URLs (HTTP request error occurred)");
                return limitedDirectUrls;
            }
            
            return new List<HomePageImage>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] General Error fetching featured homepage images: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Error Type: {ex.GetType().Name}");
            System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Stack Trace: {ex.StackTrace}");
            
            // On error, try to return direct URLs as fallback
            var directUrls = GetDirectImageUrls();
            if (directUrls.Count > 0)
            {
                var limitedDirectUrls = directUrls.Take(limit).ToList();
                System.Diagnostics.Debug.WriteLine($"[HomePageImageService] Using {limitedDirectUrls.Count} direct image URLs (error occurred, using fallback)");
                return limitedDirectUrls;
            }
            
            return new List<HomePageImage>();
        }
    }

    /// <summary>
    /// Tests the connection to Supabase to ensure the homepage_images table exists and is accessible.
    /// Useful for debugging connection issues or verifying configuration.
    /// </summary>
    /// <returns>
    /// True if the connection is successful; otherwise, false.
    /// </returns>
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var url = $"{_supabaseUrl}/rest/v1/homepage_images?limit=1";
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

