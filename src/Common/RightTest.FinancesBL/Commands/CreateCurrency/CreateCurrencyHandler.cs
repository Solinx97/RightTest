using MediatR;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Commands.CreateCurrency;

internal class CreateCurrencyHandler(FinancesContext context) : IRequestHandler<CreateCurrencyCommand, Guid>
{
    private readonly FinancesContext _context = context;

    public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancelationToken)
    {
        var currency = new Currency
        {
            Id = request.Id,
            Name = request.Name,
            Rate = request.Rate,
        };

        await _context.Set<Currency>()
            .AddAsync(currency, cancelationToken);

        await _context.SaveChangesAsync(cancelationToken);

        return currency.Id;
    }
}
