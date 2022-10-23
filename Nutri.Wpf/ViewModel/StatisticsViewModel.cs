using Nutri.Wpf.Infrastructure;
using Nutri.Wpf.Model;
using System.Security.Policy;

namespace Nutri.Wpf.ViewModel;

public class StatisticsViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;

    public StatisticsViewModel(AppSettings appSettings)
	{
        _appSettings = appSettings;
    }

    public void Laoded()
    {
        var settings = _appSettings;
    }
}