using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Persistent;

namespace Migrations.Commands.ApplyFinancesMigrations;

internal class ApplyFinancesMigrationsHanlde(FinancesContext context) : IRequestHandler<ApplyFinancesMigrationsCommand>
{
    private readonly FinancesContext _context = context;

    public async Task Handle(ApplyFinancesMigrationsCommand request, CancellationToken cancelationToken)
    {
        await _context.Database.MigrateAsync(cancelationToken);
    }
}
