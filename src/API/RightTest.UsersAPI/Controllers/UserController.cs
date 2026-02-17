using MediatR;
using Microsoft.AspNetCore.Mvc;
using RightTest.UsersAPI.Models;
using RightTest.UsersBL.Commands.Login;
using RightTest.UsersBL.Commands.Registration;

namespace RightTest.UsersAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel registration, CancellationToken cancellationToken)
    {
        var registrationCommand = new RegistrationCommand(registration.Name, registration.Password);
        await _mediator.Send(registrationCommand, cancellationToken);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel login, CancellationToken cancellationToken)
    {
        var loginCommand = new LoginCommand(login.Name, login.Password);
        var token = await _mediator.Send(loginCommand, cancellationToken);

        return Ok(token);
    }
}
