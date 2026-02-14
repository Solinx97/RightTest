using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;

namespace RightTest.FinancesDAL.Persistent;

public class FinancesContext(DbContextOptions<FinancesContext> options) : DbContext(options)
{
    public DbSet<Currency>? Currency { get; }
}
