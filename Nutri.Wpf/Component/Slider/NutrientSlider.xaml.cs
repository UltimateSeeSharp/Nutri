using LiveCharts;
using Nutri.Domain.Model;
using Nutri.Wpf.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.Slider;

public partial class NutrientSlider : UserControl
{
    public NutrientSlider()
    {
        InitializeComponent();
        _graphService = Bootstrapper.Resolve<GraphService>();
    }

    public static readonly DependencyProperty NrvListProperty = DependencyProperty.Register("NrvList", typeof(List<NrvModel>), typeof(NutrientSlider));
    public List<NrvModel> NrvList
    {
        get => (List<NrvModel>)GetValue(NrvListProperty);
        set => SetValue(NrvListProperty, value);
    }

    public static readonly DependencyProperty FoodPortionsProperty = DependencyProperty.Register("FoodPortions", typeof(List<FoodPortion>), typeof(NutrientSlider));
    public List<FoodPortion> FoodPortions
    {
        get => (List<FoodPortion>)GetValue(FoodPortionsProperty);
        set => SetValue(FoodPortionsProperty, value);
    }

    public static readonly DependencyProperty UserSettingProperty = DependencyProperty.Register("UserSetting", typeof(UserSetting), typeof(NutrientSlider));
    private readonly GraphService _graphService;

    public UserSetting UserSetting
    {
        get => (UserSetting)GetValue(UserSettingProperty);
        set => SetValue(UserSettingProperty, value);
    }

    public SeriesCollection TodaysCalorieDistributionSeries
        => FoodPortions is null
        ? new()
        : _graphService.CalorieDistributionPiChart(FoodPortions.ToArray());

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        OnPropertyChanged(nameof(TodaysCalorieDistributionSeries));
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyname = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    }
}