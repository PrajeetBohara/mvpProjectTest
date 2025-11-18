// Code written for Maps page functionality
namespace Dashboard.Pages;

/// <summary>
/// Control behind the MapsPage xaml file.
/// Provides navigation to Department Map and Campus Map options.
/// </summary>
public partial class MapsPage : ContentPage
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the MapsPage.
    /// Sets up the maps selection page with two main options.
    /// </summary>
    public MapsPage()
    {
        InitializeComponent();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles Department Map option tap.
    /// Navigates to the interactive department map showing engineering building floor plans.
    /// </summary>
    private async void OnDepartmentMapTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//DepartmentMap");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening department map: {ex.Message}");
            await DisplayAlert("Error", "Unable to open department map. Please try again later.", "OK");
        }
    }

    /// <summary>
    /// Handles Campus Map option tap.
    /// Navigates to the campus map showing all buildings and amenities.
    /// </summary>
    private async void OnCampusMapTapped(object sender, EventArgs e)
    {
        try
        {
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//CampusMap");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error opening campus map: {ex.Message}");
            await DisplayAlert("Error", "Unable to open campus map. Please try again later.", "OK");
        }
    }
    #endregion
}

