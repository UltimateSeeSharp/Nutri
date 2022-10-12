using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class FoodPortionsToTotalNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<FoodPortion> foodPortions))
            return value;

        if (!(parameter is string param))
            return value;

        var nutrientSum = foodPortions.Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == param).Amount);
        return Math.Round(nutrientSum, 2) + " " + foodPortions.First().FoodProduct.Nutrients.First(x => x.Name == param).Unit;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
