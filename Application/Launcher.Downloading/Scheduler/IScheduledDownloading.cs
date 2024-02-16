namespace Launcher.Downloading.Scheduler;

public interface IScheduledDownloading
{
    Task Download { get; }
}