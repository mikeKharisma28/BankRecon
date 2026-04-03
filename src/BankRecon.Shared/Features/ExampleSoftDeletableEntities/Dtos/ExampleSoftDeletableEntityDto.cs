using AutoMapper;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Mappings;

namespace BankRecon.Shared.Features.ExampleSoftDeletableEntities.Dtos;

public class ExampleSoftDeletableEntityDto : IMapFrom<ExampleSoftDeletableEntity>
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExampleSoftDeletableEntity, ExampleSoftDeletableEntityDto>();
    }
}
