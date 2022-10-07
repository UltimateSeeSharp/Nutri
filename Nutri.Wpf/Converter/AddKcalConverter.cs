using System;
using System.Globalization;
using System.Windows.Data;

namespace Nutris.Wpf.Converter;

public class AddKcalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is double doubleValue))
            return value;

        return doubleValue.ToString() + " kcal";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
