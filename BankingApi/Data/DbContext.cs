using Microsoft.EntityFrameworkCore;
using BankingApi.Models;

namespace BankingApi.SQLiteDatabase;

public class BankingContext : DbContext
{
    public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);

        modelBuilder.Entity<Customer>()
            .HasMany(a => a.Accounts)
            .WithOne(c => c.Customer)
            .HasForeignKey(a => a.CustomerId);
    }

}