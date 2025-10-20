using Dashboard.Models;
using System.Text.Json;

namespace Dashboard.Services;

public class ResearchImageService
{
    private readonly HttpClient _httpClient;
    private readonly string _supabaseUrl;
    private readonly string _supabaseKey;

    public ResearchImageService()
    {
        _httpClient = new HttpClient();
        _supabaseUrl = SupabaseService.SupabaseUrl;
        _supabaseKey = SupabaseService.SupabaseAnonKey;

        _httpClient.DefaultRequestHeaders.Add("apikey", _supabaseKey);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_supabaseKey}");
    }

    public async Task<List<ResearchImage>> GetFacultyResearchImagesAsync()
    {
        try
        {
            // Get list of files from the faculty_research folder
            var url = $"{_supabaseUrl}/storage/v1/object/list/faculty_research";
            System.Diagnostics.Debug.WriteLine($"Fetching faculty research images from: {url}");

            var response = await _httpClient.GetStringAsync(url);
            System.Diagnostics.Debug.WriteLine($"Faculty research images API Response: {response}");

            var files = JsonSerializer.Deserialize<List<StorageFile>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var researchImages = new List<ResearchImage>();
            
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (IsImageFile(file.Name))
                    {
                        var imageUrl = $"{_supabaseUrl}/storage/v1/object/public/faculty_research/{file.Name}";
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

    private bool IsImageFile(string fileName)
    {
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".avif", ".svg" };
        return imageExtensions.Any(ext => fileName.ToLower().EndsWith(ext));
    }

    private string GenerateCaptionFromFileName(string fileName)
    {
        // Remove file extension and replace underscores/hyphens with spaces
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        var caption = nameWithoutExt.Replace("_", " ").Replace("-", " ");
        
        // Capitalize first letter of each word
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

// Helper class for deserializing storage file list
public class StorageFile
{
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime LastModified { get; set; }
}
