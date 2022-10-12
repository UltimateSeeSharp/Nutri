using Nutri.Wpf.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class NutrientDistributionView : UserControl
{
    private readonly NutrientDistributionViewModel _nutrientDistributionViewModel;

    public NutrientDistributionView()
    {
        _nutrientDistributionViewModel = Bootstrapper.Resolve<NutrientDistributionViewModel>();

        InitializeComponent();
        DataContext = _nutrientDistributionViewModel;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        _nutrientDistributionViewModel.Loaded();
    }
}
