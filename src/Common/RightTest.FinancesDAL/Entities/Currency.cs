namespace RightTest.FinancesDAL.Entities;

public record Currency(
    Guid Id,
    string Name,
    decimal Rate
    );
