using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutris.Wpf.Converter;

public class WaterConsumptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Ingredient> ingredients))
            return value;

        if (!(parameter is string param))
            return value;

        if (param == "NaN")
            return "NaN";

        var nrvService = Bootstrapper.Resolve<NrvService>();
        var nrvs = nrvService.GetNrvs();

        var ingredient = ingredients.FirstOrDefault(x => x.Name == param);
        var nrv = nrvs.FirstOrDefault(x => x.Name == param);

        if(ingredient is null || nrv is null)
            return value;

        var nrv1Percent = nrv.Nrv / 100;

        return Math.Round(ingredient.Amount / nrv1Percent).ToString() + " %";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
