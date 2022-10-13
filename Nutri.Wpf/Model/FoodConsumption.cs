using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nutri.Wpf._Model;

public class FoodConsumption
{
    public FoodConsumption(List<FoodPortion> foodPortions)
    {
        FoodPortions = foodPortions;
    }

    public List<FoodPortion> FoodPortions { get; set; }
}