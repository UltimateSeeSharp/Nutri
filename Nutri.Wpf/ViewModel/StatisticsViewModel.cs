using LiveCharts;
using Nutri.Domain.Enum;
using Nutri.Domain.Service;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Model;
using Nutri.Wpf.Service;
using System.Threading.Tasks;

namespace Nutri.Wpf.ViewModel;

public class StatisticsViewModel : BaseViewModel
{
    private readonly DayDistributionService _dayDistributionService;
    private readonly GraphService _graphService;
    private readonly AppSettings _appSettings;

    public StatisticsViewModel(DayDistributionService dayDistributionService, GraphService graphService, AppSettings appSettings)
	{
        _dayDistributionService = dayDistributionService;
        _graphService = graphService;
        _appSettings = appSettings;
    }

    public SeriesCollection CaloriesConsumpHistory { get; set; }

    public async Task Laoded()
    {
        await Task.Run(SetCaloriesConsumpHistory);
    }

    private async Task SetCaloriesConsumpHistory()
    {
        var portions = await _dayDistributionService.GetFoodPortions(Timeframe.Last7Days);
        CaloriesConsumpHistory = _graphService.GetNutrientHistory(portions, "Energy", Timeframe.Last7Days);
        OnPropertyChanged(nameof(CaloriesConsumpHistory));
    }
}