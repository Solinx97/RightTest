using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.Commands.CreateCurrency;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetCurrencyById;

namespace RightTest.FinancesBL.IntegrationTests.CommandTests;

[CollectionDefinition("Postgresql Tests")]
public class PostgresqlTestCollection : ICollectionFixture<PostgresqlFixture> { }

[Collection("Postgresql Tests")]
public class CreateCurrencyTests(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task CreateCurrencyCommand_CurrencyId_ShouldReturnCreatedCurrencyId()
    {
        using var context = _fixture.CreateContext();

        var name = "GRY";
        var rate = 4.234m;

        // Arrange
        var id = await _mediator.Send(new CreateCurrencyCommand(Guid.NewGuid(), name, rate));

        // Act
        var currency = await _mediator.Send(new GetCurrencyByIdQuery(id));

        // Assert
        Assert.NotNull(currency);
        Assert.Equal(currency.Id, id);
        Assert.Equal(currency.Name, name);
        Assert.Equal(currency.Rate, rate);
    }
}
