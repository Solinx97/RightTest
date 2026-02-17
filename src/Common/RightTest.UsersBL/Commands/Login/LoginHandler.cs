using MediatR;
using RightTest.UsersBL.Interfaces;
using RightTest.UsersDAL.Interfaces;

namespace RightTest.UsersBL.Commands.Login;

internal class LoginHandler(IUserRepository repository, ITokenService tokenService) : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _repository = repository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<string> Handle(LoginCommand request, CancellationToken cancelationToken)
    {
        var user = await _repository.LoginAsync(request.Name, request.Password);
        var token = _tokenService.GenerateJwtToken(user.Id, user.UserName);

        return token;
    }
}