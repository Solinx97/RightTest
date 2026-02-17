using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesBL.DTOs;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Queries.GetUserFavorites;

internal class GetUserFavoritesHandler(FinancesContext context) : IRequestHandler<GetUserFavoritesQuery, IEnumerable<CurrencyDto>>
{
    private readonly FinancesContext _context = context;

    public async Task<IEnumerable<CurrencyDto>> Handle(GetUserFavoritesQuery request, CancellationToken cancelationToken)
    {
        var currencies = await _context.Set<Favorite>()
            .AsNoTracking()
            .Where(x => x.AppUserId == request.AppUserId)
            .Select(x => new CurrencyDto(
                x.Currency.Id,
                x.Currency.Name,
                x.Currency.Rate))
            .ToListAsync(cancelationToken);

        return currencies;
    }
}
