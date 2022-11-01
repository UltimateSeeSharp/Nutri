using LiveCharts;
using Microsoft.Extensions.Primitives;
using Nutri.Domain.Enum;
using Nutri.Domain.Model;
using Nutri.Domain.Service;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Model;
using Nutri.Wpf.Service;
using System.Collections.Generic;
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

    private LazyProperty<SeriesCollection>? _caloriesConsumpHistory = null;
    public LazyProperty<SeriesCollection> CaloriesConsumpHistory => _caloriesConsumpHistory ??= new(GetCaloriesConsumpHistory);

    public void Laoded()
    {
        OnPropertyChanged(nameof(CaloriesConsumpHistory));
    }

    private async Task<SeriesCollection> GetCaloriesConsumpHistory(CancellationToken cancellationToken)
    {
        var portions = await _dayDistributionService.GetFoodPortions(Timeframe.Today);
        return await _graphService.GetNutrientHistory("Energy", Timeframe.Last7Days);
    }
}