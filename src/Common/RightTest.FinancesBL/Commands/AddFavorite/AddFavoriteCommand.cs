using MediatR;

namespace RightTest.FinancesBL.Commands.AddFavorite;

public record AddFavoriteCommand(
    Guid Id,
    Guid CurrencyId,
    string AppUserId
    ) : IRequest<Guid>;
