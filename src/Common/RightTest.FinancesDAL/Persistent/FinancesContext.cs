using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;

namespace RightTest.FinancesDAL.Persistent;

public class FinancesContext(DbContextOptions<FinancesContext> options) : DbContext(options)
{
    public DbSet<Currency>? Currency { get; }

    public DbSet<Favorite>? Favorite { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Currency>()
            .HasMany(f => f.Favorites)
            .WithOne(c => c.Currency)
            .HasForeignKey(ci => ci.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
