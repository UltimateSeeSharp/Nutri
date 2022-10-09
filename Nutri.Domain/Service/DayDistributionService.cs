using Nutri.Domain.Model;

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

        for (int x = 0; x < 18; x++)
        {
            FoodPortion foodPortion = new();
            foodPortion.FoodProduct = products[r.Next(0, products.Count - 1)];
            foodPortion.Amount = r.Next(80, 100);
            foodPortion.Timestamp = DateTime.Now.AddHours(r.Next(-4, 12));

            Add(foodPortion);
        }
    }
}