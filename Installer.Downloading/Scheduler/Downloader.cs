using Autofac;
using Installer.Downloading.Abstract;
using Installer.Downloading.StateMachine;

namespace Installer.Downloading.Scheduler;

internal class Downloader : IDownloader
{
    private readonly IScopeRepository _scopeRepository;

    public Downloader(IScopeRepository scopeRepository)
    {
        _scopeRepository = scopeRepository;
    }

    public void Download(Uri source, string targetPath, string checkSum)
    {
        var id = Guid.NewGuid();
        var lifetimeScope = _scopeRepository.Add(id);

        var downloadingContextUpdater = lifetimeScope.Resolve<IDownloadingContextUpdater>();
        downloadingContextUpdater.SetValue("lifetimeScopeId", id);
        downloadingContextUpdater.SetValue("source", source);
        downloadingContextUpdater.SetValue("targetPath", targetPath);
        downloadingContextUpdater.SetValue("checkSum", checkSum);

        lifetimeScope.Resolve<IDownloadingStateMachine>();
    }
}