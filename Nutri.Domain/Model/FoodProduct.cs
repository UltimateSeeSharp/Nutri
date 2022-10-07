namespace Nutri.Domain.Model;

public class FoodProcuct
{
    public FoodProcuct(string name, DateTime publicationDate)
    {
        Name = name;
        PublicationDate = publicationDate;
        Ingredients = new();
    }

    public string Name { get; set; }

    public DateTime PublicationDate { get; set; }

    public List<Ingredient> Ingredients { get; set; }
}