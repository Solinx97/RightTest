namespace RightTest.FinancesBL.DTOs;

public record FavoriteDto(
    Guid Id,
    Guid CurrencyId,
    string AppUserId
    );
