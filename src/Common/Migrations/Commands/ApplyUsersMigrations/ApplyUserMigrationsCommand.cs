using MediatR;

namespace Migrations.Commands.ApplyUsersMigrations;

public record ApplyUserMigrationsCommand(
    ) : IRequest;
