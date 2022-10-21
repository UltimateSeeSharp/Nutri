using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component;
using Nutri.Wpf.Component.ExpanderList;
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
			OnPropertyChanged(nameof(NutritionExpander));
			OnPropertyChanged(nameof(FattyAcidExpander));
			OnPropertyChanged(nameof(MineralsTraceElementsExpander));
			OnPropertyChanged(nameof(VitaminsExpander));
			OnPropertyChanged(nameof(OtherExpander));
			OnPropertyChanged(nameof(AntioxidantExpander));
		}
	}

	public CaloriesDistributionChart CaloriesDistributionChart => new() { FoodProduct = SelectedFoodProduct! };
    public NutritionExpander? NutritionExpander => new() { FoodProduct = SelectedFoodProduct! };
	public FattyAcidExpander? FattyAcidExpander => new() { FoodProduct = SelectedFoodProduct!, UserSetting = _userService.CurrentUserSetting };
	public MineralsTraceElementsExpander? MineralsTraceElementsExpander => new() { FoodProduct = SelectedFoodProduct! };
	public VitaminsExpander? VitaminsExpander => new() { FoodProduct = SelectedFoodProduct! };
	public AntioxidantExpander? AntioxidantExpander => new() { FoodProduct = SelectedFoodProduct! };
	public OtherExpander? OtherExpander => new() { FoodProduct = SelectedFoodProduct! };

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