using System.Reflection;
using System.Text.Json;
using BankRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BankRecon.Infrastructure.Data;

public class BankReconDbContext : DbContext
{
    public BankReconDbContext(DbContextOptions<BankReconDbContext> options)
        : base(options)
    {
    }

    // Register your DbSets here
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<AuditEntry> auditEntries = OnBeforeSaveChanges();
        int result = await base.SaveChangesAsync(cancellationToken);
        await OnAfterSaveChangesAsync(auditEntries, cancellationToken);
        return result;
    }

    /// <summary>
    /// Captures change tracker entries before saving. For Added entities,
    /// the primary key is not yet available — these are deferred to after save.
    /// </summary>
    private List<AuditEntry> OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();

        foreach (EntityEntry entry in ChangeTracker.Entries())
        {
            // Skip AuditLog itself to avoid infinite recursion
            if (entry.Entity is AuditLog
                || entry.State == EntityState.Detached
                || entry.State == EntityState.Unchanged)
            {
                continue;
            }

            var auditEntry = new AuditEntry(entry)
            {
                EntityName = entry.Entity.GetType().Name,
                Timestamp = DateTimeOffset.UtcNow
            };

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.Action = "Create";
                    foreach (PropertyEntry property in entry.Properties)
                    {
                        auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                    }
                    break;

                case EntityState.Modified:
                    auditEntry.Action = "Update";
                    foreach (PropertyEntry property in entry.Properties)
                    {
                        if (property.IsModified)
                        {
                            auditEntry.AffectedColumns.Add(property.Metadata.Name);
                            auditEntry.OldValues[property.Metadata.Name] = property.OriginalValue;
                            auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                        }
                    }
                    break;

                case EntityState.Deleted:
                    auditEntry.Action = "Delete";
                    foreach (PropertyEntry property in entry.Properties)
                    {
                        auditEntry.OldValues[property.Metadata.Name] = property.OriginalValue;
                    }
                    break;
            }

            auditEntries.Add(auditEntry);
        }

        return auditEntries;
    }

    /// <summary>
    /// Persists audit log entries after the primary save so that
    /// generated keys (e.g., for Added entities) are available.
    /// </summary>
    private async Task OnAfterSaveChangesAsync(
        List<AuditEntry> auditEntries,
        CancellationToken cancellationToken)
    {
        if (auditEntries.Count == 0)
        {
            return;
        }

        foreach (AuditEntry auditEntry in auditEntries)
        {
            // For Added entities, the Id is now generated
            string entityId = auditEntry.Entry.Properties
                .FirstOrDefault(p => p.Metadata.Name == "Id")?.CurrentValue?.ToString() ?? string.Empty;

            AuditLogs.Add(new AuditLog
            {
                EntityName = auditEntry.EntityName,
                EntityId = entityId,
                Action = auditEntry.Action,
                OldValues = auditEntry.OldValues.Count > 0
                    ? JsonSerializer.Serialize(auditEntry.OldValues)
                    : null,
                NewValues = auditEntry.NewValues.Count > 0
                    ? JsonSerializer.Serialize(auditEntry.NewValues)
                    : null,
                AffectedColumns = auditEntry.AffectedColumns.Count > 0
                    ? string.Join(", ", auditEntry.AffectedColumns)
                    : null,
                Timestamp = auditEntry.Timestamp,
                PerformedBy = auditEntry.PerformedBy
            });
        }

        await base.SaveChangesAsync(cancellationToken);
    }
}

/// <summary>
/// Temporary holder for audit data collected during SaveChanges.
/// </summary>
internal class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }

    public EntityEntry Entry { get; }
    public string EntityName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public Dictionary<string, object?> OldValues { get; } = new();
    public Dictionary<string, object?> NewValues { get; } = new();
    public List<string> AffectedColumns { get; } = new();
    public DateTimeOffset Timestamp { get; set; }
    public string? PerformedBy { get; set; }
}
