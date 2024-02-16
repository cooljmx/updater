using Launcher.Abstraction.StateMachine;
using Launcher.Downloading;
using Launcher.Environment;

namespace Launcher.StateMachine.States;

internal class MetadataPreparingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IBaseAddressProvider _baseAddressProvider;
    private readonly IDownloader _downloader;
    private readonly IFolderProvider _folderProvider;

    public MetadataPreparingApplicationStateStrategy(
        IFolderProvider folderProvider,
        IApplicationContext applicationContext,
        IBaseAddressProvider baseAddressProvider,
        IApplicationStateTransition applicationStateTransition,
        IDownloader downloader)
    {
        _folderProvider = folderProvider;
        _applicationContext = applicationContext;
        _baseAddressProvider = baseAddressProvider;
        _applicationStateTransition = applicationStateTransition;
        _downloader = downloader;
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

        await _downloader.DownloadAsync(
            new Uri($"{baseAddress}{updateId}/metadata.json"),
            metadataFileName,
            string.Empty);

        _applicationStateTransition.MoveTo(ApplicationState.DownloadPreparing);
    }
}