using MediatR;
using Microsoft.AspNetCore.Mvc;
using Migrations.Commands.ApplyFinancesMigrations;
using Migrations.Queries.GetPendingFinancesMigrations;

namespace RightTest.MigrationsAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FinancesMigrationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("pending")]
    public async Task<IActionResult> PendingMigrations(CancellationToken cancellationToken)
    {
        var pendings = await _mediator.Send(new GetPendingFinancesMigrationsQuery(), cancellationToken);

        return Ok(pendings);
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyMigrations(CancellationToken cancellationToken)
    {
        var applyCommand = new ApplyFinancesMigrationsCommand();
        await _mediator.Send(applyCommand, cancellationToken);

        return Ok("All pending migrations applied successfully!");
    }
}
