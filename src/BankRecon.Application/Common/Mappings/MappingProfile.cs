using System.Reflection;
using AutoMapper;
using BankRecon.Shared.Common.Mappings;

namespace BankRecon.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile that scans the Application and Shared assemblies
/// for all types implementing <see cref="IMapFrom{T}"/> and invokes their mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Scan both assemblies for IMapFrom implementations
        ApplyMappingsFromAssembly(typeof(DependencyInjection).Assembly);   // Application
        ApplyMappingsFromAssembly(typeof(IMapFrom<>).Assembly);            // Shared
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == mapFromType))
            .ToList();

        foreach (Type type in types)
        {
            object? instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName)
                ?? type.GetInterface(mapFromType.Name)?.GetMethod(mappingMethodName);

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}
