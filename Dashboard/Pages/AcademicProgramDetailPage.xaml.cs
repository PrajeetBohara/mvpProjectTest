// Code written for Academic Program Detail Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the AcademicProgramDetailPage xaml file.
/// Displays detailed information about a selected academic program with image navigation.
/// </summary>
[QueryProperty(nameof(ProgramId), "programId")]
public partial class AcademicProgramDetailPage : ContentPage
{
    private readonly AcademicProgramService _programService;
    private AcademicProgram? _currentProgram;
    private string _programId = string.Empty;
    private int _currentImageIndex = 0;

    public AcademicProgramDetailPage(AcademicProgramService programService)
    {
        InitializeComponent();
        _programService = programService;
    }

    /// <summary>
    /// Gets or sets the program ID from query parameters.
    /// </summary>
    public string ProgramId
    {
        get => _programId;
        set
        {
            System.Diagnostics.Debug.WriteLine($"=== ProgramId property set ===");
            System.Diagnostics.Debug.WriteLine($"Received value: '{value}'");
            _programId = value ?? string.Empty;
            LoadProgramDetails();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (string.IsNullOrEmpty(_programId))
        {
            LoadProgramDetails();
        }
    }

    /// <summary>
    /// Loads program details from query parameters.
    /// </summary>
    private void LoadProgramDetails()
    {
        System.Diagnostics.Debug.WriteLine($"=== LoadProgramDetails called ===");
        System.Diagnostics.Debug.WriteLine($"Current _programId: '{_programId}'");
        
        // Get program ID from query parameters
        if (!string.IsNullOrEmpty(_programId) && int.TryParse(_programId, out var programId))
        {
            System.Diagnostics.Debug.WriteLine($"Parsed programId: {programId}");
            _currentProgram = _programService.GetProgramById(programId);
            if (_currentProgram != null)
            {
                System.Diagnostics.Debug.WriteLine($"Found program: {_currentProgram.Name}");
                DisplayProgramDetails(_currentProgram);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"✗ No program found with ID: {programId}");
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"✗ Could not parse programId from: '{_programId}'");
            
            // Fallback: try to get from Shell query
            if (Shell.Current?.CurrentState?.Location is not null)
            {
                var location = Shell.Current.CurrentState.Location;
                System.Diagnostics.Debug.WriteLine($"Shell location: {location}");
                var queryString = location.Query;
                System.Diagnostics.Debug.WriteLine($"Query string: '{queryString}'");
                
                if (!string.IsNullOrEmpty(queryString))
                {
                    // Parse query string manually (format: ?programId=1)
                    var parts = queryString.TrimStart('?').Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2 && keyValue[0] == "programId" && int.TryParse(keyValue[1], out var id))
                        {
                            System.Diagnostics.Debug.WriteLine($"Found programId in query: {id}");
                            _currentProgram = _programService.GetProgramById(id);
                            if (_currentProgram != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found program: {_currentProgram.Name}");
                                DisplayProgramDetails(_currentProgram);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("✗ Shell.Current or CurrentState is null");
            }
        }
    }

    /// <summary>
    /// Displays the program details in the UI.
    /// </summary>
    private void DisplayProgramDetails(AcademicProgram program)
    {
        ProgramNameLabel.Text = program.Name;
        ProgramFullNameLabel.Text = program.FullName;
        DescriptionLabel.Text = program.Description;
        
        // Initialize image carousel - start with first image (index 0)
        _currentImageIndex = 0;
        
        // Update UI immediately
        UpdateImageDisplay();
        
        // Debug output
        System.Diagnostics.Debug.WriteLine($"Displaying program: {program.Name}");
        // Note: ImageUrls removed - this page is deprecated in favor of URL-based navigation
    }

    /// <summary>
    /// Updates the displayed image and navigation controls.
    /// </summary>
    private async void UpdateImageDisplay()
    {
        // Note: Image functionality removed - this page is deprecated
        // Programs now show URLs directly in AcademicCataloguePage
        if (_currentProgram == null)
        {
            ProgramImage.Source = "mcneeselogo.png";
            ImageCounterLabel.Text = "Program information";
            PreviousImageButton.IsVisible = false;
            NextImageButton.IsVisible = false;
            return;
        }

        // Show default logo since images are removed
        ProgramImage.Source = "mcneeselogo.png";
        ImageCounterLabel.Text = "Images removed - see URL in main catalogue";
        PreviousImageButton.IsVisible = false;
        NextImageButton.IsVisible = false;
        System.Diagnostics.Debug.WriteLine($"Program: {_currentProgram.FullName}");
        // Note: All image loading code removed - this page is deprecated in favor of URL-based navigation
    }

    /// <summary>
    /// Handles previous image button click.
    /// </summary>
    private void OnPreviousImageClicked(object? sender, EventArgs e)
    {
        // Note: Image functionality removed - this page is deprecated
        return;
    }

    /// <summary>
    /// Handles next image button click.
    /// </summary>
    private void OnNextImageClicked(object? sender, EventArgs e)
    {
        NavigateToNextImage();
    }

    /// <summary>
    /// Handles image tap to navigate to next image.
    /// </summary>
    private void OnImageTapped(object? sender, TappedEventArgs e)
    {
        NavigateToNextImage();
    }

    /// <summary>
    /// Navigates to the next image in the carousel.
    /// </summary>
    private void NavigateToNextImage()
    {
        // Note: Image functionality removed - this page is deprecated
        return;
    }

    /// <summary>
    /// Handles back button click to navigate back to catalogue.
    /// </summary>
    private async void OnBackButtonClicked(object? sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//AcademicCatalogue");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating back to catalogue: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates an ImageSource that supports Maui assets and bundled images.
    /// Loads images from local assets folder.
    /// </summary>
    private async Task<ImageSource> CreateImageSourceAsync(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            System.Diagnostics.Debug.WriteLine("Image path is empty, using placeholder");
            return "mcneeselogo.png";
        }

        // Handle degree catalogue assets - these are stored locally in Resources/degree_catalogue/
        if (imagePath.StartsWith("degree_catalogue/", StringComparison.OrdinalIgnoreCase) ||
            imagePath.StartsWith("degree_catalogue\\", StringComparison.OrdinalIgnoreCase))
        {
            var normalizedPath = imagePath.Replace("\\", "/");
            System.Diagnostics.Debug.WriteLine($"Loading LOCAL asset from: {normalizedPath}");
            
            try
            {
                // Open the asset file from the app package
                using var stream = await FileSystem.OpenAppPackageFileAsync(normalizedPath);
                
                if (stream == null)
                {
                    System.Diagnostics.Debug.WriteLine($"✗ Stream is null for: {normalizedPath}");
                    return "mcneeselogo.png";
                }
                
                System.Diagnostics.Debug.WriteLine($"✓ Successfully opened local file: {normalizedPath}, Size: {stream.Length} bytes");
                
                // Read the stream into a byte array
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();
                
                // Create ImageSource from bytes
                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                System.Diagnostics.Debug.WriteLine($"✓ ImageSource created successfully from local file");
                
                return imageSource;
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ FileNotFoundException: {normalizedPath}");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                
                // Try alternative path formats
                var alternatives = new[]
                {
                    normalizedPath.Replace("degree_catalogue/", ""),
                    normalizedPath.ToLower(),
                    normalizedPath.Replace("/", "\\")
                };
                
                foreach (var altPath in alternatives)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"Trying alternative path: {altPath}");
                        using var altStream = await FileSystem.OpenAppPackageFileAsync(altPath);
                        if (altStream != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"✓ Found at alternative path: {altPath}");
                            using var memoryStream = new MemoryStream();
                            await altStream.CopyToAsync(memoryStream);
                            return ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                        }
                    }
                    catch (Exception altEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Alternative path {altPath} also failed: {altEx.Message}");
                    }
                }
                
                System.Diagnostics.Debug.WriteLine($"✗ All path attempts failed for: {normalizedPath}");
                return "mcneeselogo.png";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Error loading local asset: {normalizedPath}");
                System.Diagnostics.Debug.WriteLine($"Error type: {ex.GetType().Name}");
                System.Diagnostics.Debug.WriteLine($"Error message: {ex.Message}");
                return "mcneeselogo.png";
            }
        }

        // Regular image resource (from Resources/Images/)
        System.Diagnostics.Debug.WriteLine($"Loading regular image resource: {imagePath}");
        return imagePath;
    }
    
    /// <summary>
    /// Synchronous wrapper for CreateImageSourceAsync (for backward compatibility).
    /// </summary>
    private ImageSource CreateImageSource(string imagePath)
    {
        // For asset files, we need async loading, so return a placeholder that will be updated
        if (imagePath.StartsWith("degree_catalogue/", StringComparison.OrdinalIgnoreCase) ||
            imagePath.StartsWith("degree_catalogue\\", StringComparison.OrdinalIgnoreCase))
        {
            // Return placeholder, actual loading will happen in UpdateImageDisplay
            return "mcneeselogo.png";
        }
        
        return imagePath;
    }
}

