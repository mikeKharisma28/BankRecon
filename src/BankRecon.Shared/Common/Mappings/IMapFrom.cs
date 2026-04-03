using AutoMapper;

namespace BankRecon.Shared.Common.Mappings;

/// <summary>
/// Marker interface for automatic AutoMapper profile registration.
/// Implement on DTOs to define a mapping from <typeparamref name="T"/>.
/// </summary>
public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
