namespace Launcher.Downloading;

internal interface IDownloadFileMetadataService
{
    Task UpdateFileAsync();

    Task UpdateContextAsync();
}