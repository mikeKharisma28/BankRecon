namespace BankRecon.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a requested entity is not found in the data store.
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
        : base("The requested entity was not found.")
    {
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public EntityNotFoundException(string entityName, object key)
        : base($"Entity \"{entityName}\" ({key}) was not found.")
    {
    }
}
