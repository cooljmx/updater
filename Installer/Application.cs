using Installer.StateMachine;

namespace Installer;

internal sealed class Application : IApplication, IDisposable
{
    private readonly IApplicationStateMachine _applicationStateMachine;
    private readonly IApplicationStateTransition _applicationStateTransition;

    public Application(
        IApplicationStateTransition applicationStateTransition,
        IApplicationStateMachine applicationStateMachine)
    {
        _applicationStateTransition = applicationStateTransition;
        _applicationStateMachine = applicationStateMachine;
    }

    public void Start()
    {
        _applicationStateTransition.MoveTo(ApplicationState.Started);
    }

    public void Dispose()
    {
        _applicationStateMachine.Dispose();
    }
}