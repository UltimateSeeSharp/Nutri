using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Component.Slider;
using Nutri.Wpf.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class NutrientDistributionViewModel : BaseViewModel
{
    private readonly DayDistributionService _dayDistributionService;
    private readonly NrvService _nrvService;
    private readonly UserService _userService;

    public NutrientDistributionViewModel(DayDistributionService dayDistributionService, NrvService nrvService, UserService userService)
	{
        _dayDistributionService = dayDistributionService;
        _nrvService = nrvService;
        _userService = userService;
    }

    private LazyProperty<List<FoodPortion>>? _todaysFoodPortions = null;
    public LazyProperty<List<FoodPortion>> TodaysFoodPortions => _todaysFoodPortions ??= new(GetTodaysFoodPortions);

    public NutrientSlider NutrientSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs(), UserSetting = _userService.CurrentUserSetting };
    public MineralsTraceElementsSlider MineralsTraceElementsSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs() };
    public VitaminsSlider VitaminsSlider => new() { FoodPortions = TodaysFoodPortions, NrvList = _nrvService.GetNrvs() };

    public void Loaded()
    {
        OnPropertyChanged(nameof(MineralsTraceElementsSlider));
        OnPropertyChanged(nameof(VitaminsSlider));
        OnPropertyChanged(nameof(NutrientSlider));
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