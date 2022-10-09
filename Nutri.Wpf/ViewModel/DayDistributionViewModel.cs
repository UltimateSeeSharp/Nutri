using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component;
using Nutri.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class DayDistributionViewModel : BaseViewModel
{
	private readonly ProductService _productService;

	public DayDistributionViewModel(ProductService productService)
	{
		_productService = productService;
    }

    private LazyProperty<List<FoodProcuct>>? _foodProcucts = null;
    public LazyProperty<List<FoodProcuct>> FoodProcucts => _foodProcucts ??= new(GetFoodProductsAsync);
    public async Task<List<FoodProcuct>> GetFoodProductsAsync(CancellationToken cancellationToken)
    {
        return await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }

    private FoodProcuct? _selectedFoodProduct = null;
    public FoodProcuct? SelectedFoodProduct
    {
        get => _selectedFoodProduct;
        set
        {
            if (_selectedFoodProduct == value)
                return;

            _selectedFoodProduct = value;
            OnPropertyChanged();
        }
    }


    private string _searchText = String.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
                return;

            _searchText = value;
            OnPropertyChanged();

            LoadFoodProductsAsync();
        }
    }

    public async Task LoadFoodProductsAsync()
    {
        FoodProcucts.Value = await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }
}