namespace Launcher.Downloading.Scheduler;

internal interface IDownloader
{
    void Download(Uri source, string targetPath, string checkSum);
}