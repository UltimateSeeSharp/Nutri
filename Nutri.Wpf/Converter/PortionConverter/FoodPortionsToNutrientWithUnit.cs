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

        var nutrient = foodPortions.Select(x => x.FoodProduct.Nutrients.First(y => y.Name == parameter))?.First();
        var nutrientSum = foodPortions.Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == parameter).Amount);

        return Math.Round(nutrientSum, 2).ToString() + " " + nutrient.Unit;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}