namespace Nutri.Domain.Model;

public class Ingredient
{
    public Ingredient(string name, string unit, double amount)
    {
        Name = name;
        Unit = unit;
        Amount = amount;
    }

    public string Name { get; set; }

    public string Unit { get; set; }

    public double Amount { get; set; }
}