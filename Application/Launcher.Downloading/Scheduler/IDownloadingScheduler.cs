namespace Launcher.Downloading.Scheduler;

public interface IDownloadingScheduler
{
    IScheduledDownloading Schedule(Uri source, string target, string checkSum);
}