using Nutri.Wpf.ViewModel;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class DayDistributionView : UserControl
{
    private readonly DayDistributionViewModel _dayDistributionViewModel;

    public DayDistributionView()
    {
        _dayDistributionViewModel = Bootstrapper.Resolve<DayDistributionViewModel>();

        InitializeComponent();
        DataContext = _dayDistributionViewModel;
    }

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        _dayDistributionViewModel.Loaded();
    }
}
