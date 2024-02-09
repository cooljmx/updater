using Launcher.Commands;
using Launcher.Downloading.Scheduler;

namespace Launcher.StateMachine.States;

internal class StartedApplicationStateStrategy : BaseApplicationStateStrategy
{
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ICommandProvider _commandProvider;
    private readonly IDownloadingScheduler _downloadingScheduler;

    public StartedApplicationStateStrategy(
        ICommandProvider commandProvider,
        IApplicationStateTransition applicationStateTransition,
        IDownloadingScheduler downloadingScheduler)
    {
        _commandProvider = commandProvider;
        _applicationStateTransition = applicationStateTransition;
        _downloadingScheduler = downloadingScheduler;
    }

    public override ApplicationState State => ApplicationState.Started;

    protected override void DoEnter()
    {
        //switch (_commandProvider.Get())
        //{
        //    case Command.Swap:
        //        _applicationStateTransition.MoveTo(ApplicationState.Swap);
        //        break;
        //    default:
        //        throw new ArgumentOutOfRangeException();
        //}

        _downloadingScheduler.Schedule(
            new Uri("https://downloads.wdc.com/wdapp/Install_WD_Discovery_for_Windows.zip"),
            "E:\\temp\\7\\Install_WD_Discovery_for_Windows.zip",
            "1f4519a4df91f0caee835538c0664dda82bb5f5848440d90bc83be4a98469f3b");
    }
}