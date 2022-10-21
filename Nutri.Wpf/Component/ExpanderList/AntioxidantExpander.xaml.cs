using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.ExpanderList;

public partial class AntioxidantExpander : UserControl
{
    public AntioxidantExpander()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(AntioxidantExpander));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    }
}
