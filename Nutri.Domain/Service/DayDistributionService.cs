using Nutri.Domain.Enum;
using Nutri.Domain.Extentions;
using Nutri.Domain.Model;
using System.Runtime.CompilerServices;

namespace Nutri.Domain.Service;

public class DayDistributionService
{
    private List<FoodPortion> _foodPortions = new();
    private readonly ProductService _productService;

    private DateTime _currentDate => DateTime.Now;

    public DayDistributionService(ProductService productService)
    {
        _productService = productService;

        Seed();
    }

    public async Task<List<FoodPortion>> GetFoodPortions(Timeframe timeframe = Timeframe.Total, DayTimeframe? dayTimeframe = null, string? search = null)
    {
        return await Task.Factory.StartNew(() =>
        {
            if (timeframe is Timeframe.Total && string.IsNullOrEmpty(search))
                return _foodPortions;

            if (!string.IsNullOrEmpty(search))
                return _foodPortions.Where(x => x.FoodProduct.Name.ToLower().Contains(search.ToLower())).ToList();

            List<FoodPortion>? portionsInTime = null;

            switch (timeframe)
            {
                case Timeframe.Today:
                    portionsInTime = _foodPortions.Where(x => x.Timestamp.IsToday()).ToList();
                    break;

                case Timeframe.Last7Days:
                    portionsInTime = _foodPortions.Where(x => x.Timestamp.IsLast7Days()).ToList();
                    break;

                case Timeframe.ThisMonth:
                    portionsInTime = _foodPortions.Where(x => x.Timestamp.IsThisMonth()).ToList();
                    break;

                case Timeframe.ThisYear:
                    portionsInTime = _foodPortions.Where(x => x.Timestamp.IsThisYear()).ToList();
                    break;

                default:
                    portionsInTime = _foodPortions;
                    break;
            }

            //switch (dayTimeframe)
            //{
            //    case DayTimeframe.Morning:
            //        portionsInTime = portionsInTime.Where(x => x.Timestamp.Hour > 5)
            //        break;
            //
            //    default:
            //        break;
            //}

            return portionsInTime;

            //else if (allMorning)
            //{
            //    var test = portions.Where(x => x.Timestamp.Hour > 5 && x.Timestamp.Hour < 11).ToList();
            //    return test;
            //}

//            //else if (allLunch)
            //{
            //    var test = portions.Where(x => x.Timestamp.Hour > 12 && x.Timestamp.Hour < 14 && x.Timestamp.Minute < 30).ToList();
            //    return test;
            //}

//            //else if (allDinner)
            //    return portions.Where(x => x.Timestamp.Hour > 17 && x.Timestamp.Minute > 30 && x.Timestamp.Hour <= 21).ToList();

//            //else if (snacks)
            //    return portions.Where(x
            //        => x.Timestamp.Hour > 0
            //        && x.Timestamp.Hour < 5
            //        && x.Timestamp.Hour > 11
            //        && x.Timestamp.Hour < 12
            //        && x.Timestamp.Hour > 14 && x.Timestamp.Minute > 30
            //        && x.Timestamp.Hour < 17 && x.Timestamp.Minute > 30
            //        && x.Timestamp.Hour > 21).ToList();

//            //else
            //    return new();
        });
    }

    public List<FoodPortion> GetTodaysFoodPortions(bool allMorning = false, bool allLunch = false, bool allDinner = false)
    {
        var portionsToday = _foodPortions.Where(x => x.Timestamp.Year == _currentDate.Year
                                     && x.Timestamp.Month == _currentDate.Month
                                     && x.Timestamp.Day == _currentDate.Day).ToList();

        if (allMorning)
        {
            var test = portionsToday.Where(x => x.Timestamp.Hour > 5 && x.Timestamp.Hour < 12).ToList();
            return test;
        }

        if (allLunch)
        {
            var test = portionsToday.Where(x => x.Timestamp.Hour > 12 && x.Timestamp.Hour < 14).ToList();
            return test;
        }

        if (allDinner)
            return portionsToday.Where(x => x.Timestamp.Hour > 18 && x.Timestamp.Hour <= 20).ToList();

        return _foodPortions;
    }

    public void Add(FoodPortion foodPortion)
    {
        _foodPortions.Add(foodPortion);
    }

    private void Seed()
    {
        Random r = new();
        var products = _productService.GetFoodProcucts(loadAll: true);

        for (int x = 0; x < 9; x++)
        {
            FoodPortion foodPortion = new();
            foodPortion.FoodProduct = products[r.Next(0, products.Count - 1)];
            foodPortion.Amount = r.Next(80, 100);
            foodPortion.Timestamp = DateTime.Now.AddHours(r.Next(-1, 15));

            Add(foodPortion);
        }
    }
}