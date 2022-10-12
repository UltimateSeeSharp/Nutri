using Nutri.Domain.Model;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.Slider;

public partial class MineralsTraceElementsSlider : UserControl
{
    public MineralsTraceElementsSlider()
    {
        InitializeComponent();
    }


    public static readonly DependencyProperty NrvListProperty = DependencyProperty.Register("NrvList", typeof(List<NrvModel>), typeof(MineralsTraceElementsSlider));
    public List<NrvModel> NrvList
    {
        get => (List<NrvModel>)GetValue(NrvListProperty);
        set => SetValue(NrvListProperty, value);
    }

    public static readonly DependencyProperty FoodPortionsProperty = DependencyProperty.Register("FoodPortions", typeof(List<FoodPortion>), typeof(MineralsTraceElementsSlider));
    public List<FoodPortion> FoodPortions
    {
        get => (List<FoodPortion>)GetValue(FoodPortionsProperty);
        set => SetValue(FoodPortionsProperty, value);
    }
}
