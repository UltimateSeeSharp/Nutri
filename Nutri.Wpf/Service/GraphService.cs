using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Nutri.Domain.Model;
using System;
using System.Linq;
using System.Windows.Media;

namespace Nutri.Wpf.Service;

public class GraphService
{
    public SeriesCollection CalorieDistributionPiChart(params FoodProcuct[] foodProcuct)
    {
        var proteinCalories = foodProcuct.ToList().Sum(x => x.Nutrients.FirstOrDefault(x => x.Name == "Protein")!.Amount * 4);
        var fatCalories = foodProcuct.ToList().Sum(x => x.Nutrients.FirstOrDefault(x => x.Name == "Total lipid (fat)")!.Amount * 9);
        var carbohydratesTotalCalories = foodProcuct.ToList().Sum(x => x.Nutrients.FirstOrDefault(x => x.Name == "Carbohydrate, by difference")!.Amount * 4);

        var sugarCalories = foodProcuct.ToList().Sum(x => x.Nutrients.FirstOrDefault(x => x.Name == "Sugars, total including NLEA")!.Amount * 4);
        var carbohydratesCalories = carbohydratesTotalCalories - sugarCalories;

        var totalCalories = proteinCalories + carbohydratesCalories + fatCalories;
        var calories1Percent = totalCalories / 100;

        var proteinPercent = Math.Round(proteinCalories / calories1Percent);
        var carbohydratesPercent = Math.Round(carbohydratesCalories / calories1Percent);
        var sugarPercent = Math.Round(sugarCalories / calories1Percent);
        var fatPercent = Math.Round(fatCalories / calories1Percent);

        return new()
        {
            new PieSeries
            {
                Title = "Kohlenhydrate",
                Values = new ChartValues<ObservableValue> { new ObservableValue(carbohydratesPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 217, 77))
            },
            new PieSeries
            {
                Title = "Zucker",
                Values = new ChartValues<ObservableValue> { new ObservableValue(sugarPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromArgb(160, 7, 217, 77))
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
                Values = new ChartValues<ObservableValue> { new ObservableValue(proteinPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 102, 51))
            },
        };
    }

    public SeriesCollection CalorieDistributionPiChart(params FoodPortion[] foodProcuct)
    {
        var proteinCalories = foodProcuct.ToList().Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == "Protein").Amount * 4);
        var fatCalories = foodProcuct.ToList().Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == "Total lipid (fat)").Amount * 9);

        var carbohydratesTotalCalories = foodProcuct.ToList().Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == "Carbohydrate, by difference").Amount * 4);

        var sugarCalories = foodProcuct.ToList().Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == "Sugars, total including NLEA").Amount * 4);
        var carbohydratesCalories = carbohydratesTotalCalories - sugarCalories;

        var totalCalories = proteinCalories + carbohydratesCalories + fatCalories;
        var calories1Percent = totalCalories / 100;

        var proteinPercent = Math.Round(proteinCalories / calories1Percent);
        var carbohydratesPercent = Math.Round(carbohydratesCalories / calories1Percent);
        var sugarPercent = Math.Round(sugarCalories / calories1Percent);
        var fatPercent = Math.Round(fatCalories / calories1Percent);

        return new()
        {
            new PieSeries
            {
                Title = "Kohlenhydrate",
                Values = new ChartValues<ObservableValue> { new ObservableValue(carbohydratesPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 217, 77))
            },
            new PieSeries
            {
                Title = "Zucker",
                Values = new ChartValues<ObservableValue> { new ObservableValue(sugarPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromArgb(160, 7, 217, 77))
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
                Values = new ChartValues<ObservableValue> { new ObservableValue(proteinPercent) },
                DataLabels = true,
                Fill = new SolidColorBrush(Color.FromRgb(7, 102, 51))
            },
        };
    }
}