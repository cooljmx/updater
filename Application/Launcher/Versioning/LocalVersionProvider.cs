using System.Reflection;

namespace Launcher.Versioning;

internal class LocalVersionProvider : ILocalVersionProvider
{
    public Version Get()
    {
        var assembly = GetType().Assembly;

        var assemblyFileVersionAttribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();

        return assemblyFileVersionAttribute is null
            ? new Version(0, 0, 0, 0)
            : new Version(assemblyFileVersionAttribute.Version);
    }
}