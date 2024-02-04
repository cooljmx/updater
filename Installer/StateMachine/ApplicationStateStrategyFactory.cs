namespace Installer.StateMachine;

internal class ApplicationStateStrategyFactory : IApplicationStateStrategyFactory
{
    private readonly Dictionary<ApplicationState, Func<IApplicationStateStrategy>> _strategyFactories;

    public ApplicationStateStrategyFactory(IEnumerable<Func<IApplicationStateStrategy>> strategyFactories)
    {
        _strategyFactories = strategyFactories.ToDictionary(StateExtractor, factory => factory);
    }

    public IApplicationStateStrategy Create(ApplicationState state)
    {
        var factory = _strategyFactories[state];

        var strategy = factory.Invoke();

        return strategy;
    }

    private ApplicationState StateExtractor(Func<IApplicationStateStrategy> factory)
    {
        var strategy = factory.Invoke();

        return strategy.State;
    }
}