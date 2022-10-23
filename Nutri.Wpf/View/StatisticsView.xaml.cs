using Nutri.Wpf.ViewModel;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class StatisticsView : UserControl
{
    public StatisticsView()
    {
        InitializeComponent();
        DataContext = Bootstrapper.Resolve<StatisticsViewModel>();
    }
}
