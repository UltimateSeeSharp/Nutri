using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Nutri.Domain.Enum;
using Nutri.Domain.Model;
using Nutri.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Nutri.Wpf.Service;

public class GraphService
{
    private readonly DayDistributionService _dayDistrbutionService;

    public GraphService(DayDistributionService dayDistrbutionService)
    {
        _dayDistrbutionService = dayDistrbutionService;
    }

    public AxesCollection GetXAxes(Timeframe timeframe)
    {
        AxesCollection axesCollection = new();

        switch(timeframe)
        {
            case Timeframe.Last7Days:
                axesCollection.Add(new Axis()
                {
                    Labels = new string[]
                    {
                        DateTime.Now.Day.ToString() + " " + DateTime.Now.Month.ToString(),
                        DateTime.Now.AddDays(-1).Day.ToString() + " " + DateTime.Now.AddDays(-1).Month.ToString(),
                        DateTime.Now.AddDays(-2).Day.ToString() + " " + DateTime.Now.AddDays(-2).Month.ToString(),
                        DateTime.Now.AddDays(-3).Day.ToString() + " " + DateTime.Now.AddDays(-3).Month.ToString(),
                        DateTime.Now.AddDays(-4).Day.ToString() + " " + DateTime.Now.AddDays(-4).Month.ToString(),
                        DateTime.Now.AddDays(-5).Day.ToString() + " " + DateTime.Now.AddDays(-5).Month.ToString(),
                        DateTime.Now.AddDays(-6).Day.ToString() + " " + DateTime.Now.AddDays(-6).Month.ToString(),
                    }
                });
                break;
        }

        return axesCollection;
    }

    public SeriesCollection GetNutrientHistory(List<FoodPortion> foodPortions, string nutrientName, Timeframe timeframe, DayTimeframe? dayTimeFrame = null)
    {
        SeriesCollection? hisorySeries = new();

        switch (timeframe)
        {
            case Timeframe.Last7Days:
                hisorySeries.Add(new LineSeries()
                {
                    Values = new ChartValues<double>()
                    {
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-6).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-5).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-4).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-3).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-2).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.AddDays(-1).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date == DateTime.Now.Date).ToList(), nutrientName),
                    }
                });
                break;

            case Timeframe.ThisMonth:

                hisorySeries.Add(new LineSeries()
                {
                    Values = new ChartValues<double>()
                    {
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date > DateTime.Now.AddDays(-7).Date && x.Timestamp.Date <= DateTime.Now.Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date > DateTime.Now.AddDays(-14).Date && x.Timestamp.Date <= DateTime.Now.AddDays(-7).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date > DateTime.Now.AddDays(-21).Date && x.Timestamp.Date <= DateTime.Now.AddDays(-14).Date).ToList(), nutrientName),
                        GetSumOfNutrient(foodPortions.Where(x => x.Timestamp.Date > DateTime.Now.AddDays(-28).Date && x.Timestamp.Date <= DateTime.Now.AddDays(-21).Date).ToList(), nutrientName)
                    }
                });

                break;
            case Timeframe.ThisYear:
                break;
            case Timeframe.Total:
                break;
            case Timeframe.Today:
                break;
            default:
                break;
        }

        double GetSumOfNutrient(List<FoodPortion> foodPortions, string nutrientName)
            => foodPortions is not null
            ? foodPortions.Sum(x => x.FoodProduct.Nutrients.First(x => x.Name == nutrientName).Amount)
            : 0;

        return hisorySeries;
    }

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