using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Nutris.Wpf.Converter;

public class IngredientToNutrients : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Ingredient> ingredients))
            return value;

        if (!(parameter is string pararm))
            return value;

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
