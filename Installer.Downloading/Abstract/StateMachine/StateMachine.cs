namespace Installer.Downloading.Abstract.StateMachine;

public abstract class StateMachine<TState>
    where TState : notnull
{
    private readonly IStateTransition<TState> _stateTransition;
    private IStateStrategy<TState> _currentStateStrategy;

    protected StateMachine(
        TState initialState,
        IStateTransition<TState> stateTransition,
        IStateStrategyFactory<TState> stateStrategyFactory,
        IThreadPool threadPool)
    {
        _stateTransition = stateTransition;

        _stateTransition.MovedToState += OnMovedToState;

        _currentStateStrategy = stateStrategyFactory.Create(initialState);
        threadPool.ExecuteAsync(_currentStateStrategy.EnterAsync, CancellationToken.None);
    }

    private async void OnMovedToState(IStateStrategy<TState> strategy)
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