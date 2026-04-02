using BankRecon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankRecon.Infrastructure.Configurations;

// This is a template - replace/delete when you create your actual entities
// For example, if you have a ExampleTable entity in your Domain:
// For entities that need soft delete
public class ExampleSoftDeletableEntityConfiguration : SoftDeletableEntityConfiguration<ExampleSoftDeletableEntity>
{
    public override void Configure(EntityTypeBuilder<ExampleSoftDeletableEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(ExampleSoftDeletableEntity));
        // Your custom configuration
    }
}

