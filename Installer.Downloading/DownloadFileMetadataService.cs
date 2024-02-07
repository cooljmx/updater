using System.Text.Json;
using Installer.Downloading.StateMachine;

namespace Installer.Downloading;

internal class DownloadFileMetadataService : IDownloadFileMetadataService
{
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingContextUpdater _downloadingContextUpdater;

    public DownloadFileMetadataService(
        IDownloadingContextProvider downloadingContextProvider,
        IDownloadingContextUpdater downloadingContextUpdater)
    {
        _downloadingContextProvider = downloadingContextProvider;
        _downloadingContextUpdater = downloadingContextUpdater;
    }

    public async Task UpdateFileAsync()
    {
        var finalSize = _downloadingContextProvider.GetValue<long>("finalSize");
        var downloadedSize = _downloadingContextProvider.GetValue<long>("downloadedSize");
        var metadataFileName = _downloadingContextProvider.GetValue<string>("metadataFileName");

        var downloadFileMetadata = new DownloadFileMetadata(finalSize, downloadedSize);

        await using var fileStream = File.Create(metadataFileName);
        await JsonSerializer.SerializeAsync(fileStream, downloadFileMetadata);
    }

    public async Task UpdateContextAsync()
    {
        var metadataFileName = _downloadingContextProvider.GetValue<string>("metadataFileName");

        await using var fileStream = File.OpenRead(metadataFileName);
        var (finalSize, downloadedSize) = await JsonSerializer.DeserializeAsync<DownloadFileMetadata>(fileStream);

        _downloadingContextUpdater.SetValue("finalSize", finalSize);
        _downloadingContextUpdater.SetValue("downloadedSize", downloadedSize);
    }
}