using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetCurrencyByName;

namespace RightTest.FinancesBL.IntegrationTests.QueryTests;

[Collection("Postgresql Tests")]
public class GetCurrencyByNameTests(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task GetCurrencyByNameQuery_Currencies_ShouldReturnCurrencyColelctionByName()
    {
        using var context = _fixture.CreateContext();

        var length = 10;
        var name = "BYN";

        // Arrange
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, length, true);
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, length);

        // Act
        var currencies = await _mediator.Send(new GetCurrencyByNameQuery(name));

        // Assert
        Assert.NotEmpty(currencies);
        Assert.Equal(currencies.Count(), length);
    }
}
