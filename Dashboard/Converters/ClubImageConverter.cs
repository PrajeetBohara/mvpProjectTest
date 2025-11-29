// Code for Club Image Converter
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Dashboard.Converters;

/// <summary>
/// Converter that returns LogoUrl if available, otherwise falls back to ImageUrl.
/// </summary>
public class ClubImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Models.StudentClub club)
        {
            // Return LogoUrl if available, otherwise ImageUrl, otherwise default logo
            if (!string.IsNullOrEmpty(club.LogoUrl))
            {
                return club.LogoUrl;
            }
            if (!string.IsNullOrEmpty(club.ImageUrl))
            {
                return club.ImageUrl;
            }
            return "mcneeselogo.png"; // Default fallback
        }
        return "mcneeselogo.png";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

