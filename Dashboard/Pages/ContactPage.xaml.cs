using Dashboard.Models;

namespace Dashboard.Pages;

public partial class ContactPage : ContentPage
{
    public ContactPage()
    {
        InitializeComponent();
        SetContactImages();
    }

    /// <summary>
    /// Sets the profile images for the contact persons from FacultyImageConfig.
    /// </summary>
    private void SetContactImages()
    {
        // Set Dr. Srinivasan Ambatipati image
        AmbatipatiImage.Source = FacultyImageConfig.AmbatipaniImageUrl;

        // Set Sarah Reddoch image
        ReddochImage.Source = FacultyImageConfig.ReddochImageUrl;
    }
}
