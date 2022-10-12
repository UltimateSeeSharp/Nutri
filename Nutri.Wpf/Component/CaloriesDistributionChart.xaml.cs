using LiveCharts;
using Nutri.Domain.Model;
using Nutri.Wpf.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component;

public partial class CaloriesDistributionChart : UserControl
{
    private readonly GraphService _graphService = Bootstrapper.Resolve<GraphService>();

    public CaloriesDistributionChart()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(CaloriesDistributionChart));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set
        {
            SetValue(FoodProductProperty, value);
            OnPropertyChanged();
            OnPropertyChanged(nameof(CalorieDistribution));
        }
    }

    public SeriesCollection CalorieDistribution => FoodProduct is null ? new() : _graphService.CalorieDistributionPiChart(FoodProduct);

    public bool IsFoodProductSeleted => FoodProduct is not null;


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyname = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    }

    private void Expander_Expanded(object sender, RoutedEventArgs e)
    {

    }
}
