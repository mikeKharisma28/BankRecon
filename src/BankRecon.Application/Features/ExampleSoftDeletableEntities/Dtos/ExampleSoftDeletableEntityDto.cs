using AutoMapper;
using BankRecon.Application.Common.Mappings;
using BankRecon.Domain.Entities;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;

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
