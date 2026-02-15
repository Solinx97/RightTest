using MediatR;
using RightTest.UsersDAL.Interfaces;

namespace RightTest.UsersBL.Commands.Registration;

internal class RegistrationHandler(IUserRepository repository) : IRequestHandler<RegistrationCommand>
{
    private readonly IUserRepository _repository = repository;

    public async Task Handle(RegistrationCommand request, CancellationToken cancelationToken)
    {
        var wasRegistered = await _repository.RegistrationAsync(request.Name, request.Password);
        if (!wasRegistered)
        {
            throw new Exception("Registration failed");
        }
    }
}