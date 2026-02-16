using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersBL.Commands.Registration;
using RightTest.UsersBL.IntegrationTests.Base;
using RightTest.UsersBL.IntegrationTests.Data;
using RightTest.UsersDAL.Entities;

namespace RightTest.UsersBL.IntegrationTests.CommandTests;

[CollectionDefinition("Postgresql Tests")]
public class PostgresqlTestCollection : ICollectionFixture<PostgresqlFixture> { }

[Collection("Postgresql Tests")]
public class RegistrationTests(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task RegistrationCommand_ShouldRegistryNewUser()
    {
        using var context = _fixture.CreateContext();

        var name = "Oleg";
        var password = "Jiril2*";

        // Act
        await _mediator.Send(new RegistrationCommand(name, password));
        var user = await context.Set<AppUser>().FirstOrDefaultAsync(x => x.UserName == name);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(user.UserName, name);
    }
}
