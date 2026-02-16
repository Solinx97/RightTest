using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersBL.Commands.Login;
using RightTest.UsersBL.Commands.Registration;
using RightTest.UsersBL.IntegrationTests.Base;
using RightTest.UsersBL.IntegrationTests.Data;

namespace RightTest.UsersBL.IntegrationTests.CommandTests;

[Collection("Postgresql Tests")]
public class LoginTests(PostgresqlFixture fixture) : IAsyncLifetime
{
    private readonly PostgresqlFixture _fixture = fixture;

    private TestAppBase _app = new(fixture);
    private IMediator _mediator = default!;

    public async Task InitializeAsync()
    {
        _app = new TestAppBase(_fixture);
        await _app.InitializeAsync();

        _mediator = _app.Services.GetRequiredService<IMediator>();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task LoginCommand_ShouldLoginExistUser()
    {
        using var context = _fixture.CreateContext();

        var name = "Oleg";
        var password = "Jiril2*";

        // Arrange
        await _mediator.Send(new RegistrationCommand(name, password));

        // Act
        var token = await _mediator.Send(new LoginCommand(name, password));

        // Act and Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }
}
