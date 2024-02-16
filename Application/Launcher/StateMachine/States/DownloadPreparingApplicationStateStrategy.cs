using Launcher.Abstraction.StateMachine;
using Launcher.Metadata;

namespace Launcher.StateMachine.States;

internal class DownloadPreparingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly IBaseAddressProvider _baseAddressProvider;
    private readonly ILocalMetadataProvider _localMetadataProvider;

    public DownloadPreparingApplicationStateStrategy(
        IApplicationContext applicationContext,
        ILocalMetadataProvider localMetadataProvider,
        IBaseAddressProvider baseAddressProvider,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _localMetadataProvider = localMetadataProvider;
        _baseAddressProvider = baseAddressProvider;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.DownloadPreparing;

    protected override async Task DoEnterAsync()
    {
        var downloadQueue = new Queue<DownloadQueueItem>();

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

            var downloadInfo = new DownloadQueueItem(url, fileName, checkSum);
            downloadQueue.Enqueue(downloadInfo);
        }

        _applicationContext.SetValue("downloadQueue", downloadQueue);

        _applicationStateTransition.MoveTo(ApplicationState.DownloadContinuing);
    }
}