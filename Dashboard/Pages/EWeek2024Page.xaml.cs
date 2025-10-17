using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// EWeek2024Page for displaying E-Week 2024 archive with past events, winners, and memorable moments.
/// This page shows the previous year's E-Week information and achievements.
/// </summary>
public partial class EWeek2024Page : ContentPage
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the EWeek2024Page.
    /// </summary>
    public EWeek2024Page()
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
