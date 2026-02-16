using System.ComponentModel.DataAnnotations;

namespace RightTest.FinancesAPI.Models;

public record FavoriteModel(
    [Required] Guid Id,
    [Required] Guid CurrencyId,
    [Required] string AppUserId
    );
