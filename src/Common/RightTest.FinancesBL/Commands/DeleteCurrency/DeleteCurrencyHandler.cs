using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Commands.DeleteCurrency;

internal class DeleteCurrencyHandler(FinancesContext context) : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly FinancesContext _context = context;

    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancelationToken)
    {
        await _context.Set<Currency>()
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancelationToken);

        await _context.SaveChangesAsync(cancelationToken);
    }
}
