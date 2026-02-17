using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Commands.UpdateCurrencyRate;

internal class UpdateCurrencyRateHandler(FinancesContext context) : IRequestHandler<UpdateCurrencyRateCommand>
{
    private readonly FinancesContext _context = context;

    public async Task Handle(UpdateCurrencyRateCommand request, CancellationToken cancelationToken)
    {
        await _context.Set<Currency>()
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(p => 
                p.SetProperty(d => d.Rate, request.Rate),
            cancelationToken);

        await _context.SaveChangesAsync(cancelationToken);
    }
}