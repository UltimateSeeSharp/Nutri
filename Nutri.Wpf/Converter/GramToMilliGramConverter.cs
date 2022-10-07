using System;
using System.Globalization;
using System.Windows.Data;

namespace Nutri.Wpf.Converter;

internal class GramToMilliGramConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is double val))
            return value;

        var test = val;

        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
