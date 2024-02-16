using Launcher.Abstraction.StateMachine;
using Launcher.Downloading.Scheduler;

namespace Launcher.StateMachine.States;

internal class DownloadContinuingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IDownloadingScheduler _downloadingScheduler;

    public DownloadContinuingApplicationStateStrategy(
        IApplicationContext applicationContext,
        IDownloadingScheduler downloadingScheduler,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _downloadingScheduler = downloadingScheduler;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.DownloadContinuing;

    protected override async Task DoEnterAsync()
    {
        var downloadQueue = _applicationContext.GetValue<Queue<DownloadQueueItem>>("downloadQueue");

        if (downloadQueue.TryDequeue(out var item))
        {
            var scheduledDownloading = _downloadingScheduler.Schedule(item.Source, item.TargetFileName, item.CheckSum);

            await scheduledDownloading.Download;

            _applicationStateTransition.MoveTo(ApplicationState.DownloadContinuing);
        }
    }
}