using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class IngredientsToNutrientAndUnit : IValueConverter
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

        if (nutrient.Amount < 1)
        {
            var amount = nutrient.Amount * 1000;

            if (nutrient.Unit == "g")
                return amount + " mg";

            else if (nutrient.Unit == "mg")
                return amount + " mg";
        }

        return nutrient.Amount + " " + nutrient.Unit;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
