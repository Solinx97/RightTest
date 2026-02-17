namespace RightTest.FinancesDAL.Entities;

public class Favorite
{
    public Favorite() { }

    public Guid Id { get; set; }

    public Guid CurrencyId { get; set; }

    public string AppUserId { get; set; }

    public Currency Currency { get; set; }
}
