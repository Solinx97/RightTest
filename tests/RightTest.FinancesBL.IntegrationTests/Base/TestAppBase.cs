using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.Extensions;
using RightTest.FinancesBL.IntegrationTests.Data;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.IntegrationTests.Base;

internal class TestAppBase(PostgresqlFixture fixture) : IAsyncLifetime
{
    private readonly PostgresqlFixture _fixture = fixture;

    public IServiceProvider Services { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        var services = new ServiceCollection();

        services.AddMediatorSource(_fixture.Connection);

        Services = services.BuildServiceProvider();

        // apply migrations
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FinancesContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
