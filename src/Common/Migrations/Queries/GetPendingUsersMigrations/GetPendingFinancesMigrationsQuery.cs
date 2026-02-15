using MediatR;

namespace Migrations.Queries.GetPendingUsersMigrations;

public record GetPendingUsersMigrationsQuery(
    ) : IRequest<IEnumerable<string>>;
