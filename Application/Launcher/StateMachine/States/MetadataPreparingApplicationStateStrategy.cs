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

    protected override async Task DoEnterAsync()
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

        var scheduledDownloading = _downloadingScheduler.Schedule(
            new Uri($"{baseAddress}{updateId}/metadata.json"),
            metadataFileName,
            string.Empty);

        await scheduledDownloading.Download;

        _applicationStateTransition.MoveTo(ApplicationState.DownloadPreparing);
    }
}