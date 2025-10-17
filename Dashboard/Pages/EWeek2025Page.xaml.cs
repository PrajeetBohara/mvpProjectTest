using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// EWeek2025Page for displaying E-Week 2025 events, competitions, and activities.
/// This page shows the current year's E-Week information.
/// </summary>
public partial class EWeek2025Page : ContentPage
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the EWeek2025Page.
    /// </summary>
    public EWeek2025Page()
    {
        InitializeComponent();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the back button click event.
    /// Navigates back to the main E-Week page.
    /// </summary>
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//E-Week");
    }
    #endregion
}
