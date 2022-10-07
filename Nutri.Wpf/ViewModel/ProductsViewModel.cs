using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class ProductsViewModel : BaseViewModel
{
	private readonly ProductService _productService;

	public ProductsViewModel(ProductService productService)
	{
		_productService = productService;
    }

	private LazyProperty<List<FoodProcuct>>? _foodProcucts = null;
	public LazyProperty<List<FoodProcuct>> FoodProcucts => _foodProcucts ??= new(LoadFoodProductsAsync);

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
			OnPropertyChanged(nameof(FoodProcucts));
		}
	}


    public async Task<List<FoodProcuct>> LoadFoodProductsAsync(CancellationToken cancellationToken)
    {
        return await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }
}