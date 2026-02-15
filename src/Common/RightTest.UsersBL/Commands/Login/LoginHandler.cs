using MediatR;
using RightTest.UsersDAL.Interfaces;

namespace RightTest.UsersBL.Commands.Login;

internal class LoginHandler(IUserRepository repository) : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _repository = repository;

    public async Task<string> Handle(LoginCommand request, CancellationToken cancelationToken)
    {
        var token = await _repository.LoginAsync(request.Name, request.Password);

        return token;
    }
}