using Nutri.Domain.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class FoodProductToNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is FoodProcuct foodProcuct))
            return value;

        if (!(parameter is string param))
            return value;

        var protein = foodProcuct.Nutrients.FirstOrDefault(x => x.Name == param)!.Amount;
        var grade = protein / 4;

        if (grade < 1)
            return 1;

        else if (grade > 5)
            return 5;

        else
            return Math.Round(grade);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
