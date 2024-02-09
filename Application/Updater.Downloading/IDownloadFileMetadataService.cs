namespace Updater.Downloading;

internal interface IDownloadFileMetadataService
{
    Task UpdateFileAsync();

    Task UpdateContextAsync();
}