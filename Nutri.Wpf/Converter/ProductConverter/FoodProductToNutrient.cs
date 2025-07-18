﻿using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class FoodProductToNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Nutrient> nutrients))
            return value;

        if (!nutrients.Any())
            return value;

        if (!(parameter is string param))
            return value;

        var amount = nutrients.FirstOrDefault(x => x.Name == param)!.Amount;
        return Math.Round(amount, 2);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
