using MediatR;

namespace RightTest.UsersBL.Commands.Registration;

public record RegistrationCommand(
    string Name,
    string Password
    ) : IRequest;
