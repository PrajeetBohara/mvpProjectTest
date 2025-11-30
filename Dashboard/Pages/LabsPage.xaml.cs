// Code written for Labs Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the LabsPage xaml file.
/// Displays all laboratory facilities organized by building.
/// </summary>
public partial class LabsPage : ContentPage
{
    private readonly LabService _labService;

    public LabsPage(LabService labService)
    {
        InitializeComponent();
        _labService = labService;
        LoadLabs();
    }

    /// <summary>
    /// Loads and displays all labs organized by building.
    /// </summary>
    private void LoadLabs()
    {
        // Define the order we want to display buildings
        var orderedBuildings = new List<string> { "Drew Hall", "ETL" };

        foreach (var building in orderedBuildings)
        {
            var labs = _labService.GetLabsByBuilding(building);
            if (labs.Count > 0)
            {
                CreateBuildingSection(building, labs);
            }
        }
    }

    /// <summary>
    /// Creates a section for a specific building with its labs.
    /// </summary>
    private void CreateBuildingSection(string building, List<Lab> labs)
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

        // Building Header
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
            Text = building,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
            HorizontalOptions = LayoutOptions.Start
        };

        headerFrame.Content = headerLabel;
        sectionStack.Children.Add(headerFrame);

        // Labs Container
        var labsStack = new VerticalStackLayout
        {
            Spacing = 12,
            Padding = new Thickness(20, 20, 20, 20)
        };

        // Labs List - Display in a grid (2 columns)
        var labsGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            },
            ColumnSpacing = 15,
            RowSpacing = 12
        };

        int row = 0;
        int col = 0;
        foreach (var lab in labs)
        {
            var labCard = CreateLabCard(lab);
            labsGrid.Add(labCard, col, row);
            
            col++;
            if (col >= 2)
            {
                col = 0;
                row++;
            }
        }

        labsStack.Children.Add(labsGrid);
        sectionStack.Children.Add(labsStack);
        sectionFrame.Content = sectionStack;
        LabsContainer.Children.Add(sectionFrame);
    }

    /// <summary>
    /// Creates a card for displaying lab information.
    /// </summary>
    private Frame CreateLabCard(Lab lab)
    {
        var cardFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
            CornerRadius = 10,
            Padding = 18,
            HasShadow = false,
            Margin = new Thickness(0, 0, 0, 0)
        };

        var cardLayout = new VerticalStackLayout
        {
            Spacing = 8
        };

        // Lab Name
        var nameLabel = new Label
        {
            Text = lab.Name,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            LineBreakMode = LineBreakMode.WordWrap
        };

        // Lab Location
        var locationLabel = new Label
        {
            Text = lab.Location,
            FontSize = 14,
            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
            LineBreakMode = LineBreakMode.WordWrap
        };

        cardLayout.Children.Add(nameLabel);
        cardLayout.Children.Add(locationLabel);

        cardFrame.Content = cardLayout;
        return cardFrame;
    }
}
