using Nutri.Domain.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.Slider;

public partial class NutrientSlider : UserControl
{
    public NutrientSlider()
    {
        InitializeComponent();
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
    public UserSetting UserSetting
    {
        get => (UserSetting)GetValue(UserSettingProperty);
        set => SetValue(UserSettingProperty, value);
    }
}