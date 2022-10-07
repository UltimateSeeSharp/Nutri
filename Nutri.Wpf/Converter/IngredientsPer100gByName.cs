using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutris.Wpf.Converter;

public class IngredientsPer100gByName : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Ingredient> ingredients))
            return value;

        if (!(parameter is string pararm))
            return value;

        if (pararm == "Water")
            return ingredients.Where(x => x.Name == pararm).Select(x => x.Amount + " %").FirstOrDefault()!;

        return ingredients.Where(x => x.Name == pararm)
                          .Select(x => x.Amount + " " + x.Unit)
                          .FirstOrDefault() + " / 100g"!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
