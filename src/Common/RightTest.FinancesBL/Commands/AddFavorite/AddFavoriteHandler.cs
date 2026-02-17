using MediatR;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesBL.Commands.AddFavorite;

internal class AddFavoriteHandler(FinancesContext context) : IRequestHandler<AddFavoriteCommand, Guid>
{
    private readonly FinancesContext _context = context;

    public async Task<Guid> Handle(AddFavoriteCommand request, CancellationToken cancelationToken)
    {
        var favorite = new Favorite
        {
            Id = request.Id,
            CurrencyId = request.CurrencyId,
            AppUserId = request.AppUserId,
        };

        await _context.Set<Favorite>()
            .AddAsync(favorite, cancelationToken);

        await _context.SaveChangesAsync(cancelationToken);

        return favorite.Id;
    }
}

