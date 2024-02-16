namespace Launcher.Downloading.Scheduler;

internal class DownloadingScheduler : IDownloadingScheduler, IDownloadingBackgroundService
{
    private readonly IDownloader _downloader;
    private readonly Queue<DownloadingScheduleInfo> _queue = new();
    private bool _isExecuting;
    private Timer? _timer;

    public DownloadingScheduler(IDownloader downloader)
    {
        _downloader = downloader;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(OnTick, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();

        return Task.CompletedTask;
    }

    public IScheduledDownloading Schedule(Uri source, string target, string checkSum)
    {
        var scheduledDownloadingSource = new ScheduledDownloadingSource();

        var info = new DownloadingScheduleInfo(source, target, checkSum, scheduledDownloadingSource);

        _queue.Enqueue(info);

        return scheduledDownloadingSource;
    }

    private void OnTick(object? state)
    {
        if (_isExecuting)
            return;

        _isExecuting = true;
        try
        {
            if (_queue.TryDequeue(out var info))
                _downloader.Download(info.Source, info.TargetFileName, info.CheckSum, info.ScheduledDownloadingSource);
        }
        finally
        {
            _isExecuting = false;
        }
    }
}