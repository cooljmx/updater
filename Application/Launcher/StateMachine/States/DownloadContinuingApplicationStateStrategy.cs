using Launcher.Abstraction.StateMachine;
using Launcher.Downloading;

namespace Launcher.StateMachine.States;

internal class DownloadContinuingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IDownloader _downloader;

    public DownloadContinuingApplicationStateStrategy(
        IApplicationContext applicationContext,
        IApplicationStateTransition applicationStateTransition,
        IDownloader downloader)
    {
        _applicationContext = applicationContext;
        _applicationStateTransition = applicationStateTransition;
        _downloader = downloader;
    }

    public override ApplicationState State => ApplicationState.DownloadContinuing;

    protected override async Task DoEnterAsync()
    {
        var downloadQueue = _applicationContext.GetValue<Queue<DownloadQueueItem>>("downloadQueue");

        if (downloadQueue.TryDequeue(out var item))
        {
            await _downloader.DownloadAsync(item.Source, item.TargetFileName, item.CheckSum);

            _applicationStateTransition.MoveTo(ApplicationState.DownloadContinuing);
        }
    }
}