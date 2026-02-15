using MediatR;
using Microsoft.EntityFrameworkCore;
using RightTest.UsersDAL.Persistent;

namespace Migrations.Commands.ApplyUsersMigrations;

internal class ApplyUsersMigrationsHanlde(AppUsersContext context) : IRequestHandler<ApplyUserMigrationsCommand>
{
    private readonly AppUsersContext _context = context;

    public async Task Handle(ApplyUserMigrationsCommand request, CancellationToken cancelationToken)
    {
        await _context.Database.MigrateAsync(cancelationToken);
    }
}
