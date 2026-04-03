namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Dtos;

public class UpdateExampleSoftDeletableEntityDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
