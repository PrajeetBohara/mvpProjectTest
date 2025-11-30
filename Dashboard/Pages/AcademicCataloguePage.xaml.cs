// Code written for Academic Catalogue Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the AcademicCataloguePage xaml file.
/// Displays academic programs organized by degree type.
/// </summary>
public partial class AcademicCataloguePage : ContentPage
{
    private readonly AcademicProgramService _programService;

    public AcademicCataloguePage(AcademicProgramService programService)
    {
        InitializeComponent();
        _programService = programService;
        LoadPrograms();
    }

    /// <summary>
    /// Loads and displays all programs organized by degree type.
    /// </summary>
    private void LoadPrograms()
    {
        // Define the order we want to display sections
        var orderedDegreeTypes = new List<string> 
        { 
            "Bachelor's Degrees", 
            "Minors", 
            "Master's Degrees" 
        };
        
        foreach (var degreeType in orderedDegreeTypes)
        {
            var programs = _programService.GetProgramsByDegreeType(degreeType);
            if (programs.Count > 0)
            {
                CreateDegreeTypeSection(degreeType, programs);
            }
        }
    }

    /// <summary>
    /// Creates a section for a specific degree type with its programs.
    /// </summary>
    private void CreateDegreeTypeSection(string degreeType, List<AcademicProgram> programs)
    {
        // Main Section Frame
        var sectionFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#003087"),
            CornerRadius = 16,
            Padding = 0,
            HasShadow = true,
            Margin = new Thickness(0, 0, 0, 25)
        };

        var sectionStack = new VerticalStackLayout { Spacing = 0 };

        // Degree Type Header
        var headerFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
            CornerRadius = 0,
            Padding = 20,
            HasShadow = false,
            Margin = 0
        };

        var headerLabel = new Label
        {
            Text = GetDegreeTypeDisplayName(degreeType),
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
            HorizontalOptions = LayoutOptions.Start
        };

        headerFrame.Content = headerLabel;
        sectionStack.Children.Add(headerFrame);

        // Programs Container
        var programsStack = new VerticalStackLayout 
        { 
            Spacing = 10, 
            Padding = new Thickness(15, 15, 15, 15)
        };

        // Programs List
        foreach (var program in programs)
        {
            var programFrame = new Frame
            {
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
                CornerRadius = 10,
                Padding = 18,
                HasShadow = false,
                Margin = new Thickness(0, 0, 0, 0)
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnProgramTapped(program);
            programFrame.GestureRecognizers.Add(tapGesture);

            var programLayout = new HorizontalStackLayout
            {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Center
            };

            // Program Name
            var nameLabel = new Label
            {
                Text = program.FullName,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.WordWrap
            };

            // Arrow Icon
            var arrowLabel = new Label
            {
                Text = "›",
                FontSize = 28,
                FontAttributes = FontAttributes.Bold,
                TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };

            programLayout.Children.Add(nameLabel);
            programLayout.Children.Add(arrowLabel);

            programFrame.Content = programLayout;
            programsStack.Children.Add(programFrame);
        }

        sectionStack.Children.Add(programsStack);
        sectionFrame.Content = sectionStack;
        ProgramsContainer.Children.Add(sectionFrame);
    }

    /// <summary>
    /// Gets a user-friendly display name for degree types.
    /// </summary>
    private string GetDegreeTypeDisplayName(string degreeType)
    {
        return degreeType switch
        {
            "Bachelor's Degrees" => "Bachelor's Degrees",
            "Minors" => "Minors",
            "Master's Degrees" => "Master's Degrees",
            _ => degreeType
        };
    }

    /// <summary>
    /// Handles program tap to show URL.
    /// </summary>
    private async void OnProgramTapped(AcademicProgram program)
    {
        if (string.IsNullOrEmpty(program.Url))
        {
            await DisplayAlert("Program Information", 
                $"Program: {program.FullName}\n\nURL not available. Please contact the department for more information.", 
                "OK");
            return;
        }

        var result = await DisplayActionSheet(
            $"Program: {program.FullName}",
            "Cancel",
            null,
            "Open URL in Browser",
            "Copy URL to Clipboard"
        );

        if (result == "Open URL in Browser")
        {
            // Open URL
            try
            {
                await Microsoft.Maui.ApplicationModel.Launcher.Default.OpenAsync(new Uri(program.Url));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not open URL.\n\nError: {ex.Message}", "OK");
            }
        }
        else if (result == "Copy URL to Clipboard")
        {
            // Copy URL to clipboard
            try
            {
                await Clipboard.Default.SetTextAsync(program.Url);
                await DisplayAlert("Success", "URL copied to clipboard!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not copy URL.\n\nError: {ex.Message}", "OK");
            }
        }
    }
}
