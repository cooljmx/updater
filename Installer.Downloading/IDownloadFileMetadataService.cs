namespace Installer.Downloading;

internal interface IDownloadFileMetadataService
{
    Task UpdateFileAsync();

    Task UpdateContextAsync();
}