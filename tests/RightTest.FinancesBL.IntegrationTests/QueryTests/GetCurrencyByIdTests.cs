using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetCurrencyById;

namespace RightTest.FinancesBL.IntegrationTests.QueryTests;

[Collection("Postgresql Tests")]
public class GetCurrencyByIdTests(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task GetCurrencyByIdQuery_Currency_ShouldReturnCurrencyById()
    {
        using var context = _fixture.CreateContext();

        var id = Guid.NewGuid();
        var name = "BYN";
        var rate = 2.11m;

        // Arrange
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, id);

        // Act
        var currency = await _mediator.Send(new GetCurrencyByIdQuery(id));

        // Assert
        Assert.NotNull(currency);
        Assert.Equal(currency.Id, id);
        Assert.Equal(currency.Name, name);
        Assert.Equal(currency.Rate, rate);
    }
}
