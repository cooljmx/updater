namespace Launcher.Abstraction.StateMachine;

public abstract class StateStrategyFactory<TState, TStateStrategy> : IStateStrategyFactory<TState, TStateStrategy>
    where TState : notnull
    where TStateStrategy : IStateStrategy<TState>
{
    private readonly Dictionary<TState, Func<TStateStrategy>> _strategyFactories;

    protected StateStrategyFactory(IEnumerable<Func<TStateStrategy>> strategyFactories)
    {
        _strategyFactories = strategyFactories.ToDictionary(StateExtractor, factory => factory);
    }

    public TStateStrategy Create(TState state)
    {
        var factory = _strategyFactories[state];

        var strategy = factory.Invoke();

        return strategy;
    }

    private static TState StateExtractor(Func<TStateStrategy> factory)
    {
        var strategy = factory.Invoke();

        return strategy.State;
    }
}