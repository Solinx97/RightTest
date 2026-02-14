namespace RightTest.FinancesBL.DTOs;

public record CurrencyDto(
    Guid Id,
    string Name,
    decimal Rate
    );