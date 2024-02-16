using Autofac;

namespace Launcher.Downloading;

public interface IScopeRepository
{
    ILifetimeScope Add(Guid guid);

    void Remove(Guid guid);
}