using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine.States;

internal class ContinueDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private const long MaxBufferSize = 4 * 1024 * 1024L;
    private readonly IDownloadFileMetadataService _downloadFileMetadataService;
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingContextUpdater _downloadingContextUpdater;
    private readonly IDownloadingStateTransition _downloadingStateTransition;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IThreadPool _threadPool;

    public ContinueDownloadingStateStrategy(
        IDownloadingContextProvider downloadingContextProvider,
        IDownloadingContextUpdater downloadingContextUpdater,
        IHttpClientFactory httpClientFactory,
        IDownloadFileMetadataService downloadFileMetadataService,
        IDownloadingStateTransition downloadingStateTransition,
        IThreadPool threadPool)
    {
        _downloadingContextProvider = downloadingContextProvider;
        _downloadingContextUpdater = downloadingContextUpdater;
        _httpClientFactory = httpClientFactory;
        _downloadFileMetadataService = downloadFileMetadataService;
        _downloadingStateTransition = downloadingStateTransition;
        _threadPool = threadPool;
    }

    public override DownloadingState State => DownloadingState.Continue;

    protected override async Task DoEnterAsync()
    {
        var finalSize = _downloadingContextProvider.GetValue<long>("finalSize");
        var downloadedSize = _downloadingContextProvider.GetValue<long>("downloadedSize");
        var source = _downloadingContextProvider.GetValue<Uri>("source");
        var targetPath = _downloadingContextProvider.GetValue<string>("targetPath");

        var bufferSize = finalSize - downloadedSize;

        if (bufferSize > MaxBufferSize)
            bufferSize = MaxBufferSize;

        using var httpClient = _httpClientFactory.CreateClient();

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, source)
        {
            Headers = { { "Range", $"bytes={downloadedSize}-{downloadedSize + bufferSize - 1}" } },
        };

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        await using var fileStream = File.OpenWrite(targetPath);

        fileStream.Seek(downloadedSize, SeekOrigin.Begin);

        await httpResponseMessage.Content.CopyToAsync(fileStream);

        downloadedSize += bufferSize;

        _downloadingContextUpdater.SetValue("downloadedSize", downloadedSize);

        await _downloadFileMetadataService.UpdateFileAsync();

        if (finalSize == downloadedSize)
            _downloadingStateTransition.MoveTo(DownloadingState.Completed);
        else
            _threadPool.ExecuteAsync(() => _downloadingStateTransition.MoveTo(DownloadingState.Continue), CancellationToken.None);
    }
}