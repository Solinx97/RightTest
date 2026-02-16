namespace RightTest.FinancesDAL.Entities;

public class Currency
{
    public Currency() { }

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Rate { get; set; }

    public List<Favorite> Favorites { get; set; } = [];
}
