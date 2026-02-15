using MediatR;

namespace Migrations.Queries.GetPendingFinancesMigrations;

public record GetPendingFinancesMigrationsQuery(
    ) : IRequest<IEnumerable<string>>;
