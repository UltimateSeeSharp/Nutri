using Newtonsoft.Json;
using Nutri.Domain.Model;

namespace Nutri.Domain.Service;

public class ProductService
{
    private List<FoodProcuct>? _foodProcucts = null;

    public async Task<List<FoodProcuct>> GetFoodProcuctsAsync(bool loadAll = false, int length = 100, string? search = null)
    {
        return await Task.Factory.StartNew(() =>
        {
            if (_foodProcucts is null)
            {
                _foodProcucts = new();

                var productsJson = File.ReadAllText("foodProducts.json");
                var products = JsonConvert.DeserializeObject<List<FoodProcuct>>(productsJson);

                if (products is not null)
                    _foodProcucts.AddRange(products);
            }

            if (loadAll && string.IsNullOrEmpty(search))
                return _foodProcucts;

            if (!string.IsNullOrEmpty(search))
                return _foodProcucts.Where(x => x.Name.ToLower().Contains(search.ToLower())).Take(length).ToList();

            return _foodProcucts.Take(length).ToList();
        });
    }

    public List<FoodProcuct> GetFoodProcucts(bool loadAll = false, int length = 100, string? search = null)
    {
        if (_foodProcucts is null)
        {
            _foodProcucts = new();

            string productsJson = File.ReadAllText("foodProducts.json");

            var products = JsonConvert.DeserializeObject<List<FoodProcuct>>(productsJson);

            if (products is not null)
                _foodProcucts.AddRange(products);
        }

        if (loadAll && string.IsNullOrEmpty(search))
            return _foodProcucts;

        if (!string.IsNullOrEmpty(search))
            return _foodProcucts.Where(x => x.Name.ToLower().Contains(search.ToLower())).Take(length).ToList();

        return _foodProcucts.Take(length).ToList();
    }
}