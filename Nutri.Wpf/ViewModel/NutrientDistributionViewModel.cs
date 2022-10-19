using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component.Slider;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class NutrientDistributionViewModel : BaseViewModel
{
    private readonly DayDistributionService _dayDistributionService;
    private readonly NrvService _nrvService;
    private readonly UserService _userService;
    private readonly FoodPortionService _foodPortionService;

    public NutrientDistributionViewModel(DayDistributionService dayDistributionService, NrvService nrvService, UserService userService, FoodPortionService foodPortionService)
	{
        _dayDistributionService = dayDistributionService;
        _nrvService = nrvService;
        _userService = userService;
        _foodPortionService = foodPortionService;
    }

    private LazyProperty<List<FoodPortion>>? _todaysFoodPortions = null;
    public LazyProperty<List<FoodPortion>> TodaysFoodPortions => _todaysFoodPortions ??= new(GetTodaysFoodPortions);

    public List<FoodPortion> MorningFoodPortions => TodaysFoodPortions.Value is null ? new() : _foodPortionService.GetFoodPortionsDated(TodaysFoodPortions, morning: true);
    public List<FoodPortion> LunchFoodPortions => TodaysFoodPortions.Value is null ? new() : _foodPortionService.GetFoodPortionsDated(TodaysFoodPortions, lunch: true);
    public List<FoodPortion> DinnerFoodPortions => TodaysFoodPortions.Value is null ? new() : _foodPortionService.GetFoodPortionsDated(TodaysFoodPortions, dinner: true);

    public NutrientSlider NutrientSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs(), UserSetting = _userService.CurrentUserSetting };
    public MineralsTraceElementsSlider MineralsTraceElementsSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs() };
    public VitaminsSlider VitaminsSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs() };

    public void Loaded()
    {
        OnPropertyChanged(nameof(MineralsTraceElementsSlider));
        OnPropertyChanged(nameof(VitaminsSlider));
        OnPropertyChanged(nameof(NutrientSlider));

        OnPropertyChanged(nameof(MorningFoodPortions));
        OnPropertyChanged(nameof(LunchFoodPortions));
        OnPropertyChanged(nameof(DinnerFoodPortions));
    }

    public async Task LoadTodaysFoodPortions()
    {
        TodaysFoodPortions.Value = await _dayDistributionService.GetTodaysFoodPortionAsync();
    }

    public async Task<List<FoodPortion>> GetTodaysFoodPortions(CancellationToken cancellationToken)
    {
        return await _dayDistributionService.GetTodaysFoodPortionAsync();
    }
}