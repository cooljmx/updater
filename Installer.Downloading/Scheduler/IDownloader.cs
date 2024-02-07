namespace Installer.Downloading.Scheduler;

internal interface IDownloader
{
    void Download(Uri source, string targetPath, string checkSum);
}