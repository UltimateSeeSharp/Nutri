using Nutri.Domain.Service;

namespace Nutri.Domain.Model;

public class UserSetting
{
    public UserSetting(string name, DateTime birthday, double weight, NutrientProfile nutrientProfile)
    {
        Name = name;
        Birthday = birthday;
        Weight = weight;
        NutrientProfile = nutrientProfile;
    }

    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public double Weight { get; set; } 

    public double WaterNeeds => Weight * 35;

    public NutrientProfile NutrientProfile { get; set; }
}