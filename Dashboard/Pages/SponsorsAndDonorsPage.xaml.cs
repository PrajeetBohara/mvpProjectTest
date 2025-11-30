// Code written for Sponsors and Donors Page functionality
using Dashboard.Models;
using Dashboard.Services;
using Microsoft.Maui.Controls;

namespace Dashboard.Pages;

/// <summary>
/// Control behind the SponsorsAndDonorsPage xaml file.
/// Displays information about corporate sponsors and other donors.
/// </summary>
public partial class SponsorsAndDonorsPage : ContentPage
{
    private readonly SponsorDonorService _sponsorDonorService;

    public SponsorsAndDonorsPage(SponsorDonorService sponsorDonorService)
    {
        InitializeComponent();
        _sponsorDonorService = sponsorDonorService;
        LoadSponsorsAndDonors();
    }

    /// <summary>
    /// Loads and displays all sponsors and donors.
    /// </summary>
    private void LoadSponsorsAndDonors()
    {
        // Load Corporate Sponsors
        var corporateSponsors = _sponsorDonorService.GetCorporateSponsors();
        foreach (var sponsor in corporateSponsors)
        {
            CorporateSponsorsContainer.Children.Add(CreateSponsorDonorCard(sponsor));
        }

        // Load Other Donors
        var otherDonors = _sponsorDonorService.GetOtherDonors();
        foreach (var donor in otherDonors)
        {
            OtherDonorsContainer.Children.Add(CreateSponsorDonorCard(donor));
        }

        // Load Gallery Images
        var galleryImages = _sponsorDonorService.GetGalleryImages();
        foreach (var image in galleryImages)
        {
            GalleryContainer.Children.Add(CreateGalleryImageCard(image));
        }
    }

    /// <summary>
    /// Creates a card for displaying sponsor/donor information.
    /// </summary>
    private Frame CreateSponsorDonorCard(SponsorDonor sponsorDonor)
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

        // Sponsor/Donor Name
        var nameLabel = new Label
        {
            Text = sponsorDonor.Name,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFD204"),
            LineBreakMode = LineBreakMode.WordWrap
        };

        // Description
        var descriptionLabel = new Label
        {
            Text = sponsorDonor.Description,
            FontSize = 14,
            TextColor = Colors.White,
            LineBreakMode = LineBreakMode.WordWrap
        };

        cardLayout.Children.Add(nameLabel);
        cardLayout.Children.Add(descriptionLabel);

        cardFrame.Content = cardLayout;
        return cardFrame;
    }

    /// <summary>
    /// Creates a large frame card for displaying gallery images with descriptions.
    /// </summary>
    private Frame CreateGalleryImageCard(SponsorDonorImage image)
    {
        var cardFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
            CornerRadius = 12,
            Padding = 0,
            HasShadow = true,
            Margin = new Thickness(0, 0, 0, 0)
        };

        var cardLayout = new VerticalStackLayout
        {
            Spacing = 0
        };

        // Large Image
        var imageControl = new Image
        {
            Source = image.ImageUrl,
            Aspect = Aspect.AspectFill,
            HeightRequest = 400,
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#001122")
        };

        // Description Frame
        var descriptionFrame = new Frame
        {
            BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#002a54"),
            CornerRadius = 0,
            Padding = 20,
            HasShadow = false,
            Margin = 0
        };

        var descriptionLabel = new Label
        {
            Text = image.Description,
            FontSize = 14,
            TextColor = Colors.White,
            LineBreakMode = LineBreakMode.WordWrap
        };

        descriptionFrame.Content = descriptionLabel;

        cardLayout.Children.Add(imageControl);
        cardLayout.Children.Add(descriptionFrame);

        cardFrame.Content = cardLayout;
        return cardFrame;
    }
}

