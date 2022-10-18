using System;
using System.Globalization;
using System.Windows.Data;

namespace Nutri.Wpf.Converter;

public class SuffixConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(parameter is string param))
            return value;

        if (value is int intValue)
            return intValue.ToString() + param;

        if (!(value is double val))
            return value;

        return val + " " + param;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}