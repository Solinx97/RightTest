using MediatR;

namespace RightTest.FinancesBL.Commands.UpdateCurrencyName;

public record UpdateCurrencyNameCommand(
    Guid Id,
    string Name
    ) : IRequest;