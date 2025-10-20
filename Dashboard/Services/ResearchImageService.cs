/// Code written by Jael Ruiz
/// This file defines a service for retrieving faculty research images stored in Supabase.
/// The service fetches images from the "faculty_research" storage bucket, processes
/// them into display-ready objects, and includes helper methods for validation and captioning.

using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

/// <summary>
/// Handles communication with Supabase Storage to fetch and process
/// faculty research images for display in the dashboard.
/// </summary>
public class ResearchImageService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResearchImageService"/> class.
    /// Sets up the HTTP client with Supabase authorization headers.
    /// </summary>
    public ResearchImageService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        // Add authorization headers required by Supabase REST API
        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    /// <summary>
    /// Retrieves all image files stored in the "faculty_research" bucket from Supabase.
    /// Automatically filters non-image files and generates readable captions.
    /// </summary>
    /// <returns>
    /// A list of <see cref="ResearchImage"/> objects representing faculty research visuals.
    /// Returns an empty list if no images are found or an error occurs.
    /// </returns>
    public async Task<List<ResearchImage>> GetFacultyResearchImagesAsync()
    {
        try
        {
            // Fetch a list of all files from the Supabase faculty_research storage bucket
            var url = $"{_supabaseUrl}/storage/v1/object/list/faculty_research";
            System.Diagnostics.Debug.WriteLine($"Fetching faculty research images from: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Faculty research images API Response: {response}");

            // Deserialize JSON response into a list of storage file objects
            var files = JsonSerializer.Deserialize<List<StorageFile>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var researchImages = new List<ResearchImage>();

            if (files != null)
            {
                foreach (var file in files)
                {
                    // Include only supported image file types
                    if (IsImageFile(file.Name))
                    {
                        // Construct the public URL for the image
                        var imageUrl = $"{_supabaseUrl}/storage/v1/object/public/faculty_research/{file.Name}";

                        // Generate a readable caption from the file name
                        var caption = GenerateCaptionFromFileName(file.Name);

                        researchImages.Add(new ResearchImage
                        {
                            ImageUrl = imageUrl,
                            Caption = caption,
                            FileName = file.Name
                        });
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine($"Deserialized {researchImages.Count} faculty research images");
            return researchImages;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching faculty research images: {ex.Message}");
            return new List<ResearchImage>();
        }
    }

    /// <summary>
    /// Determines whether a file name corresponds to a supported image type.
    /// </summary>
    /// <param name="fileName">The file name to check.</param>
    /// <returns>True if the file is an image, otherwise false.</returns>
    private bool IsImageFile(string fileName)
    {
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".avif", ".svg" };
        return imageExtensions.Any(ext => fileName.ToLower().EndsWith(ext));
    }

    /// <summary>
    /// Generates a human-readable caption from a given file name.
    /// Removes file extensions, replaces underscores/hyphens with spaces,
    /// and capitalizes each word for a clean display caption.
    /// </summary>
    /// <param name="fileName">The original file name.</param>
    /// <returns>A formatted caption string.</returns>
    private string GenerateCaptionFromFileName(string fileName)
    {
        // Remove the file extension and replace underscores/hyphens with spaces
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        var caption = nameWithoutExt.Replace("_", " ").Replace("-", " ");

        // Capitalize the first letter of each word for readability
        var words = caption.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (!string.IsNullOrEmpty(words[i]))
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }

        return string.Join(" ", words);
    }

    /// <summary>
    /// Tests the connection to the Supabase storage bucket.
    /// Useful for verifying that the service is properly configured and authorized.
    /// </summary>
    /// <returns>
    /// True if the connection is successful, false if an error occurs.
    /// </returns>
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var url = $"{_supabaseUrl}/storage/v1/object/list/faculty_research";
            System.Diagnostics.Debug.WriteLine($"Testing faculty research images connection to: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Faculty research images connection test response: {response}");

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Faculty research images connection test failed: {ex.Message}");
            return false;
        }
    }
}

/// <summary>
/// Represents a single file retrieved from Supabase storage.
/// Used internally for JSON deserialization before converting to a <see cref="ResearchImage"/>.
/// </summary>
public class StorageFile
{
    /// <summary>
    /// The name of the file (including its extension).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The file size in bytes.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// The date and time when the file was last modified in storage.
    /// </summary>
    public DateTime LastModified { get; set; }
}
