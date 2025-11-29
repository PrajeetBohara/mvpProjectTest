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
        var degreeTypes = _programService.GetDegreeTypes();
        
        foreach (var degreeType in degreeTypes)
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
        // Degree Type Header
        var headerFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#003087"),
            CornerRadius = 12,
            Padding = 15,
            HasShadow = true,
            Margin = new Thickness(0, 0, 0, 10)
        };

        var headerLabel = new Label
        {
            Text = GetDegreeTypeDisplayName(degreeType),
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
            HorizontalOptions = LayoutOptions.Start
        };

        headerFrame.Content = headerLabel;
        ProgramsContainer.Children.Add(headerFrame);

        // Programs List
        foreach (var program in programs)
        {
            var programFrame = new Frame
            {
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
                CornerRadius = 10,
                Padding = 15,
                HasShadow = false,
                Margin = new Thickness(0, 0, 0, 8)
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
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            // Arrow Icon
            var arrowLabel = new Label
            {
                Text = "›",
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
                VerticalOptions = LayoutOptions.Center
            };

            programLayout.Children.Add(nameLabel);
            programLayout.Children.Add(arrowLabel);

            programFrame.Content = programLayout;
            ProgramsContainer.Children.Add(programFrame);
        }
    }

    /// <summary>
    /// Gets a user-friendly display name for degree types.
    /// </summary>
    private string GetDegreeTypeDisplayName(string degreeType)
    {
        return degreeType switch
        {
            "BS" => "Bachelor of Science (BS)",
            "BSChE" => "Bachelor of Science in Chemical Engineering (BSChE)",
            "BSME" => "Bachelor of Science in Mechanical Engineering (BSME)",
            "Dual Degree" => "Dual Degree (Baccalaureate + Master's)",
            "Minor" => "Minor",
            "MEng" => "Master of Engineering (MEng)",
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

        var result = await DisplayAlert(
            "Program Information",
            $"Program: {program.FullName}\n\nURL: {program.Url}\n\nWould you like to open this URL in your browser?",
            "Open URL",
            "Copy URL"
        );

        if (result)
        {
            // Open URL
            try
            {
                await Microsoft.Maui.ApplicationModel.Launcher.Default.OpenAsync(program.Url);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not open URL.\n\nError: {ex.Message}", "OK");
            }
        }
        else
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
