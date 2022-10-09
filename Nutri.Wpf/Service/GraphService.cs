using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Nutri.Domain.Model;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Media;

namespace Nutri.Wpf.Service;

public class GraphService
{
    public SeriesCollection CalorieDistributionPiChart(FoodProcuct foodProcuct)
    {
        var proteinCalories = foodProcuct.Ingredients.FirstOrDefault(x => x.Name == "Protein")!.Amount * 4;
        var carbohydratesCalories = foodProcuct.Ingredients.FirstOrDefault(x => x.Name == "Carbohydrate, by difference")!.Amount * 4;
        var fatCalories = foodProcuct.Ingredients.FirstOrDefault(x => x.Name == "Total lipid (fat)")!.Amount * 9;

        var totalCalories = proteinCalories + carbohydratesCalories + fatCalories;
        var calories1Percent = totalCalories / 100;

        var proteinPercent = Math.Round(proteinCalories / calories1Percent);
        var carbohydratesPercent = Math.Round(carbohydratesCalories / calories1Percent);
        var fatPercent = Math.Round(fatCalories / calories1Percent);

        return new()
        {
            new PieSeries
            {
                Title = "Kohlenhydrate",
                Values = new ChartValues<ObservableValue> { new ObservableValue(proteinPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 217, 77))
            },
            new PieSeries
            {
                Title = "Fette",
                Values = new ChartValues<ObservableValue> { new ObservableValue(fatPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(38, 181, 91))
            },
            new PieSeries
            {
                Title = "Proteine",
                Values = new ChartValues<ObservableValue> { new ObservableValue(carbohydratesPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 102, 51))
            },
        };
    }
}