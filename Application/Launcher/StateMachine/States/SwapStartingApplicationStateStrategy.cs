using System.Diagnostics;
using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class SwapStartingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;

    public SwapStartingApplicationStateStrategy(
        IApplicationContext applicationContext,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.SwapStarting;

    protected override Task DoEnterAsync()
    {
        var temporaryFolder = _applicationContext.GetValue<string>("temporaryFolder");

        var launcherFileName = Path.Combine(temporaryFolder, "launcher.exe");

        var currentProcess = Process.GetCurrentProcess();

        var arguments = $"swap {currentProcess.Id} {AppContext.BaseDirectory}";

        var processStartInfo = new ProcessStartInfo(launcherFileName, arguments);

        _ = Process.Start(processStartInfo);

        _applicationStateTransition.MoveTo(ApplicationState.Shutdown);

        return Task.CompletedTask;
    }
}