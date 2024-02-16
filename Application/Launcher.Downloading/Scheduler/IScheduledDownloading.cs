namespace Launcher.Downloading.Scheduler;

public interface IScheduledDownloading
{
    Task Download { get; }
    bool IsCompleted { get; }
    event Action Completed;
}