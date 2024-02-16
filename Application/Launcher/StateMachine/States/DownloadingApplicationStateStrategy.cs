using Launcher.Abstraction.StateMachine;
using Launcher.Downloading.Scheduler;
using Launcher.Metadata;

namespace Launcher.StateMachine.States;

internal class DownloadingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IBaseAddressProvider _baseAddressProvider;
    private readonly IDownloadingScheduler _downloadingScheduler;
    private readonly ILocalMetadataProvider _localMetadataProvider;
    private readonly List<IScheduledDownloading> _scheduledDownloads = new();
    private bool _isAllDownloadsScheduled;

    public DownloadingApplicationStateStrategy(
        ILocalMetadataProvider localMetadataProvider,
        IApplicationContext applicationContext,
        IBaseAddressProvider baseAddressProvider,
        IDownloadingScheduler downloadingScheduler)
    {
        _localMetadataProvider = localMetadataProvider;
        _applicationContext = applicationContext;
        _baseAddressProvider = baseAddressProvider;
        _downloadingScheduler = downloadingScheduler;
    }

    public override ApplicationState State => ApplicationState.Downloading;

    protected override async Task DoEnterAsync()
    {
        var updateId = _applicationContext.GetValue<Guid>("updateId");
        var temporaryFolder = _applicationContext.GetValue<string>("temporaryFolder");
        var metadataFileName = _applicationContext.GetValue<string>("metadataFileName");

        var metadata = await _localMetadataProvider.GetAsync(metadataFileName);

        var baseAddress = _baseAddressProvider.Get();

        foreach (var (relativeUrl, checkSum) in metadata)
        {
            var fileName = Path.Combine(temporaryFolder, relativeUrl);

            var folder = Path.GetDirectoryName(fileName)
                         ?? throw new InvalidOperationException();

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var url = new Uri($"{baseAddress}{updateId}/{relativeUrl}");

            var scheduledDownloading = _downloadingScheduler.Schedule(url, fileName, checkSum);

            scheduledDownloading.Completed += OnCompleted;

            _scheduledDownloads.Add(scheduledDownloading);
        }

        _isAllDownloadsScheduled = true;

        OnCompleted();
    }

    private void OnCompleted()
    {
        if (!_isAllDownloadsScheduled)
            return;

        if (_scheduledDownloads.Any(downloading => !downloading.IsCompleted))
            return;

        Console.WriteLine();
    }

    protected override Task DoExitAsync()
    {
        var scheduledDownloads = _scheduledDownloads.ToArray();
        _scheduledDownloads.Clear();

        foreach (var scheduledDownloading in scheduledDownloads)
            scheduledDownloading.Completed -= OnCompleted;

        return base.DoExitAsync();
    }
}