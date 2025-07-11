﻿using Nutri.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nutri.Wpf.Service;

public class FoodPortionService
{
    private DateTime _currentDate = DateTime.Now;

    public List<FoodPortion> GetFoodPortionsDated(List<FoodPortion> foodPortions, bool morning = false, bool dinner = false, bool lunch = false, bool snacks = false, int? hourFrom = null, int? hourTo = null)
    {
        var portionsToday = foodPortions.Where(x => x.Timestamp.Year == _currentDate.Year
                     && x.Timestamp.Month == _currentDate.Month
                     && x.Timestamp.Day == _currentDate.Day).ToList();

        if (morning)
            return portionsToday.Where(x => x.Timestamp.Hour >= 5 && x.Timestamp.Hour < 11).ToList();

        else if (lunch)
            return portionsToday.Where(x => x.Timestamp.Hour >= 12 && x.Timestamp.Hour < 14).ToList();

        else if (dinner)
            return portionsToday.Where(x => x.Timestamp.Hour >= 18 && x.Timestamp.Hour <= 21).ToList();

        else if (hourFrom is not null && hourTo is not null)
            return portionsToday.Where(x => x.Timestamp.Hour > hourFrom && x.Timestamp.Hour <= hourTo).ToList();

        else if (snacks)
        {
            List<FoodPortion> mainMeals = new();
            mainMeals.AddRange(portionsToday.Where(x => x.Timestamp.Hour > 5 && x.Timestamp.Hour < 11));
            mainMeals.AddRange(portionsToday.Where(x => x.Timestamp.Hour > 12 && x.Timestamp.Hour < 14).ToList());
            mainMeals.AddRange(portionsToday.Where(x => x.Timestamp.Hour > 18 && x.Timestamp.Hour <= 21).ToList());

            mainMeals.ForEach(x => portionsToday.Remove(x));
        }

        return portionsToday;
    }
}