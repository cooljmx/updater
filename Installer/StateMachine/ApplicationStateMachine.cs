namespace Installer.StateMachine;

internal class ApplicationStateMachine : IApplicationStateMachine
{
    private readonly IApplicationStateTransition _applicationStateTransition;
    private IApplicationStateStrategy _currentStateStrategy;

    public ApplicationStateMachine(
        IApplicationStateTransition applicationStateTransition,
        IApplicationStateStrategyFactory applicationStateStrategyFactory)
    {
        _applicationStateTransition = applicationStateTransition;

        _applicationStateTransition.MovedToState += OnMovedToState;

        _currentStateStrategy = applicationStateStrategyFactory.Create(ApplicationState.Created);
        _currentStateStrategy.Enter();
    }

    private void OnMovedToState(IApplicationStateStrategy strategy)
    {
        _currentStateStrategy.Exit();

        _currentStateStrategy = strategy;
        _currentStateStrategy.Enter();
    }

    public void Dispose()
    {
        _applicationStateTransition.MovedToState -= OnMovedToState;
    }
}