using BankRecon.Domain.Common;
using BankRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankRecon.Infrastructure.Data;

public class BankReconDbContext : DbContext
{
    public BankReconDbContext(DbContextOptions<BankReconDbContext> options)
        : base(options)
    {
    }

    // Register your DbSets here
    public DbSet<ExampleSoftDeletableEntity> ExampleAuditableEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
