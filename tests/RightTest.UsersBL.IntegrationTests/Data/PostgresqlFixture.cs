using Microsoft.EntityFrameworkCore;
using RightTest.UsersDAL.Entities;
using RightTest.UsersDAL.Persistent;
using Testcontainers.PostgreSql;

namespace RightTest.UsersBL.IntegrationTests.Data;

public class PostgresqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public PostgresqlFixture()
    {
        _container = new PostgreSqlBuilder("postgres:16-alpine")
          .WithDatabase("AppUser")
          .WithUsername("postgres")
          .WithPassword("124124")
          .WithCleanUp(true)
          .Build();
    }

    public DbContextOptions<AppUsersContext> Options { get; private set; } = null!;

    public string Connection { get; private set; } = string.Empty;

    public AppUsersContext CreateContext()
    {
        return new AppUsersContext(Options);
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        Connection = _container.GetConnectionString();
        Options = new DbContextOptionsBuilder<AppUsersContext>()
            .UseNpgsql(Connection)
            .Options;
    }

    public static async Task SeedAppUserTestDataAsync(AppUsersContext context, string id)
    {
        await context.Set<AppUser>().AddAsync(
            new AppUser
            {
                Id = id,
                UserName = "Oleg"
            }
        );

        await context.SaveChangesAsync();
    }

    public static async Task SeedAppUserTestDataAsync(AppUsersContext context, int length, bool isSameName = false)
    {
        for (int i = 0; i < length; i++)
        {
            await context.Set<AppUser>().AddAsync(
                new AppUser
                {
                    Id = i.ToString(),
                    UserName = isSameName ? "Oleg" : "Oleg" + i
                }
            );
        }

        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
