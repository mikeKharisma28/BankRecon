using BankRecon.Domain.Common;
using BankRecon.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRecon.Infrastructure.Configurations;

/// <summary>
/// Configuration for entities inheriting from AuditableEntity.
/// Extends BaseEntityConfiguration to add ISoftDeletable configuration.
/// </summary>
public abstract class SoftDeletableEntityConfiguration<T> : BaseEntityConfiguration<T> where T : SoftDeletableEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        ConfigureSoftDeletableProperties(builder);
    }

    protected virtual void ConfigureSoftDeletableProperties(EntityTypeBuilder<T> builder)
    {
        if (!typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
        {
            return;
        }

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(x => x.DeletedAt)
            .HasColumnType("datetimeoffset")
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(256)
            .IsRequired(false);

        // Add global query filter for soft-deleted entities
        builder.HasQueryFilter(e => !EF.Property<bool>(
            e,
            nameof(ISoftDeletable.IsDeleted)));
    }
}
