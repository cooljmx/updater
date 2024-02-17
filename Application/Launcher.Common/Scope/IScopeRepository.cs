using Autofac;

namespace Launcher.Common.Scope;

public interface IScopeRepository
{
    ILifetimeScope Add(Guid guid);

    void Remove(Guid guid);
}