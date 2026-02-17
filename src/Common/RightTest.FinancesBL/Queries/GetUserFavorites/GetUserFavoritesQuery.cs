using MediatR;
using RightTest.FinancesBL.DTOs;

namespace RightTest.FinancesBL.Queries.GetUserFavorites;

public record GetUserFavoritesQuery(
    string AppUserId
    ) : IRequest<IEnumerable<CurrencyDto>>;
