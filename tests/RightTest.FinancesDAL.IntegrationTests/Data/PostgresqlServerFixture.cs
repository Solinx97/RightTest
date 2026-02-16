using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;
using Testcontainers.PostgreSql;

namespace RightTest.FinancesDAL.IntegrationTests.Data;

public class PostgresqlServerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public PostgresqlServerFixture()
    {
        _container = new PostgreSqlBuilder()
          .WithImage("postgres:16-alpine")
          .WithDatabase("Currency")
          .WithUsername("postgres")
          .WithPassword("124124")
          .WithCleanUp(true)
          .Build();
    }

    public DbContextOptions<FinancesContext> Options { get; private set; } = null!;

    public FinancesContext CreateContext()
    {
        return new FinancesContext(Options);
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public static async Task Drop(FinancesContext context)
    {
        // Delete all rows
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Currency");

        // Reset IDENTITY column to 0 (so next insert starts at 1)
        await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('Currency', RESEED, 0)");
    }

    public static async Task SeedCurrencyTestDataAsync(FinancesContext context)
    {
        await context.Set<Currency>().AddRangeAsync(
            new Currency(Guid.NewGuid(), "BYN", 2.11m),
            new Currency(Guid.NewGuid(), "USD", 3.11m)
        );

        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
