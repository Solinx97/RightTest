using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RightTest.FinancesAPI.Models;
using RightTest.FinancesBL.Commands.CreateCurrency;
using RightTest.FinancesBL.Commands.DeleteCurrency;
using RightTest.FinancesBL.Commands.UpdateCurrencyName;
using RightTest.FinancesBL.Commands.UpdateCurrencyRate;
using RightTest.FinancesBL.Queries.GetCurrencyById;
using RightTest.FinancesBL.Queries.GetCurrencyByName;
using RightTest.FinancesBL.Queries.GetCurrencyByRate;

namespace RightTest.FinancesAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class FinanceController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("getByName")]
    public async Task<IActionResult> GetCurrencyByName(string name, CancellationToken cancellationToken)
    {
        var currencies = await _mediator.Send(new GetCurrencyByNameQuery(name), cancellationToken);

        return Ok(currencies);
    }

    [HttpGet("getByRate")]
    public async Task<IActionResult> GetCurrencyByRate(decimal rate, CancellationToken cancellationToken)
    {
        var currencies = await _mediator.Send(new GetCurrencyByRateQuery(rate), cancellationToken);

        return Ok(currencies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var currencies = await _mediator.Send(new GetCurrencyByIdQuery(id), cancellationToken);

        return Ok(currencies);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CurrencyModel currency, CancellationToken cancellationToken)
    {
        var createCommand = new CreateCurrencyCommand(currency.Id, currency.Name, currency.Rate);
        var id = await _mediator.Send(createCommand, cancellationToken);

        return Ok(id);
    }

    [HttpPatch("updateName/{id}")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] CurrencyModel currency, CancellationToken cancellationToken)
    {
        if (id != currency.Id)
        {
            return BadRequest("Route ID and body ID do not match.");
        }

        var updateCommand = new UpdateCurrencyNameCommand(currency.Id, currency.Name);
        await _mediator.Send(updateCommand, cancellationToken);

        return NoContent();
    }

    [HttpPatch("updateRate/{id}")]
    public async Task<IActionResult> UpdateRate(Guid id, [FromBody] CurrencyModel currency, CancellationToken cancellationToken)
    {
        if (id != currency.Id)
        {
            return BadRequest("Route ID and body ID do not match.");
        }

        var updateCommand = new UpdateCurrencyRateCommand(currency.Id, currency.Rate);
        await _mediator.Send(updateCommand, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCurrencyCommand(id), cancellationToken);

        return NoContent();
    }
}
