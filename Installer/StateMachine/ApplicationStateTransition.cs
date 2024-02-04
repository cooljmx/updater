namespace Installer.StateMachine;

internal class ApplicationStateTransition : IApplicationStateTransition
{
    private readonly Lazy<IApplicationStateStrategyFactory> _applicationStateStrategyLazyFactory;

    public ApplicationStateTransition(Lazy<IApplicationStateStrategyFactory> applicationStateStrategyLazyFactory)
    {
        _applicationStateStrategyLazyFactory = applicationStateStrategyLazyFactory;
    }

    public void MoveTo(ApplicationState state)
    {
        var strategy = _applicationStateStrategyLazyFactory.Value.Create(state);

        MovedToState?.Invoke(strategy);
    }

    public event Action<IApplicationStateStrategy>? MovedToState;
}