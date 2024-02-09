using Autofac;

namespace Updater.Downloading.Abstract;

public interface IScopeRepository
{
    ILifetimeScope Add(Guid guid);

    void Remove(Guid guid);
}