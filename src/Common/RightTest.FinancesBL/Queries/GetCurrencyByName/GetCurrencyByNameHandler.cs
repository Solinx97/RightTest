using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesBL.DTOs;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Queries.GetCurrencyByName;

internal class GetCurrencyByNameHandler(FinancesContext context) : IRequestHandler<GetCurrencyByNameQuery, IEnumerable<CurrencyDto>>
{
    private readonly FinancesContext _context = context;

    public async Task<IEnumerable<CurrencyDto>> Handle(GetCurrencyByNameQuery request, CancellationToken cancelationToken)
    {
        var currencies = await _context.Set<Currency>()
            .AsNoTracking()
            .Where(x => x.Name == request.Name)
            .Select(x => new CurrencyDto(x.Id, x.Name, x.Rate))
            .ToListAsync(cancelationToken);

        return currencies;
    }
}
