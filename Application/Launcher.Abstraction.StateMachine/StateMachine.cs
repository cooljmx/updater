namespace Launcher.Abstraction.StateMachine;

public abstract class StateMachine<TState, TStateStrategy, TStateTransition, TStateStrategyFactory> : IStateMachine<TState>
    where TState : notnull
    where TStateStrategy : IStateStrategy<TState>
    where TStateTransition : IStateTransition<TState, TStateStrategy>
    where TStateStrategyFactory : IStateStrategyFactory<TState, TStateStrategy>
{
    private readonly TStateTransition _stateTransition;
    private IStateStrategy<TState> _currentStateStrategy;

    protected StateMachine(
        TState initialState,
        TStateTransition stateTransition,
        TStateStrategyFactory stateStrategyFactory,
        IThreadPool threadPool)
    {
        _stateTransition = stateTransition;

        _stateTransition.MovedToState += OnMovedToState;

        _currentStateStrategy = stateStrategyFactory.Create(initialState);
        threadPool.ExecuteAsync(_currentStateStrategy.EnterAsync, CancellationToken.None);
    }

    private async void OnMovedToState(TStateStrategy strategy)
    {
        try
        {
            await _currentStateStrategy.ExitAsync();

            _currentStateStrategy = strategy;
            await _currentStateStrategy.EnterAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            throw;
        }
    }

    public void Dispose()
    {
        _stateTransition.MovedToState -= OnMovedToState;
    }
}