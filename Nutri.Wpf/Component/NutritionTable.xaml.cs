using LiveCharts;
using Nutri.Domain.Model;
using Nutri.Wpf._Model;
using Nutri.Wpf.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyname = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
    }
}