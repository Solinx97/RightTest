using MediatR;

namespace RightTest.FinancesBL.Commands.UpdateCurrencyRate;

public record UpdateCurrencyRateCommand(
    Guid Id,
    decimal Rate
    ) : IRequest;