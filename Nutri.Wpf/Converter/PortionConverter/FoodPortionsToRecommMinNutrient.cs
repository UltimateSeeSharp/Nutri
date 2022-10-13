using Nutri.Domain.Model;
using Nutri.Domain.Service;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.PortionConverter;

public class FoodPortionsToRecommMinNutrient : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<FoodPortion> foodPortions))
            return value;

        if (!(parameter is string param))
            return value;

        var nrvSerice = Bootstrapper.Resolve<NrvService>();
        var nrvModel = nrvSerice.GetNrv(param);

        return nrvModel.Nrv * 0.8;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}