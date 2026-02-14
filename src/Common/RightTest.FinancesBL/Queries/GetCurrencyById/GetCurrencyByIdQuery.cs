using MediatR;
using RightTest.FinancesBL.DTOs;

namespace RightTest.FinancesBL.Queries.GetCurrencyById;

public record GetCurrencyByIdQuery(
    Guid Id
    ) : IRequest<CurrencyDto?>;
