// Code written for FirstImageConverter to get first image from list
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Dashboard.Converters;

/// <summary>
/// Converter to get the first image URL from a list of image URLs.
/// Returns an ImageSource that handles Maui asset files.
/// </summary>
public class FirstImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is List<string> imageUrls && imageUrls.Count > 0)
        {
            var firstImage = imageUrls[0];
            return CreateImageSource(firstImage);
        }

        return "mcneeselogo.png"; // Default placeholder
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static ImageSource CreateImageSource(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return "mcneeselogo.png";
        }

        // Handle Maui asset files (stored in Resources/degree_catalogue/**)
        if (imagePath.StartsWith("degree_catalogue/", StringComparison.OrdinalIgnoreCase) ||
            imagePath.StartsWith("degree_catalogue\\", StringComparison.OrdinalIgnoreCase))
        {
            var normalizedPath = imagePath.Replace("\\", "/");

            return new StreamImageSource
            {
                Stream = token => FileSystem.OpenAppPackageFileAsync(normalizedPath)
            };
        }

        // Default behavior for standard Maui images
        return imagePath;
    }
}

