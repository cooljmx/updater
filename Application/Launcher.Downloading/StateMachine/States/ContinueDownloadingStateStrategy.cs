using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine.States;

internal class ContinueDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private const long MaxBufferSize = 64 * 1024L;
    private readonly IDownloadFileMetadataService _downloadFileMetadataService;
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingContextUpdater _downloadingContextUpdater;
    private readonly IDownloadingStateTransition _downloadingStateTransition;
    private readonly IHttpClientFactory _httpClientFactory;

    public ContinueDownloadingStateStrategy(
        IDownloadingContextProvider downloadingContextProvider,
        IDownloadingContextUpdater downloadingContextUpdater,
        IHttpClientFactory httpClientFactory,
        IDownloadFileMetadataService downloadFileMetadataService,
        IDownloadingStateTransition downloadingStateTransition)
    {
        _downloadingContextProvider = downloadingContextProvider;
        _downloadingContextUpdater = downloadingContextUpdater;
        _httpClientFactory = httpClientFactory;
        _downloadFileMetadataService = downloadFileMetadataService;
        _downloadingStateTransition = downloadingStateTransition;
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
            _downloadingStateTransition.MoveTo(DownloadingState.Continue);
    }
}