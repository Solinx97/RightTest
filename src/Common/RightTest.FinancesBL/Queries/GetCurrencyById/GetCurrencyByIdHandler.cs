using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesBL.DTOs;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Queries.GetCurrencyById;

internal class GetCurrencyByIdHandler(FinancesContext context) : IRequestHandler<GetCurrencyByIdQuery, CurrencyDto?>
{
    private readonly FinancesContext _context = context;

    public async Task<CurrencyDto?> Handle(GetCurrencyByIdQuery request, CancellationToken cancelationToken)
    {
        var currency = await _context.Set<Currency>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new CurrencyDto(x.Id, x.Name, x.Rate))
            .SingleOrDefaultAsync(cancelationToken);

        return currency;
    }
}