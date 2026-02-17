using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Persistent;

namespace Migrations.Queries.GetPendingFinancesMigrations;

internal class GetPendingFinancesMigrationsHandler(FinancesContext context) : IRequestHandler<GetPendingFinancesMigrationsQuery, IEnumerable<string>>
{
    private readonly FinancesContext _context = context;

    public async Task<IEnumerable<string>> Handle(GetPendingFinancesMigrationsQuery request, CancellationToken cancelationToken)
    {
        var migrations = await _context.Database.GetPendingMigrationsAsync(cancelationToken);

        return migrations;
    }
}