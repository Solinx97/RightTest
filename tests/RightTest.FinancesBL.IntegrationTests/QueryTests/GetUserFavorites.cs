using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetUserFavorites;

namespace RightTest.FinancesBL.IntegrationTests.QueryTests;

[Collection("Postgresql Tests")]
public class GetUserFavorites(PostgresqlFixture fixture) : IAsyncLifetime
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

        var currencyId = Guid.Parse("b081ad51-1aec-4094-89f6-412115173933");
        var appUserId = "1d374a14-ce2f-45a2-98c3-0c18e61f0ec9";

        // Arrange
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, currencyId);
        await PostgresqlFixture.SeedFavoriteTestDataAsync(context, Guid.NewGuid(), currencyId, appUserId);

        // Act
        var currencies = await _mediator.Send(new GetUserFavoritesQuery(appUserId));

        // Assert
        Assert.NotNull(currencies);
        Assert.NotEmpty(currencies);
        Assert.Single(currencies);
        Assert.Equal(currencies.First().Id, currencyId);
    }
}
