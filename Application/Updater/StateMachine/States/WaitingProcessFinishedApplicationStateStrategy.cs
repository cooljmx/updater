using System.Diagnostics;

namespace Updater.StateMachine.States;

internal class WaitingProcessFinishedApplicationStateStrategy : BaseApplicationStateStrategy
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

    protected override void DoEnter()
    {
        var processId = _applicationContext.GetValue<int>("processId");

        try
        {
            var process = Process.GetProcessById(processId);

            process.WaitForExit(TimeSpan.FromSeconds(5));
        }
        catch (ArgumentException _)
        {
        }

        _applicationStateTransition.MoveTo(ApplicationState.CopyingToTarget);
    }
}