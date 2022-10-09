namespace Nutri.Domain.Model;

public class FoodPortion
{
    public FoodProcuct FoodProduct { get; set; }

    public double Amount { get; set; }

    public DateTime Timestamp { get; set; }
}
