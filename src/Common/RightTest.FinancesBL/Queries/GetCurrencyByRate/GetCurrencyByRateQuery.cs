using MediatR;
using RightTest.FinancesBL.DTOs;

namespace RightTest.FinancesBL.Queries.GetCurrencyByRate;

public record GetCurrencyByRateQuery(
    decimal Rate
    ) : IRequest<IEnumerable<CurrencyDto>>;