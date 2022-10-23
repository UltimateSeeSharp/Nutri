using Nutri.Wpf.ViewModel;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class StatisticsView : UserControl
{
    private readonly StatisticsViewModel _statisticsViewModel;

    public StatisticsView()
    {
        _statisticsViewModel = Bootstrapper.Resolve<StatisticsViewModel>();

        InitializeComponent();
        DataContext = _statisticsViewModel;
    }

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) => _statisticsViewModel.Laoded();
}
