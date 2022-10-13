using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.PortionConverter;

public class FoodPortionsToNutrientWithUnit : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<FoodPortion> foodPortions))
            return value;

        if (!(parameter is string param))
            return value;

        var unit = foodPortions.First().FoodProduct.Nutrients.First(x => x.Name == param).Unit;
        var sum = foodPortions.Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == param).Amount);

        return Math.Round(sum, 2) + " " + unit;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}