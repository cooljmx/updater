using System.Diagnostics;
using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class WaitingProcessFinishedApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;

    public WaitingProcessFinishedApplicationStateStrategy(
        IApplicationContext applicationContext,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.WaitingProcessFinished;

    protected override async Task DoEnterAsync()
    {
        var processId = _applicationContext.GetValue<int>("processId");

        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            var process = Process.GetProcessById(processId);

            await process.WaitForExitAsync(cancellationTokenSource.Token);
        }
        catch (ArgumentException _)
        {
        }
        finally
        {
            await cancellationTokenSource.CancelAsync();
            cancellationTokenSource.Dispose();
        }

        _applicationStateTransition.MoveTo(ApplicationState.CopyingToTarget);
    }
}