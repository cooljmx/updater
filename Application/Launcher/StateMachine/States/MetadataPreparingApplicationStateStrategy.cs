using Launcher.Abstraction.StateMachine;
using Launcher.Downloading.Scheduler;
using Launcher.Environment;

namespace Launcher.StateMachine.States;

internal class MetadataPreparingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IBaseAddressProvider _baseAddressProvider;
    private readonly IDownloadingScheduler _downloadingScheduler;
    private readonly IFolderProvider _folderProvider;
    private IScheduledDownloading _scheduledDownloading;

    public MetadataPreparingApplicationStateStrategy(
        IFolderProvider folderProvider,
        IApplicationContext applicationContext,
        IDownloadingScheduler downloadingScheduler,
        IBaseAddressProvider baseAddressProvider,
        IApplicationStateTransition applicationStateTransition)
    {
        _folderProvider = folderProvider;
        _applicationContext = applicationContext;
        _downloadingScheduler = downloadingScheduler;
        _baseAddressProvider = baseAddressProvider;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.MetadataPreparing;

    protected override Task DoEnterAsync()
    {
        var updateId = _applicationContext.GetValue<Guid>("updateId");

        var temporaryFolder = _folderProvider.GetTemporaryFolder(updateId);
        _applicationContext.SetValue("temporaryFolder", temporaryFolder);

        if (!Directory.Exists(temporaryFolder))
            Directory.CreateDirectory(temporaryFolder);

        var metadataFileName = Path.Combine(temporaryFolder, "metadata.json");
        _applicationContext.SetValue("metadataFileName", metadataFileName);

        if (File.Exists(metadataFileName))
            File.Delete(metadataFileName);

        var baseAddress = _baseAddressProvider.Get();

        _scheduledDownloading = _downloadingScheduler.Schedule(
            new Uri($"{baseAddress}{updateId}/metadata.json"),
            metadataFileName,
            string.Empty);

        _scheduledDownloading.Completed += OnCompleted;

        if (_scheduledDownloading.IsCompleted)
            OnCompleted();

        return Task.CompletedTask;
    }

    private void OnCompleted()
    {
        _applicationStateTransition.MoveTo(ApplicationState.Downloading);
    }

    protected override Task DoExitAsync()
    {
        _scheduledDownloading.Completed -= OnCompleted;

        return Task.CompletedTask;
    }
}