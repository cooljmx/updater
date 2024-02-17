using Autofac;
using Launcher.Common.Scope;
using Launcher.Downloading.StateMachine;

namespace Launcher.Downloading;

internal class Downloader : IDownloader
{
    private readonly IScopeRepository _scopeRepository;

    public Downloader(IScopeRepository scopeRepository)
    {
        _scopeRepository = scopeRepository;
    }

    public Task DownloadAsync(Uri source, string targetPath, string checkSum)
    {
        var taskCompletionSource = new TaskCompletionSource();
        var id = Guid.NewGuid();
        var lifetimeScope = _scopeRepository.Add(id);

        var downloadingContextUpdater = lifetimeScope.Resolve<IDownloadingContextUpdater>();
        downloadingContextUpdater.SetValue("lifetimeScopeId", id);
        downloadingContextUpdater.SetValue("source", source);
        downloadingContextUpdater.SetValue("targetPath", targetPath);
        downloadingContextUpdater.SetValue("checkSum", checkSum);
        downloadingContextUpdater.SetValue("taskCompletionSource", taskCompletionSource);

        lifetimeScope.Resolve<IDownloadingStateMachine>();

        return taskCompletionSource.Task;
    }
}