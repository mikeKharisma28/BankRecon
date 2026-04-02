using BankRecon.Domain.Common;
using BankRecon.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRecon.Infrastructure.Configurations;

/// <summary>
/// Base configuration for all entities inheriting from BaseEntity.
/// Handles ICreatable and IUpdatable property configuration.
/// </summary>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigureBaseProperties(builder);
        ConfigureCreatableProperties(builder);
        ConfigureUpdatableProperties(builder);
    }

    protected virtual void ConfigureBaseProperties(EntityTypeBuilder<T> builder)
    {
        // Configure primary key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd();
    }

    protected virtual void ConfigureCreatableProperties(EntityTypeBuilder<T> builder)
    {
        // Configure ICreatable
        if (!typeof(ICreatable).IsAssignableFrom(typeof(T)))
        {
            return;
        }

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetimeoffset")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(256)
            .IsRequired(false);
    }

    protected virtual void ConfigureUpdatableProperties(EntityTypeBuilder<T> builder)
    {
        // Configure IUpdatable
        if (!typeof(IUpdatable).IsAssignableFrom(typeof(T)))
        {
            return;
        }

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("datetimeoffset")
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(256)
            .IsRequired(false);
    }
}
