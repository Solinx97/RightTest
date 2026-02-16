using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RightTest.UsersBL.Extensions;
using RightTest.UsersBL.IntegrationTests.Data;
using RightTest.UsersDAL.Persistent;

namespace RightTest.UsersBL.IntegrationTests.Base;

internal class TestAppBase(PostgresqlFixture fixture) : IAsyncLifetime
{
    private readonly PostgresqlFixture _fixture = fixture;

    public IServiceProvider Services { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        var builder = Host.CreateApplicationBuilder();

        // load normal app configuration
        builder.Configuration.AddJsonFile("appsettings.json", optional: true);
        builder.Configuration.AddUserSecrets<TestAppBase>();
        builder.Configuration.AddEnvironmentVariables();

        // override connection string for tests
        builder.Configuration["ConnectionStrings:Default"] = _fixture.Connection;

        builder.Services.AddMediatorSource(builder.Configuration.GetSection("JWT"), _fixture.Connection);

        var host = builder.Build();
        Services = host.Services;

        // apply migrations
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppUsersContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
