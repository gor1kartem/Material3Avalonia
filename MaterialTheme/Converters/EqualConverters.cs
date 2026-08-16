using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MaterialTheme.Converters;
public class MultiEqualConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2)
            return true;

        var first = values[0];

        for (int i = 1; i < values.Count; i++)
        {
            var current = values[i];

            bool equal = first == null ? current == null : first.Equals(current);
            if (!equal)
                return false;
        }

        return true;
    }
}

public class MultiNotEqualConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2)
            return false;

        var first = values[0];

        for (int i = 1; i < values.Count; i++)
        {
            var current = values[i];

            bool equal = first == null ? current == null : first.Equals(current);
            if (!equal)
                return true; // нашли хотя бы одну пару, которая отличается
        }

        return false; // все значения равны
    }
}