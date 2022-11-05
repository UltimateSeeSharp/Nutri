using LiveCharts;
using LiveCharts.Wpf;
using Nutri.Domain.Enum;
using Nutri.Domain.Service;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Model;
using Nutri.Wpf.Service;
using System.Threading;
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

    private LazyProperty<CartesianChart>? _consumptionHistory = null;
    public LazyProperty<CartesianChart> ConsumptionHistory => _consumptionHistory ??= new(GetConsumptionHistory);

    private LazyProperty<CartesianChart>? _consumptionHistoryTest= null;
    public LazyProperty<CartesianChart> ConsumptionHistoryTest => _consumptionHistoryTest ??= new(GetConsumptionHistory);

    public Timeframe CurrentTimeframe => Timeframe.Last7Days;

    public async Task Laoded()
    {
        //await LoadConsumptionHistory(Timeframe.ThisMonth);
    }

    async Task<CartesianChart> GetConsumptionHistory(CancellationToken cancellationToken)
    {
        var foodPortions = await _dayDistributionService.GetFoodPortions(CurrentTimeframe);
        var seriesCollection = _graphService.GetNutrientHistory(foodPortions, "Energy", CurrentTimeframe);
        return new() 
        { 
            Series = seriesCollection,
            AxisX = _graphService.GetXAxes(CurrentTimeframe),
        };
    }
}