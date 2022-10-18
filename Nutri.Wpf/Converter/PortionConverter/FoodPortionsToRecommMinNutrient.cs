using Nutri.Domain.Model;
using Nutri.Domain.Service;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Windows.Data;
using System.Linq;

namespace Nutri.Wpf.Converter.PortionConverter;

public class FoodPortionsToRecommMinNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<FoodPortion> foodPortions))
            return value;

        if (!(parameter is string param))
            return value;

        double result = 0;

        if (param == "Energy")
        {
            result = foodPortions.Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == "Energy").Amount);
        }
        else
        {
            var nrvSerice = Bootstrapper.Resolve<NrvService>();
            var nrvModel = nrvSerice.GetNrv(param);
            result = nrvModel.Nrv;
        }

        return result * 0.8;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}