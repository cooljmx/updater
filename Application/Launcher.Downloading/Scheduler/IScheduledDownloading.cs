namespace Launcher.Downloading.Scheduler;

public interface IScheduledDownloading
{
    bool IsCompleted { get; }
    event Action Completed;
}