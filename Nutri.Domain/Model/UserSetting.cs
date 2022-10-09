using Nutri.Domain.Service;

namespace Nutri.Domain.Model;

public class UserSetting
{
    public UserSetting(string name, DateTime birthday, double weight, double caloriesUsageRest)
    {
        Name = name;
        Birthday = birthday;
        Weight = weight;
        CaloriesUsageRest = caloriesUsageRest;
    }

    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public double Weight { get; set; } 

    public double CaloriesUsageRest { get; set; }

    public double WaterNeeds => Weight * 35;
}