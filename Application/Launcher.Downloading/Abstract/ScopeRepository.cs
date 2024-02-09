using Autofac;

namespace Launcher.Downloading.Abstract;

internal class ScopeRepository : IScopeRepository
{
    private readonly ILifetimeScope _rootLifetimeScope;

    private readonly Dictionary<Guid, ILifetimeScope> _scopes = new();

    public ScopeRepository(ILifetimeScope rootLifetimeScope)
    {
        _rootLifetimeScope = rootLifetimeScope;
    }

    public ILifetimeScope Add(Guid guid)
    {
        var lifetimeScope = _rootLifetimeScope.BeginLifetimeScope();

        _scopes.Add(guid, lifetimeScope);

        return lifetimeScope;
    }

    public void Remove(Guid guid)
    {
        if (_scopes.Remove(guid, out var lifetimeScope))
            lifetimeScope.Dispose();
    }
}