using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class FoodProductToNutrientWithUnit : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Nutrient> nutrients))
            return value;

        if (!nutrients.Any())
            return value;

        if (!(parameter is string param))
            return value;

        var nutrient = nutrients.FirstOrDefault(x => x.Name == param);

        return Math.Round(nutrient.Amount, 2).ToString() + " " + nutrient.Unit;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
