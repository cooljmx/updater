using Installer.Downloading.Abstract;
using Installer.StateMachine;

namespace Installer;

internal sealed class Application : IApplication
{
    private readonly IApplicationStateMachine _applicationStateMachine;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IThreadPool _threadPool;

    public Application(
        IApplicationStateTransition applicationStateTransition,
        IApplicationStateMachine applicationStateMachine,
        IThreadPool threadPool)
    {
        _applicationStateTransition = applicationStateTransition;
        _applicationStateMachine = applicationStateMachine;
        _threadPool = threadPool;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _threadPool.ExecuteAsync(() => _applicationStateTransition.MoveTo(ApplicationState.Started), CancellationToken.None);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _applicationStateMachine.Dispose();

        return Task.CompletedTask;
    }
}