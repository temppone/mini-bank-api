using ApiTest.Model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Balance> Balances { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<DailyTransactions> DailyTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(a => a.Identifier)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Balance>()
            .Property(b => b.Identifier)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Identifier)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<DailyTransactions>()
            .Property(dt => dt.Identifier)
            .ValueGeneratedOnAdd();
    }
}
