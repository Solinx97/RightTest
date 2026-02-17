using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.Commands.DeleteCurrency;
using RightTest.FinancesBL.IntegrationTests.Base;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesBL.Queries.GetCurrencyById;

namespace RightTest.FinancesBL.IntegrationTests.CommandTests;

[Collection("Postgresql Tests")]
public class DeleteCurrencyTests(PostgresqlFixture fixture) : IAsyncLifetime
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
    public async Task DeleteCurrencyCommand_ShouldRemoveExistCurrencyById()
    {
        using var context = _fixture.CreateContext();

        var id = Guid.NewGuid();

        // Arrange
        await PostgresqlFixture.SeedCurrencyTestDataAsync(context, id);

        await _mediator.Send(new DeleteCurrencyCommand(id));

        // Act
        var currency = await _mediator.Send(new GetCurrencyByIdQuery(id));

        // Assert
        Assert.Null(currency);
    }
}
