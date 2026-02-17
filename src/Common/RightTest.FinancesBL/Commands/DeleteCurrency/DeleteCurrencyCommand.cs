using MediatR;

namespace RightTest.FinancesBL.Commands.DeleteCurrency;

public record DeleteCurrencyCommand(
    Guid Id
    ) : IRequest;