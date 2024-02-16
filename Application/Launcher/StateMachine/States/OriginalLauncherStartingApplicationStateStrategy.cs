using System.Diagnostics;
using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class OriginalLauncherStartingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;

    public OriginalLauncherStartingApplicationStateStrategy(
        IApplicationContext applicationContext,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.OriginalLauncherStarting;

    protected override Task DoEnterAsync()
    {
        var targetPath = _applicationContext.GetValue<string>("targetPath");

        var launcherFileName = Path.Combine(targetPath, "launcher.exe");

        Process.Start(launcherFileName);

        _applicationStateTransition.MoveTo(ApplicationState.Shutdown);

        return Task.CompletedTask;
    }
}