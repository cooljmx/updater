using Autofac;
using Launcher.Downloading.Abstract;
using Launcher.Downloading.StateMachine;

namespace Launcher.Downloading.Scheduler;

internal class Downloader : IDownloader
{
    private readonly IScopeRepository _scopeRepository;

    public Downloader(IScopeRepository scopeRepository)
    {
        _scopeRepository = scopeRepository;
    }

    public void Download(Uri source, string targetPath, string checkSum, IScheduledDownloadingSource scheduledDownloadingSource)
    {
        var id = Guid.NewGuid();
        var lifetimeScope = _scopeRepository.Add(id);

        var downloadingContextUpdater = lifetimeScope.Resolve<IDownloadingContextUpdater>();
        downloadingContextUpdater.SetValue("lifetimeScopeId", id);
        downloadingContextUpdater.SetValue("source", source);
        downloadingContextUpdater.SetValue("targetPath", targetPath);
        downloadingContextUpdater.SetValue("checkSum", checkSum);
        downloadingContextUpdater.SetValue("scheduledDownloadingSource", scheduledDownloadingSource);

        lifetimeScope.Resolve<IDownloadingStateMachine>();
    }
}