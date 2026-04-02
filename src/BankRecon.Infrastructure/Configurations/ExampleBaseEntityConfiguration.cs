using BankRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRecon.Infrastructure.Configurations;

// This is a template - replace/delete when you create your actual entities
// For example, if you have a ExampleTable entity in your Domain:
// For entities that don't need soft delete
public class ExampleBaseEntityConfiguration : BaseEntityConfiguration<ExampleBaseEntity>
{
    public override void Configure(EntityTypeBuilder<ExampleBaseEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(ExampleBaseEntity));
        // Your custom configuration
    }
}

