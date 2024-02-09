using Autofac;

namespace Launcher.Downloading.Abstract;

public interface IScopeRepository
{
    ILifetimeScope Add(Guid guid);

    void Remove(Guid guid);
}