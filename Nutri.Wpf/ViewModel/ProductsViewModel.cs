using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class ProductsViewModel : BaseViewModel
{
	private readonly ProductService _productService;
	private readonly UserService _userService;
	private readonly GraphService _graphService;

	public ProductsViewModel(ProductService productService, UserService userService, GraphService graphService)
	{
		_productService = productService;
        _userService = userService;
		_graphService = graphService;
    }

	private LazyProperty<List<FoodProcuct>>? _foodProcucts = null;
	public LazyProperty<List<FoodProcuct>> FoodProcucts => _foodProcucts ??= new(GetFoodProductsAsync);

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

			OnPropertyChanged(nameof(IsFoodProductSeleted));

			OnPropertyChanged(nameof(CaloriesDistributionChart));
			OnPropertyChanged(nameof(NutritionTable));
			OnPropertyChanged(nameof(FattyAcidTable));
			OnPropertyChanged(nameof(MineralsTraceElementsTable));
			OnPropertyChanged(nameof(VitaminsTable));
			OnPropertyChanged(nameof(OtherTable));
		}
	}

	public CaloriesDistributionChart CaloriesDistributionChart => new() { FoodProduct = SelectedFoodProduct! };
    public NutrientTable? NutritionTable => new() { FoodProduct = SelectedFoodProduct! };
	public FattyAcidTable? FattyAcidTable => new() { FoodProduct = SelectedFoodProduct!, UserSetting = _userService.CurrentUserSetting };
	public MineralsTraceElementsTable? MineralsTraceElementsTable => new() { FoodProduct = SelectedFoodProduct! };
	public VitaminsTable? VitaminsTable => new() { FoodProduct = SelectedFoodProduct! };
	public OtherTable? OtherTable => new() { FoodProduct = SelectedFoodProduct! };

	public bool IsFoodProductSeleted => SelectedFoodProduct is not null;

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


    public async Task<List<FoodProcuct>> GetFoodProductsAsync(CancellationToken cancellationToken)
    {
        return await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }

    public async Task LoadFoodProductsAsync()
    {
        FoodProcucts.Value = await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }
}