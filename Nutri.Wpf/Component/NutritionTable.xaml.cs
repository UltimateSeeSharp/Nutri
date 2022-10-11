using Nutri.Domain.Model;
using Nutri.Wpf._Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nutri.Wpf.Component;

public partial class NutritionTable : UserControl
{
    public NutritionTable()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FoodPortionsProperty = DependencyProperty.Register("FoodPortions", typeof(List<FoodPortion>), typeof(NutritionTable));
    
    public List<FoodPortion> FoodPortions
    {
        get
        {
            var res = (List<FoodPortion>)GetValue(FoodPortionsProperty);
            return res;
        }
        set
        {
            SetValue(FoodPortionsProperty, value);
        }
    }

    public FoodConsumption FoodConsumption => new(FoodPortions);
}