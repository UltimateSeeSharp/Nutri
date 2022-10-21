using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.ExpanderList;

public partial class FattyAcidExpander : UserControl
{
    public FattyAcidExpander()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(FattyAcidExpander));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    }

    public static readonly DependencyProperty UserSettingProperty = DependencyProperty.Register("UserSetting", typeof(UserSetting), typeof(FattyAcidExpander));
    public UserSetting UserSetting
    {
        get => (UserSetting)GetValue(UserSettingProperty);
        set => SetValue(UserSettingProperty, value);
    }
}
