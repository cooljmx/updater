namespace Launcher.Downloading.Scheduler;

internal class ScheduledDownloadingSource : IScheduledDownloadingSource, IScheduledDownloading
{
    private bool _isCompleted;

    public bool IsCompleted
    {
        get => _isCompleted;
        private set
        {
            _isCompleted = value;

            Completed?.Invoke();
        }
    }

    public event Action? Completed;

    public void Complete()
    {
        IsCompleted = true;
    }
}