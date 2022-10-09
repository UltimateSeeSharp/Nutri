using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component;

public partial class FattyAcidTable : UserControl
{
    public FattyAcidTable()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(FattyAcidTable));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    }

    public static readonly DependencyProperty UserSettingProperty = DependencyProperty.Register("UserSetting", typeof(UserSetting), typeof(FattyAcidTable));
    public UserSetting UserSetting
    {
        get => (UserSetting)GetValue(UserSettingProperty);
        set => SetValue(UserSettingProperty, value);
    }
}
