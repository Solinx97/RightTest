using System.ComponentModel.DataAnnotations;

namespace RightTest.FinancesAPI.Models;

public record CurrencyModel(
    [Required] Guid Id,
    [Required] string Name,
    [Required] decimal Rate
    );
