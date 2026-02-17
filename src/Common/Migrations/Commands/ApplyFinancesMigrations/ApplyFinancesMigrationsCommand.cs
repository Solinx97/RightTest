using MediatR;

namespace Migrations.Commands.ApplyFinancesMigrations;

public record ApplyFinancesMigrationsCommand(
    ) : IRequest;
