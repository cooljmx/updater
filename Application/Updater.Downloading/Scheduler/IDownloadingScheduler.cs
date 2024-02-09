namespace Updater.Downloading.Scheduler;

public interface IDownloadingScheduler
{
    void Schedule(Uri source, string target, string checkSum);
}