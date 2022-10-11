namespace Nutri.Domain.Model;

public class FoodProcuct
{
    public FoodProcuct(string name, DateTime publicationDate)
    {
        Name = name;
        PublicationDate = publicationDate;
        Nutrients = new();
    }

    public string Name { get; set; }

    public DateTime PublicationDate { get; set; }

    public List<Nutrient> Nutrients { get; set; }
}