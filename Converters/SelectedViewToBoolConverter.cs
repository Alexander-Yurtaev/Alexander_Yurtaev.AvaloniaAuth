using Alexander_Yurtaev.AvaloniaAuth.Enums;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Alexander_Yurtaev.AvaloniaAuth.Converters;

public class SelectedViewToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!Enum.TryParse<SelectedViewModel>(value?.ToString(), out var selectedView))
        {
            return null;
        }

        if (!Enum.TryParse<SelectedViewModel>(parameter?.ToString(), out var targetSelectedView))
        {
            return null;
        }

        return selectedView == targetSelectedView;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!bool.TryParse(value?.ToString(), out var boolValue))
        {
            return null;
        }
        
        if (boolValue != true)
        {
            return false;
        }

        if (!Enum.TryParse<SelectedViewModel>(parameter?.ToString(), out var targetSelectedView))
        {
            return null;
        }

        return targetSelectedView;
    }
}
