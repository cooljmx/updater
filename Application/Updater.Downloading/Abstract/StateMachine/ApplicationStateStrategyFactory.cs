namespace Updater.Downloading.Abstract.StateMachine;

public abstract class ApplicationStateStrategyFactory<TState> : IStateStrategyFactory<TState>
    where TState : notnull
{
    private readonly Dictionary<TState, Func<IStateStrategy<TState>>> _strategyFactories;

    protected ApplicationStateStrategyFactory(IEnumerable<Func<IStateStrategy<TState>>> strategyFactories)
    {
        _strategyFactories = strategyFactories.ToDictionary(StateExtractor, factory => factory);
    }

    public IStateStrategy<TState> Create(TState state)
    {
        var factory = _strategyFactories[state];

        var strategy = factory.Invoke();

        return strategy;
    }

    private static TState StateExtractor(Func<IStateStrategy<TState>> factory)
    {
        var strategy = factory.Invoke();

        return strategy.State;
    }
}