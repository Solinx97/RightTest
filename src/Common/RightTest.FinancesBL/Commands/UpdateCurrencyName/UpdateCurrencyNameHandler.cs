using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Commands.UpdateCurrencyName;

internal class UpdateCurrencyNameHandler(FinancesContext context) : IRequestHandler<UpdateCurrencyNameCommand>
{
    private readonly FinancesContext _context = context;

    public async Task Handle(UpdateCurrencyNameCommand request, CancellationToken cancelationToken)
    {
        await _context.Set<Currency>()
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(p => 
                p.SetProperty(d => d.Name, request.Name),
            cancelationToken);

        await _context.SaveChangesAsync(cancelationToken);
    }
}