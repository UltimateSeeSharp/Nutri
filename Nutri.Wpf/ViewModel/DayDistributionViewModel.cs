using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component;
using Nutri.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nutri.Wpf.ViewModel;

public class DayDistributionViewModel : BaseViewModel
{
	private readonly ProductService _productService;
    private readonly DayDistributionService _dayDistributionService;

    public DayDistributionViewModel(ProductService productService, DayDistributionService dayDistributionService)
	{
		_productService = productService;
        _dayDistributionService = dayDistributionService;
    }

    private LazyProperty<List<FoodPortion>>? _todaysFoodPortions = null;
    public LazyProperty<List<FoodPortion>> TodaysFoodPortions => _todaysFoodPortions ??= new(GetTodaysFoodPortions);


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
        }
    }


    private FoodPortion _currentFoodPortion = new();
    public FoodPortion CurrentFoodPortion
    {
        get => _currentFoodPortion;
        set
        {
            if (_currentFoodPortion == value)
                return;

            _currentFoodPortion = value;
            OnPropertyChanged();
        }
    }


    public NutritionTable NutritionTable => new() { FoodPortions = TodaysFoodPortions };


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

    private string _searchTextFoodPortions = String.Empty;
    public string SearchTextFoodPortions
    {
        get => _searchTextFoodPortions;
        set
        {
            if (_searchTextFoodPortions == value)
                return;

            _searchTextFoodPortions = value;
            OnPropertyChanged();

            _todaysFoodPortions = null;
            LoadTodaysFoodPortions();
            OnPropertyChanged(nameof(TodaysFoodPortions));
        }
    }


    public ICommand AddFoodProduct => new RelayCommand()
    {
        CanExecuteFunc = () => SelectedFoodProduct is not null,
        CommandAction = () =>
        {
            CurrentFoodPortion.FoodProduct = SelectedFoodProduct!;

            var hour = CurrentFoodPortion.Timestamp.Hour;
            var minute = CurrentFoodPortion.Timestamp.Minute;
            var second = CurrentFoodPortion.Timestamp.Second;
            CurrentFoodPortion.Timestamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);

            _todaysFoodPortions = null;
            _dayDistributionService.Add(CurrentFoodPortion);

            LoadTodaysFoodPortions();
            OnPropertyChanged(nameof(TodaysFoodPortions));
        }
    };


    public async Task LoadTodaysFoodPortions()
    {
        TodaysFoodPortions.Value = await _dayDistributionService.GetTodaysFoodPortionAsync();
    }

    public async Task<List<FoodPortion>> GetTodaysFoodPortions(CancellationToken cancellationToken)
    {
        return await _dayDistributionService.GetTodaysFoodPortionAsync(search: SearchTextFoodPortions);
    }

    public async Task LoadFoodProductsAsync()
    {
        FoodProcucts.Value = await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }

    public async Task<List<FoodProcuct>> GetFoodProductsAsync(CancellationToken cancellationToken)
    {
        return await _productService.GetFoodProcuctsAsync(loadAll: true, search: SearchText);
    }
}