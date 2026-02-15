using MediatR;
using RightTest.FinancesBL.Commands.CreateCurrency;
using RightTest.FinancesBL.DTOs;
using RightTest.FinancesBL.Interfaces;
using System.Globalization;

namespace RightTest.FinancesAPI.Services;

public class CurrencyService(IServiceScopeFactory scopeFactory, ILogger<CurrencyService> logger, IExternalService client) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<CurrencyService> _logger = logger;
    private readonly IExternalService _externalService = client;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("DataGrabberWorker started.");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var valutes = await _externalService.GrabDataAsync(ct);
                foreach (var valute in valutes)
                {
                    await FillDatabaseTableAsync(mediator, valute, ct);
                }

                _logger.LogInformation("Data received.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while grabbing data");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), ct);
        }

        _logger.LogInformation("DataGrabberWorker stopped.");
    }

    private static async Task FillDatabaseTableAsync(IMediator mediator, ValuteDto valute, CancellationToken ct)
    {
        if (decimal.TryParse(valute.VunitRate, NumberStyles.Any, new CultureInfo("ru-RU"), out decimal rate))
        {
            var createCommand = new CreateCurrencyCommand(Guid.NewGuid(), valute.Name, rate);
            await mediator.Send(createCommand, ct);
        }
    }
}
