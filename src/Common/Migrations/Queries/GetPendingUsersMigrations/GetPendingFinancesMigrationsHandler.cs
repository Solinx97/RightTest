using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.UsersDAL.Persistent;

namespace Migrations.Queries.GetPendingUsersMigrations;

internal class GetPendingUsersMigrationsHandler(AppUsersContext context) : IRequestHandler<GetPendingUsersMigrationsQuery, IEnumerable<string>>
{
    private readonly AppUsersContext _context = context;

    public async Task<IEnumerable<string>> Handle(GetPendingUsersMigrationsQuery request, CancellationToken cancelationToken)
    {
        var migrations = await _context.Database.GetPendingMigrationsAsync(cancelationToken);

        return migrations;
    }
}