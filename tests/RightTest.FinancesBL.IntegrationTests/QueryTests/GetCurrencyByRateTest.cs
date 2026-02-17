using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetCurrencyByRate;

namespace RightTest.FinancesBL.IntegrationTests.QueryTests;

[Collection("Postgresql Tests")]
public class GetCurrencyByRateTest(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task GetCurrencyByRateQuery_Currencies_ShouldReturnCurrencyColelctionByRate()
    {
        using var context = _fixture.CreateContext();

        var length = 10;
        var rate = 2.11m;

        // Arrange
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, length, false, true);
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, length);

        // Act
        var currencies = await _mediator.Send(new GetCurrencyByRateQuery(rate));

        // Assert
        Assert.NotEmpty(currencies);
        Assert.Equal(currencies.Count(), length);
    }
}
