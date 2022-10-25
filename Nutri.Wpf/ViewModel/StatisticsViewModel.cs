using LiveCharts;
using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Model;

namespace Nutri.Wpf.ViewModel;

public class StatisticsViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;

    public StatisticsViewModel(AppSettings appSettings)
	{
        _appSettings = appSettings;
    }

    public SeriesCollection xCaloriesConsumpHistory;

    public void Laoded()
    {
        var settings = _appSettings;
    }
}