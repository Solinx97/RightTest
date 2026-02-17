using MediatR;

namespace RightTest.FinancesBL.Commands.CreateCurrency;

public record CreateCurrencyCommand(
    Guid Id,
    string Name,
    decimal Rate
    ) : IRequest<Guid>;