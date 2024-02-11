using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine.States;

internal class StartingDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private readonly IDownloadFileMetadataService _downloadFileMetadataService;
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingContextUpdater _downloadingContextUpdater;
    private readonly IDownloadingStateTransition _downloadingStateTransition;
    private readonly IHttpClientFactory _httpClientFactory;

    public StartingDownloadingStateStrategy(
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

    public override DownloadingState State => DownloadingState.Starting;

    protected override async Task DoEnterAsync()
    {
        var source = _downloadingContextProvider.GetValue<Uri>("source");
        var metadataFileName = _downloadingContextProvider.GetValue<string>("metadataFileName");

        if (File.Exists(metadataFileName))
        {
            await _downloadFileMetadataService.UpdateContextAsync();
        }
        else
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Head, source);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var headersContentLength = httpResponseMessage.Content.Headers.ContentLength ??
                                           throw new InvalidOperationException();

                _downloadingContextUpdater.SetValue("finalSize", headersContentLength);
                _downloadingContextUpdater.SetValue("downloadedSize", 0L);

                await _downloadFileMetadataService.UpdateFileAsync();
            }
        }

        _downloadingStateTransition.MoveTo(DownloadingState.Continue);
    }
}