using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.ExpanderList;

public partial class OtherExpander : UserControl
{
    public OtherExpander()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(OtherExpander));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    }
}
