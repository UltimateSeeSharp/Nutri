using Nutri.Wpf.ViewModel;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class DayDistributionView : UserControl
{
    public DayDistributionView()
    {
        InitializeComponent();
        DataContext = Bootstrapper.Resolve<DayDistributionViewModel>();
    }
}
