namespace Launcher.Downloading.Scheduler;

internal class ScheduledDownloadingSource : IScheduledDownloadingSource, IScheduledDownloading
{
    private readonly TaskCompletionSource _downloadCompletionSource;

    public ScheduledDownloadingSource()
    {
        _downloadCompletionSource = new TaskCompletionSource();
    }

    public Task Download => _downloadCompletionSource.Task;

    public void Complete()
    {
        _downloadCompletionSource.SetResult();
    }
}