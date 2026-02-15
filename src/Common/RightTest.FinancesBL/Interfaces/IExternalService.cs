using RightTest.FinancesBL.DTOs;

namespace RightTest.FinancesBL.Interfaces;

public interface IExternalService
{
    Task<IEnumerable<ValuteDto>> GrabDataAsync(CancellationToken ct);
}
