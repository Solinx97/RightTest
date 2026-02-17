using MediatR;
using Microsoft.AspNetCore.Mvc;
using Migrations.Commands.ApplyUsersMigrations;
using Migrations.Queries.GetPendingUsersMigrations;

namespace RightTest.MigrationsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersMigrationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("pending")]
    public async Task<IActionResult> PendingMigrations(CancellationToken cancellationToken)
    {
        var pendings = await _mediator.Send(new GetPendingUsersMigrationsQuery(), cancellationToken);

        return Ok(pendings);
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyMigrations(CancellationToken cancellationToken)
    {
        var applyCommand = new ApplyUserMigrationsCommand();
        await _mediator.Send(applyCommand, cancellationToken);

        return Ok("All pending migrations applied successfully!");
    }
}
