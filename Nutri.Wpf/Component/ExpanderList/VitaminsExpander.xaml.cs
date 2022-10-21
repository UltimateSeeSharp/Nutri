using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.ExpanderList;

public partial class VitaminsExpander : UserControl
{
    public VitaminsExpander()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(VitaminsExpander));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    }
}
