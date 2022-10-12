using Nutri.Domain.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.PortionConverter;

public class FoodPortionToNutrientWithUnit : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<FoodPortion> foodPortion))
            return value;

        if (!(parameter is string param))
            return value;

        Nutrient nutrient = foodPortion.FoodProduct.Nutrients.First(x => x.Name == param);

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}