namespace Installer.Downloading.Abstract.StateMachine;

public abstract class StateTransition<TState, TStateStrategyFactory> : IStateTransition<TState>
    where TState : notnull
    where TStateStrategyFactory : IStateStrategyFactory<TState>
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

    public event Action<IStateStrategy<TState>>? MovedToState;
}