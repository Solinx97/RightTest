using MediatR;

namespace RightTest.UsersBL.Commands.Login;

public record LoginCommand(
    string Name,
    string Password
    ) : IRequest<string>;