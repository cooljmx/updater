namespace Launcher.Abstraction.StateMachine;

public abstract class StateTransition<TState, TStateStrategy, TStateStrategyFactory> : IStateTransition<TState, TStateStrategy>
    where TState : notnull
    where TStateStrategy : IStateStrategy<TState>
    where TStateStrategyFactory : IStateStrategyFactory<TState, TStateStrategy>
{
    private readonly Lazy<TStateStrategyFactory> _stateStrategyLazyFactory;

    protected StateTransition(Lazy<TStateStrategyFactory> stateStrategyLazyFactory)
    {
        _stateStrategyLazyFactory = stateStrategyLazyFactory;
    }

    public void MoveTo(TState state)
    {
        var strategy = _stateStrategyLazyFactory.Value.Create(state);

        MovedToState?.Invoke(strategy);
    }

    public event Action<TStateStrategy>? MovedToState;
}