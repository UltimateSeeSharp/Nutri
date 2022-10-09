using Nutri.Domain.Model;
using Nutri.Domain.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nutri.Wpf.Converter.ProductConverter;

public class IngredientsToNrvPercent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is List<Nutrient> nutrients))
            return value;

        if (!(parameter is string param))
            return value;

        var nrvService = Bootstrapper.Resolve<NrvService>();

        Nutrient nutrient = nutrients.FirstOrDefault(x => x.Name.ToLower() == param.ToLower())!;
        NrvModel nrvModel = nrvService.GetNrv(param)!;

        if (nutrient is null || nrvModel is null)
            return value;

        if (nrvModel.Nrv is 0)
            return "-";

        var result = nutrient.Amount / (nrvModel.Nrv / 100);

        return Math.Round(result, 1).ToString() + " %";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}