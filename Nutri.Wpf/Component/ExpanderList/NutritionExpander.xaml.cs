using Nutri.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component.ExpanderList;

public partial class NutritionExpander : UserControl
{
    public NutritionExpander()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodProductProperty = DependencyProperty.Register("FoodProduct", typeof(FoodProcuct), typeof(NutritionExpander));
    public FoodProcuct FoodProduct
    {
        get => (FoodProcuct)GetValue(FoodProductProperty);
        set => SetValue(FoodProductProperty, value);
    } 
}
