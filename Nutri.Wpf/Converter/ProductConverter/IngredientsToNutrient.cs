using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class IngredientsToNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Nutrient> foodProducts))
            return value;

        if (!(parameter is string param))
            return value;

        Nutrient nutrient = foodProducts.FirstOrDefault(x => x.Name.ToLower() == param.ToLower());

        if (nutrient is null)
            return value;

        return nutrient.Amount;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}