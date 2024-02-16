namespace Launcher.Downloading.Scheduler;

internal class ScheduledDownloadingSource : IScheduledDownloadingSource, IScheduledDownloading
{
    private readonly TaskCompletionSource _downloadCompletionSource;

    public ScheduledDownloadingSource()
    {
        _downloadCompletionSource = new TaskCompletionSource();
    }

    public Task Download => _downloadCompletionSource.Task;

    public bool IsCompleted => _downloadCompletionSource.Task.IsCompleted;

    public event Action? Completed;

    public void Complete()
    {
        _downloadCompletionSource.SetResult();
        Completed?.Invoke();
    }
}