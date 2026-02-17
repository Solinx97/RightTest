using MediatR;
using RightTest.FinancesBL.DTOs;

namespace RightTest.FinancesBL.Queries.GetCurrencyByName;

public record GetCurrencyByNameQuery(
    string Name
    ) : IRequest<IEnumerable<CurrencyDto>>;