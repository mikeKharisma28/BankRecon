using BankRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRecon.Infrastructure.Configurations;

/// <summary>
/// EF Core Fluent API configuration for the AuditLog entity.
/// </summary>
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EntityName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.EntityId)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.OldValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.NewValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.AffectedColumns)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(x => x.Timestamp)
            .HasColumnType("datetimeoffset")
            .IsRequired();

        builder.Property(x => x.PerformedBy)
            .HasMaxLength(256)
            .IsRequired(false);

        // Index for querying by entity
        builder.HasIndex(x => new { x.EntityName, x.EntityId });

        // Index for querying by timestamp
        builder.HasIndex(x => x.Timestamp);
    }
}
