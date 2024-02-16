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

        var launcherPath = Path.Combine(temporaryFolder, "launcher.exe");

        var currentProcess = Process.GetCurrentProcess();
        var currentAssemblyFileName = GetType().Assembly.Location;
        var currentProcessPath = Path.GetDirectoryName(currentAssemblyFileName);

        var arguments = $"swap {currentProcess.Id} {currentProcessPath}";

        var processStartInfo = new ProcessStartInfo(launcherPath, arguments);

        _ = Process.Start(processStartInfo);

        _applicationStateTransition.MoveTo(ApplicationState.Shutdown);

        return Task.CompletedTask;
    }
}