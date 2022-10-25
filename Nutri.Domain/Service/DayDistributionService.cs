using Nutri.Domain.Enum;
using Nutri.Domain.Extentions;
using Nutri.Domain.Model;
using Nutri.Domain.Model.User;

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

    public async Task<List<FoodPortion>> GetFoodPortions(Timeframe timeframe, EatingHabit? eatingHabit = null, DayTimeframe? dayTimeframe = null, string? search = null)
    {
        return await Task.Factory.StartNew(() =>
        {
            if (timeframe is Timeframe.Total && string.IsNullOrEmpty(search))
                return _foodPortions;

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

            if (dayTimeframe is null || eatingHabit is null)
                return portionsInTime;

            List<FoodPortion> todaysPortions = _foodPortions.Where(x => x.Timestamp.IsToday()).ToList();

            switch (dayTimeframe)
            {
                case DayTimeframe.Morning:
                    portionsInTime = portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.MorningStart && x.Timestamp.Hour < eatingHabit.MorningStop).ToList();
                    break;

                case DayTimeframe.Lunch:
                    portionsInTime = portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.LunchStart && x.Timestamp.Hour < eatingHabit.LunchStop).ToList();
                    break;

                case DayTimeframe.Dinner:
                    portionsInTime = portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.DinnerStart && x.Timestamp.Hour < eatingHabit.DinnerStop).ToList();
                    break;

                case DayTimeframe.Other:
                    List<FoodPortion> mainMealfoodPortions = new();
                    mainMealfoodPortions.AddRange(portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.MorningStart && x.Timestamp.Hour < eatingHabit.MorningStop).ToList());
                    mainMealfoodPortions.AddRange(portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.LunchStart && x.Timestamp.Hour < eatingHabit.LunchStop).ToList());
                    mainMealfoodPortions.AddRange(portionsInTime.Where(x => x.Timestamp.Hour > eatingHabit.DinnerStart && x.Timestamp.Hour < eatingHabit.DinnerStop).ToList());

                    mainMealfoodPortions.ForEach(x => todaysPortions.Remove(x));
                    portionsInTime = mainMealfoodPortions;
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrEmpty(search))
                return portionsInTime.Where(x => x.FoodProduct.Name.ToLower().Contains(search.ToLower())).ToList();

            return portionsInTime;
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

        for (int i = 0; i < 365; i++)
        {
            for (int x = 0; x < 9; x++)
            {
                FoodPortion foodPortion = new();
                foodPortion.FoodProduct = products[r.Next(0, products.Count - 1)];
                foodPortion.Amount = r.Next(80, 100);
                foodPortion.Timestamp = DateTime.Now.AddDays(-i).AddHours(r.Next(-1, 15));

                Add(foodPortion);
            }
        }
    }
}